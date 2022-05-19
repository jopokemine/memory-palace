using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace {
    public static class DataManipulation {
        public static void Swap<T>(ref T a, ref T b) {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
