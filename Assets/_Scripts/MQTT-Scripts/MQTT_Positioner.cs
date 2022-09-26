using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M2MqttUnity.Examples;

public struct MQTTPosition
{
    public float x;
    public float y;
    public float z;
}

public class MQTT_Positioner : MonoBehaviour
{
    public string topic;

    public GameObject objectToMove;
    
    // Start is called before the first frame update
    void Start()
    {
        M2MqttUnityStagingAR._mqttEvent.AddListener(CheckMQTT_Topic);
        SendPostionToTopic();
    }

    // Update is called once per frame
    void CheckMQTT_Topic(MQTTMsg msg)
    {
        if (msg.topic.Equals("StagingAR/"+topic))
        {
         MoveObject(msg.msg);
        }
    }

    void SendPostionToTopic()
    {
        string msg = JsonUtility.ToJson(objectToMove.transform.position);
        FindObjectOfType<M2MqttUnityStagingAR>().MQTT_Publish(topic, msg);
    }
    
    void SendFeedbackToPosition(){
        string msg = JsonUtility.ToJson(objectToMove.transform.position);
        FindObjectOfType<M2MqttUnityStagingAR>().MQTT_Publish("feedback", msg);
    }
    
    void SendFeedbackToConversion(string msg){
        FindObjectOfType<M2MqttUnityStagingAR>().MQTT_Publish("conversion", msg);
    }
    
    void MoveObject(string mqttMsg)
    {
        MQTTPosition mqttPosition = JsonUtility.FromJson<MQTTPosition>(mqttMsg);
        SendFeedbackToConversion(mqttPosition.x +","+ mqttPosition.y+ "," + mqttPosition.z);
        objectToMove.transform.localPosition = new Vector3(mqttPosition.x,mqttPosition.y,mqttPosition.z);
        SendFeedbackToPosition();
    }

    private void OnDisable()
    {
        M2MqttUnityStagingAR._mqttEvent.RemoveListener(CheckMQTT_Topic);
    }
}
