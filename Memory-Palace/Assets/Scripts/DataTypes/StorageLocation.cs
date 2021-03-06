using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemoryPalace.DataTypes;

namespace MemoryPalace.DataTypes {
    public class StorageLocation {
        int id;
        string name;
        List<Item> items;
        public StorageLocation(string _name, List<Item> _items, int _id = -1) {
            this.id = _id;
            this.name = _name;
            this.items = _items;
        }

        public int GetID() {
            return this.id;
        }

        public string GetName() {
            return this.name;
        }

        public void SetName(string _name) {
            this.name = _name;
        }

        public List<Item> GetItems() {
            return this.items;
        }

        public Item GetItemAt(int index) {
            return this.items[index];
        }

        public void AddItem(Item item) {
            this.items.Add(item);
        }

        public void SetItemAt(int index, Item item) {
            this.items[index] = item;
        }

        public void RemoveItemAt(int index) {
            items.RemoveAt(index);
        }
    }
}
