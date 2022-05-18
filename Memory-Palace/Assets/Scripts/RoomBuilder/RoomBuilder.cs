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
            Invoke("CanCreateDragTrue", 1.5f);
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
            Button resizeButton = room.transform.Find("ResizeButton").GetComponent<Button>();
            Button deleteButton = room.transform.Find("DeleteButton").GetComponent<Button>();
            RectTransform rect = room.GetComponent<RectTransform>();

            // Set place in hierarchy
            room.transform.SetParent(roomsBase);

            // Set sizing and positioning
            room.transform.position = midPoint;
            // These two lines are to undo the automatic scaling of the UI from the canvas, which makes the resizeButton huge and the room tiny
            // This happens because parent transform modifications cascade down the object hierarchy
            rect.localScale = new Vector3(1,1,1);
            dimensions *= scale;
            // Set minimum Room size, that being 100 for 0.5metre room
            if(dimensions.x < 100) {
                dimensions.x = 100;
            }
            if(dimensions.y < 100) {
                dimensions.y = 100;
            }
            // dimensions +50 so we have 25px wall size
            dimensions = new Vector2((dimensions.x+50) - (dimensions.x % 100), (dimensions.y+50) - (dimensions.y % 100));
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dimensions.x);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimensions.y);

            // Define room qualities, such as name, id and internal values of width and height
            room.name = $"Room-{index}";
            roomScript.id = index;
            roomScript.name = roomDataName.textComponent.text;
            roomScript.description = roomDataDescription.textComponent.text;
            ClearRoomInfoForm();
            roomScript.SetupDimensions(dimensions.y/100, dimensions.x/100);

            // Move buttons to correct locations and add functionality
            SetupRoomButtons(dimensions, resizeButton, deleteButton, index);
        }

        void SetupRoomButtons(Vector2 dimensions, Button resizeButton, Button deleteButton, int index) {
            // Move rezize button to bottom right and delete button to bottom left
            resizeButton.transform.localPosition += new Vector3(dimensions.x * 0.5f, -(dimensions.y * 0.5f), 0);
            deleteButton.transform.localPosition += new Vector3(-(dimensions.x * 0.5f), -(dimensions.y * 0.5f), 0);
            // Setup Button functionality so they're pointed at the correct GameObject
            deleteButton.onClick.AddListener(() => DeleteRoom(index));
            // TODO: Setup Resize Button, perhaps give UI when clicking on room to specify size numerically?
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

