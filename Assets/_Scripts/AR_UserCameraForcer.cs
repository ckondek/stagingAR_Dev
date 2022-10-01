using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AR_UserCameraForcer : MonoBehaviour
{
    public ARCameraManager arCam;
    public ARSession arSession;
    
    // Start is called before the first frame update
    void Start()
    {
        arCam.requestedFacingDirection = CameraFacingDirection.User;
        arCam.requestedLightEstimation = LightEstimation.MainLightDirection;
        arSession.requestedTrackingMode = TrackingMode.RotationOnly;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
