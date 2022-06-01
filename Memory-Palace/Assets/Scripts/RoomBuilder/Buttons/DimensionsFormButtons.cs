using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryPalace.RoomBuilder {
    public class DimensionsFormButtons : MonoBehaviour {
        public InputField widthInput;
        public InputField heightInput;

        public event System.Action OnAcceptNewValues;

        public void ClearData() {
            widthInput.text = "";
            heightInput.text = "";
        }

        public void UpdateValues(out float width, out float height) {
            float newWidth = Mathf.Clamp(float.Parse(widthInput.text), 0.5f, 10.0f);
            newWidth -= (newWidth % 0.5f);
            float newHeight = Mathf.Clamp(float.Parse(heightInput.text), 0.5f, 10.0f);
            newHeight -= (newHeight % 0.5f);
            width = newWidth;
            height = newHeight;
        }
    }
}
