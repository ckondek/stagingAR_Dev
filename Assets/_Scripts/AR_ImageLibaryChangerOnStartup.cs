using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AR_ImageLibaryChangerOnStartup : MonoBehaviour
{
    public ARTrackedImageManager arTrackedImageManager;

    public XRReferenceImageLibrary library;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        arTrackedImageManager.referenceLibrary = library;
    }
    
    }
   