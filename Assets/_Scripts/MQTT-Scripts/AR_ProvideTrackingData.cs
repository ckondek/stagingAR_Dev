using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class AR_ProvideTrackingData : MonoBehaviour
{
    /// <summary>
    /// this is just the beginning it now only works with on Image If we want to update more Image tackers we could split same by refernce name
    /// </summary>
    public ARTrackedImageManager arImageManger;

    private Vector3 positionOfTrackedImage;

    private bool imageTrackedOnce;

    public Vector3 GetPositionFromImageTracker()
    {
        if (imageTrackedOnce)
        {
            return positionOfTrackedImage;
        }
        else
        {
            return new Vector3(0.0f, 0.0f, 0.0f);
        }
        
    }

    private void OnEnable()
    {
        arImageManger.trackedImagesChanged += TabletTrackedImage;
    }

    private void TabletTrackedImage(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage image in args.added)
        {
           positionOfTrackedImage = image.gameObject.transform.localPosition;
           imageTrackedOnce = true;
        }
        
        foreach (ARTrackedImage image in args.updated)
        { 
            positionOfTrackedImage = image.gameObject.transform.localPosition; 
        }
    }
    
    private void OnDisable()
    {
        arImageManger.trackedImagesChanged -= TabletTrackedImage;
    }

}
