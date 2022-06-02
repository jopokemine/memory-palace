using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace.Util {
    public static class CoordinateGeometry {
        public static Vector2 MidPointVector2(Vector2 p1, Vector2 p2) {
            return new Vector2(((p1.x + p2.x) * 0.5f), ((p1.y + p2.y) * 0.5f));
        }

        public static Vector2 DistanceVector2(Vector2 p1, Vector2 p2) {
            Vector2 difference = new Vector2(1*(p1.x-p2.x), 1*(p1.y-p2.y));
            return new Vector2(Mathf.Abs(difference.x), Mathf.Abs(difference.y));
        }

        public static bool BoxesIntersect(Vector2 midp1, Vector2 dim1, Vector2 midp2, Vector2 dim2) {
            // bottom left, top left, bottom right, top right
            Vector2[] minimaxi1 = CalculateBoxMinimaMaxima(midp1, DistanceMidBoxToEdge(midp1, dim1));
            Vector2[] minimaxi2 = CalculateBoxMinimaMaxima(midp2, DistanceMidBoxToEdge(midp2, dim2));

            // check them against each other?
            // if(minimaxi1[0] < minimaxi2[3])
            // Create function that takes two vector 2's and a value (0,1,2,3) that evaluates corners?

            return false;
        }

        public static Vector2 DistanceMidBoxToEdge(Vector2 midpoint, Vector2 dimensions) {
            return midpoint + (dimensions * 0.5f);
        }

        public static Vector2[] CalculateBoxMinimaMaxima(Vector2 midpoint, Vector2 distance) {
            // 0,0 in ui is bottom left
            Vector2[] minimaxi =  new Vector2[4];
            // This is loop unrolling, no other reason
            // bottom left
            minimaxi[0] = midpoint - distance;
            // top left
            minimaxi[1] = new Vector2(midpoint.x - distance.x, midpoint.y + distance.y);
            // bottom right
            minimaxi[2] = new Vector2(midpoint.x + distance.x, midpoint.y - distance.y);
            // top right
            minimaxi[3] = midpoint + distance;

            return minimaxi;
        }
    }
}
