using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PrefabManager : MonoBehaviour
{

    public bool hideStatic = true;
    public bool hideImg = true;
    [SerializeField]
    private List<GameObject> statGameObjects = new List<GameObject>();
    [SerializeField]
    private List<GameObject> iGameObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
       

        foreach (GameObject go in statGameObjects)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in iGameObjects)
        {
            go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hideStatic)
        {
            foreach (GameObject go in statGameObjects)
            {
                go.SetActive(true);
            } 
        }
        else
        {
            foreach (GameObject go in statGameObjects)
            {
                go.SetActive(false);
            } 
        }
        
        if (!hideImg)
        {
            foreach (GameObject go in iGameObjects)
            {
                go.SetActive(true);
            } 
        }
        else
        {
            foreach (GameObject go in iGameObjects)
            {
                go.SetActive(false);
            } 
        }
        
    }
}
