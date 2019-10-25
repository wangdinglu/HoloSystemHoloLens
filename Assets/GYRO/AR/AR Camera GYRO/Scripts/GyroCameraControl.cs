using UnityEngine;
using UnityEngine.Events;

namespace MixOne {
    [AddComponentMenu ("AR/GyroCameraControl")]
    public class GyroCameraControl : MonoBehaviour 
    {
	    //private Gyroscope gyro;
	    //private static bool gyroSupported;
	    private Quaternion rotationFix = new Quaternion(0f, 0f, 1f, 0f);

	    public Transform gyroCamera;

	    [Tooltip("If it's 'true' then Gyro's 'Y' Rotation is resetted on Scene Closing or Reloading.")]
	    public bool IsGyroDisabledOnDestroy = false;
	    public UnityEvent OnGyroIsNotSupported;

	    void Start () 
	    {
		    //gyroSupported = SystemInfo.supportsGyroscope;

		    //gyroCamera.parent.transform.rotation = Quaternion.Euler (90f, 180f, 0f);

		    //if (gyroSupported) 
		    //{
			   // gyro = Input.gyro;
			   // gyro.enabled = true;
		    //}
		    //else
		    //{
			   // //Your Logic
			   // OnGyroIsNotSupported.Invoke();
		    //}
	    }

	    void Update () 
	    {
		    //if (gyroSupported)
		    //{
			   // gyroCamera.localRotation = gyro.attitude * rotationFix;
		    //}
	    }

	    private void OnDestroy()
	    {
		    //if (gyro != null && IsGyroDisabledOnDestroy)
		    //{
			   // gyro.enabled = false;
			
			   // //print("Reset Gyro!");
		    //} 
	    }

        public Vector3 GetGyroStatus()
        {
            return gyroCamera.transform.eulerAngles;
        }

        public void DisableRotation()
        {
            //gyroSupported = false;
        }

        public void EnableRotation()
        {
            //gyroSupported = true;
        }

        public void SetGyroRotation(Vector3 rotation)
        {
            gyroCamera.transform.eulerAngles = rotation;
        }
    }
}
