using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace.Testing
{
    public class QuickTests : MonoBehaviour {
        public GameObject contextMenuPrefab;
        public static event System.Action OnMouseDownCustom;
        // Start is called before the first frame update
        void Start() {
            // Debug.Log((655-(655%100))/100);
        }

        public void ClickedRoom() {
            Debug.Log("Clicked ROom");
        }

        public void SubscribeFunction() {
            OnMouseDownCustom += FunctionToAdd;
        }

        public void UnsubscribeFunction() {
            OnMouseDownCustom -= FunctionToAdd;
        }

        public void TestFunctionality() {
            Debug.Log("Pressed");
            OnMouseDownCustom?.Invoke(); // Same next lines, checks if it's null then invokes the event, parameters go in Invoke brackets
            if(OnMouseDownCustom != null) {
                OnMouseDownCustom();
            }
        }

        // private void OnMouseDown() {
        //     Debug.Log("mouse down");
        //     if(OnMouseDownCustom != null) {
        //         OnMouseDownCustom();
        //     }
        // }

        void FunctionToAdd() {
            Debug.Log("Subscribed");
        }
    }
}
