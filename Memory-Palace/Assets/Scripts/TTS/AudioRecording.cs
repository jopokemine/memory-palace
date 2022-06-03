using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

using ItemDB = DataBank.Items;

// IBM specific
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Utilities;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Watson.SpeechToText.V1;
using IBM.Watson.SpeechToText.V1.Model;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MemoryPalace.TTS {
    [RequireComponent(typeof (AudioSource))]
    public class AudioRecording : MonoBehaviour {
        AudioSource audioSource;
        public GameObject listeningPopup;
        public GameObject queryPopup;
        Text queryText;

        IEnumerator audioRecordCoroutine;

        void Awake() {
            audioSource = gameObject.GetComponent<AudioSource>();
            audioRecordCoroutine = this.Recording(5f);
            queryText = queryPopup.transform.GetChild(1).GetComponent<Text>();
        }

        public void StartRecording() {
            audioSource.clip = Microphone.Start("", false, 6, 44100);
            StartCoroutine(audioRecordCoroutine);
            listeningPopup.SetActive(true);
        }

        public void CancelRecording() {
            StopCoroutine(audioRecordCoroutine);
            Microphone.End("");
            audioRecordCoroutine = this.Recording(5f);
        }

        IEnumerator Recording(float t) {
            yield return new WaitForSeconds(t);
            FinishRecording();
        }

        public void FinishRecording() {
            Microphone.End("");

            ExportClipData(audioSource.clip);
            StartSending();
            listeningPopup.SetActive(false);
            audioRecordCoroutine = this.Recording(5f);
        }

        // by https://stackoverflow.com/users/982639/alexandru

        void ExportClipData(AudioClip clip) {
            var data = new float[clip.samples * clip.channels];
            clip.GetData(data, 0);
            var path = Path.Combine(Application.persistentDataPath, "Recording.wav");
            // My contribution, check if file exists then remove it if it does, throws an error otherwise
            if(File.Exists(path)) {
                File.Delete(path);
            }
            using (var stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write)) {
                // The following values are based on http://soundfile.sapp.org/doc/WaveFormat/
                var bitsPerSample = (ushort)16;
                var chunkID = "RIFF";
                var format = "WAVE";
                var subChunk1ID = "fmt ";
                var subChunk1Size = (uint)16;
                var audioFormat = (ushort)1;
                var numChannels = (ushort)clip.channels;
                var sampleRate = (uint)clip.frequency;
                var byteRate = (uint)(sampleRate * clip.channels * bitsPerSample / 8);  // SampleRate * NumChannels * BitsPerSample/8
                var blockAlign = (ushort)(numChannels * bitsPerSample / 8); // NumChannels * BitsPerSample/8
                var subChunk2ID = "data";
                var subChunk2Size = (uint)(data.Length * clip.channels * bitsPerSample / 8); // NumSamples * NumChannels * BitsPerSample/8
                var chunkSize = (uint)(36 + subChunk2Size); // 36 + SubChunk2Size
                // Start writing the file.
                WriteString(stream, chunkID);
                WriteInteger(stream, chunkSize);
                WriteString(stream, format);
                WriteString(stream, subChunk1ID);
                WriteInteger(stream, subChunk1Size);
                WriteShort(stream, audioFormat);
                WriteShort(stream, numChannels);
                WriteInteger(stream, sampleRate);
                WriteInteger(stream, byteRate);
                WriteShort(stream, blockAlign);
                WriteShort(stream, bitsPerSample);
                WriteString(stream, subChunk2ID);
                WriteInteger(stream, subChunk2Size);
                foreach (var sample in data) {
                    // De-normalize the samples to 16 bits.
                    var deNormalizedSample = (short)0;
                    if (sample > 0) {
                        var temp = sample * short.MaxValue;
                        if (temp > short.MaxValue)
                            temp = short.MaxValue;
                        deNormalizedSample = (short)temp;
                    }
                    if (sample < 0) {
                        var temp = sample * (-short.MinValue);
                        if (temp < short.MinValue)
                            temp = short.MinValue;
                        deNormalizedSample = (short)temp;
                    }
                    WriteShort(stream, (ushort)deNormalizedSample);
                }
            }
        }

        void WriteString(Stream stream, string value) {
            foreach (var character in value)
                stream.WriteByte((byte)character);
        }

        void WriteInteger(Stream stream, uint value) {
            stream.WriteByte((byte)(value & 0xFF));
            stream.WriteByte((byte)((value >> 8) & 0xFF));
            stream.WriteByte((byte)((value >> 16) & 0xFF));
            stream.WriteByte((byte)((value >> 24) & 0xFF));
        }

        void WriteShort(Stream stream, ushort value) {
            stream.WriteByte((byte)(value & 0xFF));
            stream.WriteByte((byte)((value >> 8) & 0xFF));
        }

        public void StartSending() {
            StartCoroutine(SendToWatson(ResCallback));
        }
        
        void ResCallback(string data) {
            Debug.Log("here");
            Debug.Log($"Transcript: {data}");
            string[] splitTranscript = data.Split(' ');
            ItemDB itemDB = new ItemDB();
            List<string> allItemNames = itemDB.getItems();
            int wordIndex = CheckWords(splitTranscript, allItemNames);
            if(wordIndex > -1) { // Word exists as item name
                Vector2 itemPos = itemDB.getItemPos(allItemNames[wordIndex]);
                queryPopup.SetActive(true);
                queryText.text = $"Query: \"{data}\"";
                Debug.Log(itemPos);
                return;
            }
            Debug.Log("Couldn't find it");
            return;
            // if(!string.IsNullOrEmpty(foundWord)) {
            //     Debug.Log(foundWord);
            //     return;
            // }
            // Debug.Log("couldn't find it");
            // return;

            int CheckWords(string[] splitTranscript, List<string> allItemNames) {
                foreach(string word in splitTranscript) {
                    for(int i=0; i<allItemNames.Count; i++) {
                        if(word.ToLower() == allItemNames[i].ToLower()) {
                            return i;
                        }
                    }
                }
                return -1;
            }
        }

        IEnumerator SendToWatson(System.Action<string> callback = null) {
            string apikey = "HtSmqWZvaE05ZFEC2Gl21lF_P66Ex4EZ6uyiHEAnHjWo";
            var authenticator = new IBM.Cloud.SDK.Authentication.Iam.IamAuthenticator(
                apikey: $"{apikey}"
            );
            while (!authenticator.CanAuthenticate()) yield return null;

            var speechToText = new SpeechToTextService(authenticator);
            speechToText.SetServiceUrl("https://api.eu-gb.speech-to-text.watson.cloud.ibm.com/instances/695d0518-de2d-40a0-a618-aae9dca000e6");

            SpeechRecognitionResults recognizeResponse = null;
            speechToText.Recognize(
                callback: (DetailedResponse<SpeechRecognitionResults> response, IBMError error) =>
                {
                    Log.Debug("SpeechToTextServiceV1", "Recognize result: {0}", response.Response);
                    recognizeResponse = response.Result;
                },
                audio: new MemoryStream(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, "Recording.wav"))),
                contentType: "audio/wav",
                wordAlternativesThreshold: 0.9f,
                keywords: new List<string>()
                {
                    "where",
                    "medicine"
                },
                keywordsThreshold: 0.5f
            );

            while (recognizeResponse == null)
            {
                yield return null;
            }

            var parsedData = JObject.Parse(JsonConvert.SerializeObject(recognizeResponse));

            if(callback != null) callback((string)parsedData["results"][0]["alternatives"][0]["transcript"]);
            // callback?.Invoke(etc..);

            // Debug.Log(parsedData["results"][0]["alternatives"][0]["transcript"]);
        }
    }
}
