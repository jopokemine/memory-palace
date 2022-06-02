using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    public class ArrowRotation : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
             transform.rotation = Input.gyro.attitude;
        }
    }
}
