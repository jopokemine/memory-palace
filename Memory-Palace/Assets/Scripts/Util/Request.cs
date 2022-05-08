using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MemoryPalace.Util {
    public class Request : MonoBehaviour {
        public void GetRequest(string url, Action<string> callback) {
            UnityWebRequest req = UnityWebRequest.Get(url);
            StartCoroutine(ReqCoroutine(req, callback));
        }

        public void PostRequest(string url, string body, Action<string> callback) { // Create New
            UnityWebRequest req = UnityWebRequest.Post(url, body);
            StartCoroutine(ReqCoroutine(req, callback));
        }

        IEnumerator ReqCoroutine(UnityWebRequest req, Action<string> callback = null) {
            // req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();

            if(req.result != UnityWebRequest.Result.Success) {
                Debug.Log(req.error);
            } else {
                if(callback != null) callback(req.downloadHandler.text);
            }
        }

        // public static IEnumerator PutRequest(string url, string body) { // Update Existing

        // }

        public static IEnumerator DeleteRequest(string url, string itemLocation) {
            UnityWebRequest req = UnityWebRequest.Delete($"{url}/{itemLocation}");
            yield return req.SendWebRequest();

            if(req.result != UnityWebRequest.Result.Success) {
                Debug.Log(req.error);
            } else {
                Debug.Log(req.downloadHandler.text);
                byte[] results = req.downloadHandler.data;
            }
        }
    }
}
