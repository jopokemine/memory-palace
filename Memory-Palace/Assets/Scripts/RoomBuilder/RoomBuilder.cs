using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CG = MemoryPalace.Util.CoordinateGeometry;

namespace MemoryPalace.RoomBuilder {
    public class RoomBuilder : MonoBehaviour {
        /* Internal variables */
        GameObject[] rooms = new GameObject[15];
        Transform canvasTransform;
        Vector2 scale;
        Vector2 a;
        Vector2 b;
        bool canCreateDrag;

        /* External Elements */
        public Transform roomsBase;
        public GameObject roomTemplate;
        public InputField roomDataName;
        public InputField roomDataDescription;
        public GameObject dragPrompt;

        // Start is called before the first frame update
        void Start() {
            // Initialise all values to null, so we can easily which have values
            for(int i=0; i<15; i++) {
                rooms[i] = null;
            }
            // Get the canvas, then the scaling to deal with strange transformations of prefab Instantiation
            canvasTransform = GameObject.Find("Canvas").transform;
            scale = new Vector2(1/canvasTransform.localScale.x, 1/canvasTransform.localScale.y);
        }

        void Update() {
            if(canCreateDrag) DragToMakeRoom();
        }

        void DragToMakeRoom() {
            // First point when mouse/touch starts, user drags and then finishing point, when they remove is second point
            if(Input.GetMouseButtonDown(0)) {
                a = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            if(Input.GetMouseButtonUp(0)) {
                b = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                NewRoom(CG.MidPointVector2(a,b), CG.DistanceVector2(a,b));
                a = Vector2.zero;
                b = Vector2.zero;
                dragPrompt.SetActive(false);
                canCreateDrag = false;
            }
        }

        public void EnableDragToMakeRoom() {
            Invoke("CanCreateDragTrue", 0.25f);
        }

        void CanCreateDragTrue() {
            canCreateDrag = true;
        }

        public void NewRoom(Vector2 midPoint, Vector2 dimensions) {
            // Find index of first empty spot
            // Assign to room for button funcionality setup, so it points to the correct one.
            int index = Array.FindIndex(rooms, i => i == null);

            GameObject newRoom = Instantiate(roomTemplate);
            SetupNewRoom(newRoom, midPoint, dimensions, index);
            rooms[index] = newRoom;
        }

        void SetupNewRoom(GameObject room, Vector2 midPoint, Vector2 dimensions, int index) {
            // cache components
            Room roomScript = room.GetComponent<Room>();
            Button deleteButton = room.transform.Find("DeleteButton").GetComponent<Button>();

            // Set place in hierarchy
            roomScript.SetParent(roomsBase);

            // Set sizing and positioning
            // room.transform.position = midPoint;
            roomScript.UpdatePosition(midPoint);
            // This line undoes the automatic scaling of the UI from the canvas, which makes the resizeButton huge and the room tiny. The local scale is set to 1 inside the room
            // This happens because parent transform modifications cascade down the object hierarchy
            dimensions *= scale;
            // Clamp size between minimum (.5m 100px) and maximum (10m 2000px)
            dimensions.x = Mathf.Clamp(dimensions.x, 100, 2000);
            dimensions.y = Mathf.Clamp(dimensions.y, 100, 2000);
            // dimensions +40 so we have 20px wall size
            dimensions = new Vector2((dimensions.x - (dimensions.x % 100)) + 40, (dimensions.y - (dimensions.y % 100)) + 40);
            roomScript.UpdateSize((dimensions.x-40)/100,( dimensions.y-40)/100);
            roomScript.SetUISize(dimensions);

            // Define room qualities, such as name, id
            // BUG: room.name gets flushed when roomScript.name is set
            roomScript.id = index;
            roomScript.SetupRoomText(roomDataName.textComponent.text, roomDataDescription.textComponent.text);
            ClearRoomInfoForm();

            // Move buttons to correct locations and add functionality
            roomScript.UpdateDeleteButtonPosition();
            deleteButton.onClick.AddListener(() => DeleteRoom(index));
            roomScript.SetupContextMenuButton();
        }

        void DeleteRoom(int index) {
            Destroy(rooms[index]);
            rooms[index] = null;
        }

        void ClearRoomInfoForm() {
            roomDataName.text = "";
            roomDataDescription.text = "";
        }
    }
}