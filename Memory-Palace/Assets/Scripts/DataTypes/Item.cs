using UnityEngine;

namespace MemoryPalace.DataTypes {
    public class Item {
        string itemName {get; set;}
        string itemDescription {get; set;}
        Vector2 itemLocation {get; set;}

        public Item(string _name, float _x = 0, float _y = 0, string _description = "") {
            this.itemName = _name;
            this.itemDescription = _description;
            this.itemLocation = new Vector2(_x, _y);
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
    }
}
