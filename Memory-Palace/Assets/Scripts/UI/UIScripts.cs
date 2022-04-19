using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using MemoryPalace;

namespace MemoryPalace.UI {
    public class UIScripts : MonoBehaviour {

        public void LoadScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }

        public void Request() {
            StartCoroutine(Util.Request.GetRequest("http://192.168.1.199:5000/api/v1/textResponse"));
        }
    }

    [RequireComponent(typeof (AudioSource))]
    public class AudioVisualiser : MonoBehaviour {
        AudioSource audioSource;
        public void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        public void Update() {
            GetSpectrumAudioSource();
        }

        void GetSpectrumAudioSource() {
            //
        }
    }
}

