using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace MemoryPalace.TTS {
    [RequireComponent(typeof (AudioSource))]
    public class AudioRecording : MonoBehaviour {
        AudioSource audioSource;

        void Awake() {
            audioSource = gameObject.GetComponent<AudioSource>();
        }
        public void StartRecording() {
            audioSource.clip = Microphone.Start("", false, 6, 44100);
            StartCoroutine(this.Recording(5f));
        }

        public void StopRecording() {
            StopCoroutine(this.Recording(5f));
            this.FinishRecording();
        }

        IEnumerator Recording(float t) {
            yield return new WaitForSeconds(t);
            FinishRecording();
        }

        public void FinishRecording() {
            Microphone.End("");
            audioSource.Play();
            // byte[] audioData = audioSource.GetOutputData();
            Debug.Log("Finished Recording / Returned TTS");
        }
    }
}
