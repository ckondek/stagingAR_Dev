using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[Serializable]
public struct FaceState
{
    public Material faceMaterial;
    public GameObject faceGameObject;
}
public class FaceSwitcher : MonoBehaviour
{
    public List<FaceState> faceStates = new List<FaceState>();

    public Renderer arRenderer;
    
    private int indexList;

    private int count;
    // Start is called before the first frame update
    void Start()
    {
        indexList = faceStates.Count;

        foreach (FaceState fs in faceStates)
        {
            fs.faceGameObject.SetActive(false);
        }
        
        faceStates[0].faceGameObject.SetActive(true);
        arRenderer.material = faceStates[0].faceMaterial;
    }

    public void SwitchFace()
    {
        faceStates[count].faceGameObject.SetActive(false);
        
        if (count >= indexList-1)
        {
            count = 0;
        }
        else
        {
            count++; 
        }
        //Debug.Log(count);
        arRenderer.material = faceStates[count].faceMaterial;
        faceStates[count].faceGameObject.SetActive(true);
    }

    
}
