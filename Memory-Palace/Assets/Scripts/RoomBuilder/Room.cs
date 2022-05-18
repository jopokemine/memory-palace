using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MemoryPalace.RoomBuilder {
    public class Room : MonoBehaviour {
        /* Internal Variables */
        public int id {get; set;}
        // Min 0.5 metres (100px), max 10 metres(2000px) <-- larger than screen width
        public float width = 1.0f, height = 1.0f; // Incremented in 0.5 metre intervals, assigned in case not oversigned
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


        public void SetupDimensions(float _height, float _width) {
            this.height = _height;
            this.width = _width;
        }

        public void Resize() {
            Debug.Log("clicked");
        }

        public override string ToString() {
            return $"ID:{id}\nName: {name}\nDesc: {description}\nDimensions: {width}w x {height}h";
        }
    }
}