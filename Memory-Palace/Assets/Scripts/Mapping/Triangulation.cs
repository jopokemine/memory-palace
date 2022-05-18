using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MemoryPalace.Util;
using MemoryPalace.Bluetooth;

namespace MemoryPalace.Triangulation
{
    public class Triangulation : MonoBehaviour
    {
        BluetoothDevice[] GetStrongestThreeDevices(BluetoothDevice[] bluetoothDevices)
        {
            return (from i in bluetoothDevices
                    orderby i.GetRssi() descending
                    select i).Take(3);
        }

        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        public float[] TriangulateUserPos(BluetoothDevice[] bluetoothDevices)
        {
            if (bluetoothDevices.Length == 1)
            {
                return new float[0.0, 0.0];  // TODO: Figure out what this should return if only one BT device
            }
            else if (bluetoothDevices.Length == 2)
            {
                return new float[0, 0];  // TODO: Figure out how to find the two points the user could be in, and return
                // Or just return same as above
            }
            else
            {
                BluetoothDevice[] devices = GetStrongestThreeDevices(bluetoothDevices);
                BluetoothDevice blue1 = devices[0];
                BluetoothDevice blue2 = devices[1];
                BluetoothDevice blue3 = devices[2];

                float x1 = -2 * blue1.GetX();
                float y1 = -2 * blue1.GetY();
                float ans1 = (Math.Pow(blue1.GetDist(), 2)) - (Math.Pow(blue1.GetX() * -1, 2)) - (Math.Pow(blue1.GetY() * -1, 2));

                float x2 = -2 * blue2.GetX();
                float y2 = -2 * blue2.GetY();
                float ans2 = (Math.Pow(blue2.GetDist(), 2)) - (Math.Pow(blue2.GetX() * -1, 2)) - (Math.Pow(blue2.GetY() * -1, 2));

                float x3 = -2 * blue3.GetX();
                float y3 = -2 * blue3.GetY();
                float ans3 = (Math.Pow(blue3.GetDist(), 2)) - (Math.Pow(blue3.GetX() * -1, 2)) - (Math.Pow(blue3.GetY() * -1, 2));

                x2 = x1 - x2;
                y2 = y1 - y2;
                ans2 = ans1 - ans2;

                x3 = x1 - x3;
                y3 = y1 - y3;
                ans3 = ans1 - ans3;

                float deter = (x2 * y3) - (y2 * x3);
                float invFrac = 1 / deter;

                Swap<float>(x2, y3);

                x3 *= -1;
                y2 *= -1;

                float userX = (x2 * ans2) + (x3 * ans3);
                float userY = (y2 * ans2) + (y3 * ans3);
                userX *= invFrac;
                userY *= invFrac;

                return new float[userX, userY];
            }
        }
    }
}
