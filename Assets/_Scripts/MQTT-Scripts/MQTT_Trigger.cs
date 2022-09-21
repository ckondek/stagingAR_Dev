using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using M2MqttUnity.Examples;

[System.Serializable]
public class MyMQTT_Trigger : UnityEvent
{
}

public class MQTT_Trigger : MonoBehaviour
{
    public string topic;

    public string msg;

    public MyMQTT_Trigger triggerEvent;
    
    
    // Start is called before the first frame update
    void Start()
    {
        M2MqttUnityStagingAR._mqttEvent.AddListener(CheckMQTTMsgforTrigger);
    }

    // Update is called once per frame
    void CheckMQTTMsgforTrigger(MQTTMsg mqttMsg)
    {
        if (mqttMsg.topic.Equals("StagingAR/"+topic) && mqttMsg.msg.Equals(msg))
        {
            triggerEvent.Invoke();
        }
    }

    private void OnDestroy()
    {
        M2MqttUnityStagingAR._mqttEvent.RemoveListener(CheckMQTTMsgforTrigger);
    }
}
