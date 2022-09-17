using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_playerInfo : MonoBehaviour
{
   
    public Text textGameObject;
    private GameObject camCollider;
    // Start is called before the first frame update
    void Start()
    {
        camCollider = GameObject.Find("cameraCollider");
      
      
       

    }

    // Update is called once per frame
    void Update()
    {
        textGameObject.text = camCollider.transform.position.ToString();
    }
}
