using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MemoryPalace.RoomBuilder {
    public class Room : MonoBehaviour, IDragHandler {
        /* Internal Variables */
        public int id {get; set;}
        // Min 0.5 metres, max 10 metres
        public float width = 1.0f, height = 1.0f; // Incremented in 0.5 metre intervals
        public string name, description;
        GameObject[] bluetoothDevices = new GameObject[5];
        List<GameObject> doors = new List<GameObject>();
        GameObject parent;
        GameObject draggable;

        Vector2 minSize = new Vector2(0.5f, 0.5f);
        Vector2 maxSize = new Vector2(10f, 10f);

        private RectTransform rectTransform;
        private Vector2 currentPointerPosition;
        private Vector2 previousPointerPosision;

        void Awake() {
            // Debug.Log($"23: {transform.parent.GetComponent<RectTransform>()}");
            // rectTransform = transform.parent.GetComponent<RectTransform>();
        }

        public void SetupDimensions(float _height, float _width) {
            this.height = _height;
            this.width = _width;
        }

        public void OnDrag(PointerEventData data) {
            // if(rectTransform == null) return;

            // Vector2 sizeDelta = rectTransform.sizeDelta;

            // RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out currentPointerPosition);
            // Vector2 resizeValue = currentPointerPosition - previousPointerPosision;

            // sizeDelta += new Vector2(resizeValue.x, -resizeValue.y);
            // sizeDelta = new Vector2 (
            //     Mathf.Clamp(sizeDelta.x, minSize.x, maxSize.x),
            //     Mathf.Clamp(sizeDelta.y, minSize.y, maxSize.y)
            // );

            // rectTransform.sizeDelta = sizeDelta;
            // previousPointerPosision = currentPointerPosition;
        }

        /* External Elements */


        // Start is called before the first frame update
        void Start() {
            // Debug.Log($"{width} + {height}");
            // Debug.Log(draggable);
            // draggable.onClick.AddListener(() => Resize());
            // draggable.onClick.AddListener(Resize);
        }

        // Update is called once per frame
        void Update() {
        }

        public void Resize() {
            Debug.Log("clicked");
        }
    }
}