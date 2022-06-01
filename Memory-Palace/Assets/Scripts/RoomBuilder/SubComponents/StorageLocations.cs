using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using MemoryPalace.DataTypes;

namespace MemoryPalace.RoomBuilder {
    public class StorageLocations : MonoBehaviour {
        // Dummy Values
        List<string> storageLocations = new List<string> {"cupboard", "bench", "bag", "tray"};
        List<Item> items2 = new List<Item>();
        // Intermediary so we can get the itemName and itemDescription
        GameObject itemInformation;
        InputField itemName;
        InputField itemDescription;

        Dropdown storageLocationDropdown;
        GameObject itemDropdownBase;
        Dropdown itemDropdown;

        void Awake() {
            // cache the values
            storageLocationDropdown = GameObject.Find("StorageLocationDropdown").GetComponent<Dropdown>();
            itemDropdownBase = GameObject.Find("ItemDropdown");
            itemDropdown = itemDropdownBase.GetComponent<Dropdown>();

            itemInformation = GameObject.Find("ItemInformation");

            itemName = itemInformation.transform.Find("ItemName").GetChild(1).GetComponent<InputField>();
            itemDescription = itemInformation.transform.Find("ItemDescription").GetChild(1).GetComponent<InputField>();

            // Initialise dummy items
            items2.Add(new Item("Keys"));
            items2.Add(new Item("Medicine"));
            items2.Add(new Item("Wallet"));
            items2.Add(new Item("Glasses"));
        }

        void Start() {
            // replace this with a database query when neccesary
            PopulateStorageLocationsDropdown(storageLocations);
        }

        public void StorageLocationDropdownValueChange(Dropdown change) {
            // Debug.Log(change.value);
            if(change.value == 0) { // Selected "Please Select"
                // essentially want to clear data and make uninteractable
                itemDropdown.ClearOptions();
                UninteractiveItemDropdown();
                UninteractiveItemInformation();
            }
            else if(change.value == (change.options.Count -1)) { // Selected "+ New"
                Debug.Log("+ New");
            } else { // Selected an actual location
                // probably do change.value -1 so it lines up with array indexes
                Debug.Log(change.value);
                PopulateItemDropdown(items2);
                InteractiveItemDropdown();
            }
        }

        public void ItemDropdownValueChange(Dropdown change) {
            // Debug.Log(change.value);
            if(change.value == 0) { // Selected "Please Select"
                UninteractiveItemInformation();
            }
            else if(change.value == (change.options.Count - 1)) { // Selected "+ New"
                InteractiveItemInformation();
                PopulateItemInformation(items2[change.value-1]);
            } else { // Selected an actual item
                //
            }
        }

        public void InteractiveItemDropdown() {
            itemDropdownBase.SetActive(true);
        }

        public void UninteractiveItemDropdown() {
            itemDropdownBase.SetActive(false);
        }

        public void InteractiveItemInformation() {
            itemInformation.gameObject.SetActive(true);
        }

        public void UninteractiveItemInformation() {
            itemInformation.gameObject.SetActive(false);
        }

        public void PopulateStorageLocationsDropdown(List<string> _storageLocations) {
            _storageLocations.Insert(0, "Please Select"); // Acts as a placeholder
            _storageLocations.Add("+ New"); // Acts as a easier way to flag up that this is a new item
            // removes all previous items inside
            storageLocationDropdown.ClearOptions();
            storageLocationDropdown.AddOptions(_storageLocations);

            // List<string> itemNames = _items.Select(x => x.GetName()).ToList();
            // itemNames.Insert(0, "Please Select");
            // itemNames.Add("+ New");
            // itemDropdown.ClearOptions();
            // itemDropdown.AddOptions(itemNames);
        }

        void PopulateItemDropdown(List<Item> _items) {
            List<string> itemNames = _items.Select(x => x.GetName()).ToList();
            itemNames.Insert(0, "Please Select");
            itemNames.Add("+ New");
            itemDropdown.ClearOptions();
            itemDropdown.AddOptions(itemNames);
        }

        void PopulateItemInformation(Item item) {
            itemName.text = item.GetName();
            itemDescription.text = item.GetDescription();
        }

        public void ClearItemInputs() {
            itemName.text = "";
            itemDescription.text = "";
        }
    }
}
