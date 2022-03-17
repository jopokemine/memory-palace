using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MemoryPalace.UI {
    public class UIScripts : MonoBehaviour {

        public void ClickedCheck() {
            Debug.Log("Clicked");
        }

        public void LoadScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
    }
}

