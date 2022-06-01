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
        Vector2 position, dimensions;
        float width = 1.0f, height = 1.0f; // Incremented in 0.5 metre intervals, assigned in case not oversigned
        string roomName, description;
        GameObject[] bluetoothDevices = new GameObject[5];
        int[] doors = new int[4]; // One for each direction
        Transform parent;
        Vector2 minSize = new Vector2(0.5f, 0.5f);
        Vector2 maxSize = new Vector2(10f, 10f);
        // GameObject draggable;

        /* SubObjects of the room */
        DimensionsFormButtons dimForm;
        RectTransform rect, innerSpace;
        Button contextMenuButton, deleteButton;
        GameObject closeContextMenuButton, contextMenu;
        Transform widthText, heightText;

        void Awake() {
            // cache required variables once
            this.dimForm = transform.Find("Form-Dimensions").GetComponent<DimensionsFormButtons>();
            this.rect = gameObject.GetComponent<RectTransform>();
            this.innerSpace = transform.GetChild(0).GetComponent<RectTransform>();
            this.contextMenuButton = transform.Find("OpenContextMenuButton").GetComponent<Button>();
            this.closeContextMenuButton = transform.Find("CloseContextMenuButton").gameObject;
            this.contextMenu = transform.Find("RoomContextMenu").gameObject;
            this.deleteButton = transform.Find("DeleteButton").GetComponent<Button>();
            this.widthText = transform.Find("WidthText");
            this.heightText = transform.Find("HeightText");
        }

        public void SetParent(Transform parent) {
            this.parent = parent;
            transform.parent = parent;
        }

        void ResizeUI() {
            // Transform values back into pixel widths, with 40 for walls either side
            int pixelWidth = (int)(this.width * 100) + 40;
            int pixelHeight = (int)(this.height * 100) + 40;
            SetUISize(new Vector2(pixelWidth, pixelHeight));
        }

        public void SetUISize(Vector2 dimensions) {
            rect.localScale = new Vector3(1,1,1);
            this.dimensions = dimensions;
            this.rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dimensions.x);
            this.rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimensions.y);

            this.innerSpace.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dimensions.x-20);
            this.innerSpace.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dimensions.y-20);
            UpdateRoomDimensionsText();
            UpdateDeleteButtonPosition();
        }

        public void SetupRoomText(string _name = null, string _description = null) {
            if(_name!=null) this.name = _name;
            gameObject.name = _name;
            if(_description!=null) this.description = _description;
        }

        public void SetupContextMenuButton() {
            contextMenuButton.onClick.AddListener(() => {
                closeContextMenuButton.SetActive(true);
                contextMenu.SetActive(true);
            });
        }

        public void UpdatePosition(Vector2 pos) {
            this.position = pos;
            transform.position = pos;
        }


        public void UpdateSize(float _width, float _height) { // Size refers to real world units in metres
            this.height = _height;
            this.width = _width;
        }

        public void UpdateValues() {
            // Sets the widths to the new values
            // the "out" keywords passes a reference to the variables, so they can be set externally as if they're here
            // using "out" rather than "ref" since it requires assignment
            this.dimForm.UpdateValues(out width, out height);
            this.UpdateSize((this.width), (this.height));
            ResizeUI();
        }

        public void UpdateRoomDimensionsText() {
            // maybe add high contrast background?
            if(this.position.x >= 0) { // text to left
                heightText.localPosition = new Vector3(-(this.dimensions.x*0.5f)-40, 0, 0);
                if(this.dimensions.y < 400) heightText.localPosition += new Vector3(0,(this.dimensions.y/400)*120,0);
                heightText.localRotation = Quaternion.Euler(0,0,90);
            } else { // text to right
                heightText.localPosition = new Vector3((this.dimensions.x*0.5f)+40, -120.0f, 0);
                if(this.dimensions.y < 400) heightText.localPosition += new Vector3(0,(this.dimensions.y/400)*-120,0);
                heightText.localRotation = Quaternion.Euler(0,0,270);
            }
            heightText.GetComponent<Text>().text = $"Height: {this.height}m";

            if(this.position.y >= 0) { // text below
                widthText.localPosition = new Vector3(120.0f, -(this.dimensions.y*0.6f)-40, 0);
            } else { // text above
                widthText.localPosition = new Vector3(0, (this.dimensions.y*0.6f)+40, 0);
            }
            widthText.GetComponent<Text>().text = $"Width: {this.width}m";
        }

        public void UpdateDeleteButtonPosition() {
            this.deleteButton.transform.localPosition = new Vector3(0-(dimensions.x*0.5f),0-(dimensions.y*0.5f),0);
        }

        public override string ToString() {
            return $"ID:{id}\nName: {name}\nDesc: {description}\nDimensions: {width}w x {height}h";
        }
    }
}