using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MemoryPalace.Util {
    public class Request : MonoBehaviour {
        // public static IEnumerator PostRequest(string url, string body) { // Create New
        //     UnityWebRequest www = UnityWebRequest.Post()
        // }

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
        
        public static IEnumerator GetRequest(string url) {
            UnityWebRequest req = UnityWebRequest.Get(url);
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success) {
                Debug.Log(req.error);
            }
            else {
                // Show results as text
                Debug.Log(req.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = req.downloadHandler.data;

                yield return req.downloadHandler.text;
            }
        }
    }
}
