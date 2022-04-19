using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace.Util {
    public static class CoordinateGeometry {
        public static Vector2 MidPointVector2(Vector2 p1, Vector2 p2) {
            return new Vector2(((p1.x + p2.x) * 0.5f), ((p1.y + p2.y) * 0.5f));
        }

        public static Vector2 DistanceVector2(Vector2 p1, Vector2 p2) {
            Vector2 difference = p1 - p2;
            return new Vector2(difference.x, difference.y);
        }
    }
}
