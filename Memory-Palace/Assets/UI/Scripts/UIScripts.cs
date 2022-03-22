using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace MemoryPalace.UI {
  public class UIScripts : MonoBehaviour {

    public void LoadScene(string sceneName) {
      SceneManager.LoadScene(sceneName);
    }
  }

  [RequireComponent(typeof(AudioSource))]
  public class AudioVisualiser : MonoBehaviour {
    AudioSource audioSource;
    public void Start() { audioSource = GetComponent<AudioSource>(); }

    public void Update() { GetSpectrumAudioSource(); }

    void GetSpectrumAudioSource() {
      //
    }
  }
}
