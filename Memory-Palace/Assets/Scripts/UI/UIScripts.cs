using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using MemoryPalace.Util;

namespace MemoryPalace.UI {
    public class UIScripts : MonoBehaviour {
        Request req;

        void Start() {
            req = GameObject.Find("Utilities").GetComponent<Request>();
        }

        public void LoadScene(string sceneName) {
            Debug.Log(sceneName);
            SceneManager.LoadScene(sceneName);
        }

        public void Request() {
            req.PostRequest("http://127.0.0.1:5000/api/v1/postResponse", "{\"Name\":\"Kitchen\"}", ResCallback);
            // req.GetRequest("http://127.0.0.1:5000/api/v1/textResponse", ResCallback);

            void ResCallback(string data) {
                Debug.Log($"UIScripts 26: {data}");
                return;
            }
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

