using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace.Tracking
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
            float rotationTime = 180f;
		    transform.Rotate(Vector3.right * (rotationTime * Time.deltaTime));
            // transform.Rotate(45f, 45f, 0f, Space.Self);
        }
    }
}
