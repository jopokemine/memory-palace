using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace MemoryPalace.TTS {
    [RequireComponent(typeof (AudioSource))]
    public class AudioRecording : MonoBehaviour {
        public void StartRecording() {
            Debug.Log("Start Recording");
            StartCoroutine(this.Recording(5f));
        }

        public IEnumerator Recording(float t) {
            yield return new WaitForSeconds(t);
            EndRecording();
        }

        public void EndRecording() {
            Debug.Log("Finished Recording / Returned TTS");
        }
    }
}
