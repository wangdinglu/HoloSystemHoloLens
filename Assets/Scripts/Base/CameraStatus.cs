using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MixOne
{
    public class CameraStatus : MonoBehaviour
    {
        public enum status
        {
            Hold = 0,
            Move = 1,
            SpatialHold = 2,
            SpatialAlign = 3
        }
    }

}
