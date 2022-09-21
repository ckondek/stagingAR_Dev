using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class MQTT_PositionPublisher : MonoBehaviour
{
    private Vector3 camPosition;
    private Vector3 positionRelativeToImageMarker;
    private float distance;

    private MQTT_Multiplayer mqttMP;
    public AR_ProvideTrackingData trackingData;
    public GameObject arCameraGameObject;
    
    private UI_TextUpdater debugText;
    public void Start()
    {
        mqttMP = FindObjectOfType<MQTT_Multiplayer>();
        debugText = FindObjectOfType<UI_TextUpdater>();
        StartCoroutine("UpdateEvery100MS");
    }

    public Vector3 GetPositionReleativeToImageMarker()
    {
        Vector3 positionRelative = new Vector3();
        ////hier muss diese Berechnung rein. 
        Vector3 imageTargetPosition = trackingData.GetPositionFromImageTracker();
        positionRelative = imageTargetPosition - arCameraGameObject.transform.position;
        //positionRelative = positionRelative * -1;
        /*
        positionRelative = arCameraGameObject.transform.InverseTransformPoint(trackingData.GetPositionFromImageTracker());
        distance = Vector3.Distance(arCameraGameObject.transform.position,
            trackingData.GetPositionFromImageTracker());
        */
        return positionRelative;
    }
    
    
    
    private IEnumerator UpdateEvery100MS()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            camPosition = arCameraGameObject.transform.position;
            positionRelativeToImageMarker = GetPositionReleativeToImageMarker();
            Debug.DrawLine(camPosition,trackingData.GetPositionFromImageTracker(),Color.black);
            debugText.UpdateText("Position Relative to Marker x: " 
                                 + positionRelativeToImageMarker.x 
                                 + "\n,y: " + positionRelativeToImageMarker.y+
                                 "\n,z: " + positionRelativeToImageMarker.z + 
                                 "\n Pos Cam:" + camPosition +"\n Dis: " + distance);
            mqttMP.UpdateMyData(camPosition,positionRelativeToImageMarker);
        }
        
    }
}
