using UnityEngine;

namespace MemoryPalace.DataTypes {
    public class Item {
        int id;
        string itemName {get; set;}
        string itemDescription {get; set;}
        Vector2 itemLocation {get; set;}

        public Item(string _name, int _id, float _x = 0, float _y = 0, string _description = "") {
            this.id = _id
            this.itemName = _name;
            this.itemDescription = _description;
            this.itemLocation = new Vector2(_x, _y);
        }

        public Item(Item item, string _name = null, string _description = null) {
            this.id = _id;
            this.itemName = item.GetName();
            this.itemDescription = item.GetDescription();
            this.itemLocation = item.GetLocation();

            if(!string.IsNullOrEmpty(_name)) this.itemName = _name;
            if(!string.IsNullOrEmpty(_description)) this.itemDescription = _description;
        }

        public int GetID() {
            return this.id;
        }

        public string GetName() {
            return this.itemName;
        }

        public string GetDescription() {
            return this.itemDescription;
        }

        public Vector2 GetLocation() {
            return this.itemLocation;
        }

        public override string ToString() {
            return $"Name: {this.itemName}\nDescription: {this.itemDescription}\nLocation: {this.itemLocation}";
        }
    }
}
