using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class AR_PhillipCustom : MonoBehaviour
{
    //info begin
    [Header("This component requires a ARTrackedImageManager.",order = 0)]
    [Space(1.0f,order = 1)]
    [Header("!!! Prefabs need a <PrefabManager> Component !!!",order = 2)]
    [Space(1.0f,order = 3)]
    [Header("Put all prefabs you want to spawn in the list below.",order = 4)]
    [Space(1.0f,order = 5)]
    [Header("Name them accordingly to the marker you want to spawn them on.",order = 6)]
    [Space(1.0f,order = 7)]
    //info end
    
    [SerializeField] private GameObject[] picturePrefabs; // list of all thinks you want to spawn
    private ARTrackedImageManager _arTrackedImageManager; // references to the anchor made by image tracking 

    private Dictionary<string, GameObject> picPrefabs = new Dictionary<string, GameObject>(); // create dictionary to compare names to
    

    private void Awake()
    {
        //Get ARImageManager Component to interact
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();

        //instantiate Prefabs and put in Dictionary
        foreach (GameObject picPrefab in picturePrefabs)
        {
            GameObject newPicPrefab = Instantiate(picPrefab, Vector3.zero, Quaternion.identity);
            newPicPrefab.name = picPrefab.name;
            picPrefabs.Add(picPrefab.name, newPicPrefab);
        }

    }

    private void Start()
    {
        // hide all Prefabs
        foreach (KeyValuePair<string, GameObject> entry in picPrefabs)
        {
            entry.Value.SetActive(false);
        }
    }

    private void OnEnable()
    {
        // connect to tracked Images function
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        // disconnect to tracked Images function
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)// happens every time a Image is scanned the first time 
        {
            UpdateContent(trackedImage);
        }
        
        foreach (ARTrackedImage trackedImage in eventArgs.updated) // happens every time a Image position is updated
        {
            UpdateContent(trackedImage);
        }
        
        foreach (ARTrackedImage trackedImage in eventArgs.removed) // happens if Image is removed from the tracked Image list - note: it happens not very often.
        {
            //hide prefabs if track Image is Removed
            foreach (KeyValuePair<string, GameObject> entry in picPrefabs)
            {
                if (entry.Key == trackedImage.referenceImage.name)
                {
                   entry.Value.GetComponent<PrefabManager>().hideImg = true; 
                }
            }
        }
    }

    private void UpdateContent(ARTrackedImage trackedImage)
    {
        MatchContentAndImage(trackedImage.referenceImage.name, trackedImage.transform.position, trackedImage.transform.forward);
    }

    void MatchContentAndImage(string nameImg, Vector3 newPosition, Vector3 newForward)
    {
        if (picturePrefabs != null)
        {
            picPrefabs[nameImg].SetActive(true);
            picPrefabs[nameImg].GetComponent<PrefabManager>().hideImg = false;
            UpdateStaticObject(nameImg);
            picPrefabs[nameImg].transform.position = newPosition;
            picPrefabs[nameImg].transform.forward = newForward;

        }
    }

    void UpdateStaticObject(string nameImg)
    {
        foreach (KeyValuePair<string, GameObject> entry in picPrefabs)
        {
            if (entry.Key.Equals(nameImg))
            {
                entry.Value.GetComponent<PrefabManager>().hideStatic = false;
            }
            else
            {
                entry.Value.GetComponent<PrefabManager>().hideStatic = true;
            }
        }
    }

}
