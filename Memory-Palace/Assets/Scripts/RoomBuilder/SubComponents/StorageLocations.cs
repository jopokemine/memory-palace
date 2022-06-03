using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using MemoryPalace.DataTypes;
using ItemDB = DataBank.Items;
using StorageDB = DataBank.StorageLocations;

namespace MemoryPalace.RoomBuilder {
    public class StorageLocations : MonoBehaviour {
        ItemDB itemDB;
        StorageDB storageDB;
        // Dummy Values
        int locationIndex = 0;
        int itemIndex = 0;
        List<StorageLocation> locations = new List<StorageLocation>();
        List<int> storageIDs;
        List<int> itemIDs;
        List<int> deletedItemIDs; // This is to prevent errors
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
            storageDB = new StorageDB();
            itemDB = new ItemDB();
            // Put it in the middle, irrespective of where the room itself is
            gameObject.transform.position = gameObject.transform.parent.parent.position;
            // Get data and put it in relevant Lists
            GetDataFromDatabase();
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

            // Hide everything now it's been cached and can be referred to later
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
                locations[locationIndex] = new StorageLocation(storageName.text, locations[locationIndex].GetItems(), locations[locationIndex].GetID());
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
            storageIDs = new List<int>();
            itemIDs = new List<int>();
            List<string[]> roomItems = itemDB.getItemsInRoom("Test Room");
            foreach (string[] item in roomItems) {
                int found = -1;
                for(int i=0; i<locations.Count; i++) {
                    if (locations[i].GetName() == item[1]) {
                        found = i;
                        break;
                    }
                }
                if(found == -1) {
                    locations.Add(new StorageLocation(item[1], new List<Item>(), int.Parse(item[3])));
                    locations[locations.Count-1].AddItem(new Item(item[0], int.Parse(item[2])));
                } else {
                    locations[found].AddItem(new Item(item[0]));
                }
                storageIDs.Add(int.Parse(item[3]));
                itemIDs.Add(int.Parse(item[2]));
                // item[0] is the item name, item[2] is item id
                // item[1] is the storage location name, item[3] is storage id
            }
        }

        public void UpdateDatbase() {
            // public void addStorageLocation (string storage_name, string x, string y, string room_id)
            // public void updateStorageLocation(string id, string new_name)
            // public void updateItemName(string id, string new_name)
            // public void addItem (string item_name, string x, string y, string storage_id)
            // Filter out all of the deleted entries
            foreach(int id in storageIDs) {
                // StorageLocation loc = locations.Where(x => x.GetID() == id) || null;
                // if(loc != null)
            }
            // Update or add all the non deleted ones
            foreach(StorageLocation location in locations) {
                if(!storageIDs.Contains(location.GetID())) {
                    storageDB.addStorageLocation(location.GetName(), "0", "0", "0");
                } else {
                    storageDB.updateStorageLocation(location.GetID().ToString(), location.GetName());
                }
                foreach(Item item in location.GetItems()) {
                    if(!itemIDs.Contains(item.GetID())) {
                        itemDB.addItem(item.GetName(), "0", "0", location.GetID().ToString());
                    } else {
                        itemDB.updateItemName(item.GetID().ToString(), item.GetName());
                    }
                }
            }
            // tick button, slap that bad boy in!
        }

        public void DiscardChanges() {
            // cross button, who even CARES about what i just spend time on!
        }
    }
}
