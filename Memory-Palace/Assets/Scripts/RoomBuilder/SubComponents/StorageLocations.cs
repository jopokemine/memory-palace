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
        int locationIndex = 0;
        int itemIndex = 0;
        List<Item> items1 = new List<Item>();
        List<Item> items2 = new List<Item>();
        List<StorageLocation> locations = new List<StorageLocation>();
        // Intermediary so we can get the itemName and itemDescription
        GameObject storageInformation;
        InputField storageName;
        GameObject itemInformation;
        InputField itemName;
        InputField itemDescription;

        Dropdown storageLocationDropdown;
        GameObject itemDropdownBase;
        Dropdown itemDropdown;

        // Buttons for final accept or cancel
        GameObject acceptButton;
        GameObject cancelButton;

        GameObject itemDeleteButton;
        GameObject itemUpdateButton;
        GameObject itemAddButton;
        GameObject storageUpdateButton;
        GameObject storageDeleteButton;
        GameObject storageAddButton;

        public GameObject errorMessage;
        

        void Awake() {
            // cache the values
            storageLocationDropdown = GameObject.Find("StorageLocationDropdown").GetComponent<Dropdown>();
            itemDropdownBase = GameObject.Find("ItemDropdown");
            itemDropdown = itemDropdownBase.GetComponent<Dropdown>();

            storageInformation = GameObject.Find("StorageInformation");
            itemInformation = GameObject.Find("ItemInformation");

            storageName = storageInformation.transform.Find("StorageName").GetChild(1).GetComponent<InputField>();

            itemName = itemInformation.transform.Find("ItemName").GetChild(1).GetComponent<InputField>();
            itemDescription = itemInformation.transform.Find("ItemDescription").GetChild(1).GetComponent<InputField>();

            acceptButton = GameObject.Find("AcceptButton");
            itemDeleteButton = itemInformation.transform.Find("DeleteButton").gameObject;
            itemUpdateButton = itemInformation.transform.Find("UpdateButton").gameObject;
            itemAddButton = itemInformation.transform.Find("AddButton").gameObject;
            storageDeleteButton = storageInformation.transform.Find("DeleteButton").gameObject;
            storageUpdateButton = storageInformation.transform.Find("UpdateButton").gameObject;
            storageAddButton = storageInformation.transform.Find("AddButton").gameObject;
            cancelButton = GameObject.Find("CancelButton");

            // Initialise dummy items
            items1.Add(new Item("Headphones"));
            items1.Add(new Item("Spoon"));
            items2.Add(new Item("Keys"));
            items2.Add(new Item("Medicine"));
            items2.Add(new Item("Wallet"));
            items2.Add(new Item("Glasses"));
            locations.Add(new StorageLocation("Kitchen", items1));
            locations.Add(new StorageLocation("Bathroom", items2));
            UninteractiveGameObject(itemDropdownBase);
            UninteractiveGameObject(itemInformation);
            UninteractiveGameObject(storageInformation);
            UninteractiveGameObject(itemUpdateButton);
            UninteractiveGameObject(storageUpdateButton);
        }

        void Start() {
            // replace this with a database query when neccesary
            PopulateStorageLocationsDropdown(locations);
        }

        public void StorageLocationDropdownValueChange(Dropdown change) {
            if(change.value == 0) { // Selected "Please Select"
                // essentially want to clear data and make uninteractable
                itemDropdown.ClearOptions();
                UninteractiveGameObject(itemDropdownBase);
                UninteractiveGameObject(itemInformation);
                UninteractiveGameObject(storageInformation);
            }
            else if(change.value == (change.options.Count -1)) { // Selected "+ New"
                Debug.Log("+ New");
                ClearStorageInputs();
                InteractiveGameObject(storageAddButton);
                InteractiveGameObject(storageInformation);
                UninteractiveGameObject(storageUpdateButton);
                // AddNewStorageLocation();
            } else { // Selected an actual location
                locationIndex = change.value-1;
                PopulateItemDropdown(locations[locationIndex].GetItems());
                PopulateStorageInformation(locations[locationIndex].GetName());
                InteractiveGameObject(itemDropdownBase);
                InteractiveGameObject(storageInformation);
                InteractiveGameObject(storageUpdateButton);
                UninteractiveGameObject(storageAddButton);
                ClearItemInputs();
            }
            UninteractiveGameObject(itemInformation);
        }

        public void ItemDropdownValueChange(Dropdown change) {
            // Debug.Log(change.value);
            if(change.value == 0) { // Selected "Please Select"
                UninteractiveGameObject(itemInformation);
            }
            else if(change.value == (change.options.Count - 1)) { // Selected "+ New"
                ClearItemInputs();
                InteractiveGameObject(itemInformation);
                InteractiveGameObject(itemAddButton);
                UninteractiveGameObject(itemUpdateButton);
            } else { // Selected an actual item
                itemIndex = change.value-1;
                PopulateItemInformation(locations[locationIndex].GetItemAt(change.value-1));
                InteractiveGameObject(itemInformation);
                InteractiveGameObject(itemUpdateButton);
                UninteractiveGameObject(itemAddButton);
            }
        }

        public void PopulateStorageLocationsDropdown(List<StorageLocation> _storageLocations) {
            List<string> locationNames = _storageLocations.Select(x => x.GetName()).ToList();
            locationNames.Insert(0, "Please Select"); // Acts as a placeholder
            locationNames.Add("+ New"); // Acts as a easier way to flag up that this is a new item
            // removes all previous items inside
            storageLocationDropdown.ClearOptions();
            storageLocationDropdown.AddOptions(locationNames);
        }

        void PopulateStorageInformation(string _name) {
            storageName.text = _name;
        }

        void ClearStorageInputs() { // This could be achieved with the above function and an empty string, but this is easier to think about
            storageName.text = "";
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

        public void InteractiveGameObject(GameObject obj) {
            obj.SetActive(true);
        }

        public void UninteractiveGameObject(GameObject obj) {
            obj.SetActive(false);
        }

        void DisableErrorMessage() {
            errorMessage.SetActive(false);
        }

        public void AddNewStorageLocation() {
            if(!string.IsNullOrEmpty(storageName.text)) {
                locations.Add(new StorageLocation(storageName.text, new List<Item>()));
                PopulateStorageLocationsDropdown(locations);
                storageLocationDropdown.value = locations.Count;
                return;
            }
            errorMessage.SetActive(true);
            Invoke("DisableErrorMessage", 3.5f);
        }

        public void UpdateStorageLocation() {
            if(!string.IsNullOrEmpty(storageName.text)) {
                locations[locationIndex] = new StorageLocation(storageName.text, locations[locationIndex].GetItems());
                PopulateStorageLocationsDropdown(locations);
                storageLocationDropdown.value = ++locationIndex;
                return;
            }

            errorMessage.SetActive(true);
            Invoke("DisableErrorMessage", 3.5f);
        }

        public void RemoveStorageLocation() {
            locations.RemoveAt(locationIndex);
            storageLocationDropdown.value = 0;
            PopulateStorageLocationsDropdown(locations);
        }

        public void AddNewItem() {
            if(!string.IsNullOrEmpty(itemName.text)) {
                locations[locationIndex].AddItem(new Item(itemName.text));
                PopulateItemDropdown(locations[locationIndex].GetItems());
                itemDropdown.value = locations[locationIndex].GetItems().Count;
                return;
            }

            errorMessage.SetActive(true);
            Invoke("DisableErrorMessage", 3.5f);
        }

        public void UpdateItem() {
            if(!string.IsNullOrEmpty(itemName.text)) {
                locations[locationIndex].SetItemAt(itemIndex, new Item(locations[locationIndex].GetItemAt(itemIndex), itemName.text));
                PopulateItemDropdown(locations[locationIndex].GetItems());
                itemDropdown.value = ++itemIndex;
                return;
            }

            errorMessage.SetActive(true);
            Invoke("DisableErrorMessage", 3.5f);
        }

        public void RemoveItem() {
            locations[locationIndex].RemoveItemAt(itemIndex);
            itemDropdown.value = 0;
            PopulateItemDropdown(locations[locationIndex].GetItems());
        }

        void GetDataFromDatabase() {
            // Get the actual data to start from lmao
        }

        public void MoveValuesToDatabase() {
            // tick button, slap that bad boy in!
        }

        public void DiscardChanges() {
            // cross button, who even CARES about what i just spend time on!
        }
    }
}
