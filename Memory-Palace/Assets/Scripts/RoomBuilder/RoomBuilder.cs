using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CG = MemoryPalace.Util.CoordinateGeometry;

namespace MemoryPalace.RoomBuilder {
    public class RoomBuilder : MonoBehaviour {
        /* Internal variables */
        #nullable enable
        List<GameObject?> rooms;
        #nullable disable
        GameObject[] rooms1 = new GameObject[15];

        /* External Elements */
        public Transform RoomsBase;
        public GameObject RoomTemplate;
        Transform canvasTransform;
        Vector2 scale;

        Vector2 a;
        Vector2 b;
        // Start is called before the first frame update
        void Start() {
            rooms = new List<GameObject?>();
            foreach(GameObject? obj in rooms) {
                Debug.Log(obj);
            }
            for(int i=0; i<15; i++) {
                rooms1[i] = null;
            }
            canvasTransform = GetComponentInParent<Canvas>().rootCanvas.transform;
            scale = new Vector2(1/canvasTransform.localScale.x, 1/canvasTransform.localScale.y);
            Debug.Log(scale);
        }

        void Update() {
            if(Input.GetMouseButtonDown(0)) {
                a = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            if(Input.GetMouseButtonUp(0)) {
                b = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                NewRoom(CG.MidPointVector2(a,b), CG.DistanceVector2(a,b));
                a = Vector2.zero;
                b = Vector2.zero;
            }
        }

        public void NewRoom(Vector2 midPoint, Vector2 dimensions) {
            // TODO: check through rooms array to see if any empty spots
            // Find index of first empty spot
            // Assign to room for button funcionality setup, so it points to the correct one.

            // TODO: Get form data of name and description and pass to Room entity.


            GameObject newRoom = Instantiate(RoomTemplate);
            SetupNewRoom(newRoom, midPoint, dimensions);
            rooms.Add(newRoom);
        }

        void SetupNewRoom(GameObject room, Vector2 midPoint, Vector2 dimensions) {
            // cache components
            Room roomScript = room.GetComponent<Room>();
            Button resizeButton = room.transform.Find("ResizeButton").GetComponent<Button>();

            // Set place in hierarchy
            room.transform.SetParent(RoomsBase);

            // Set sizing and positioning
            room.transform.position = midPoint;
            RectTransform rect = room.GetComponent<RectTransform>();
            rect.localScale = new Vector3(1,1,1);
            dimensions *= scale;
            dimensions = new Vector2((dimensions.x+50) - (dimensions.x % 100), (dimensions.y+50) - (dimensions.y % 100));
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dimensions.x);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimensions.y);

            // Define room qualities, such as name, id and internal values of width and height
            room.name = $"Room-{rooms.Count}";
            roomScript.id = rooms.Count;
            roomScript.SetupDimensions(dimensions.y/100, dimensions.x/100);

            // Move resize button to bottom right and add functionality
            // SetupRoomButtons(room.transform.Find("ResizeButton").GetComponent<Button>());
            SetupRoomButtons(dimensions, resizeButton);
            // TODO: move to bottom right
            // room.transform.Find("ResizeButton").GetComponent<Button>().onClick.AddListener(() => DeleteRoom(rooms.Count));
        }

        void SetupRoomButtons(Vector2 dimensions, Button resizeButton) {
            resizeButton.transform.position = new Vector3(dimensions.x/scale.x,dimensions.y/scale.y,0);
        }

        public void DeleteRoom(int index) {
            Destroy(rooms[index]);
            rooms.RemoveAt(index);
        }
    }
}

