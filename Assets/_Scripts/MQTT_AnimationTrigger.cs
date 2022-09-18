using System.Collections;
using System.Collections.Generic;
using M2MqttUnity.Examples;
using UnityEngine;

public class MQTT_AnimationTrigger : MonoBehaviour
{
    public string topic;

    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        M2MqttUnityStagingAR._mqttEvent.AddListener(CheckMQTTMsgforAnimationTrigger);
    }

    // Update is called once per frame
    void CheckMQTTMsgforAnimationTrigger(MQTTMsg msg)
    {
        if (msg.topic.Equals("StagingAR/"+topic))
        {
            animator.SetTrigger(msg.msg);
            Debug.Log(msg.msg + " triggered");
        }
    }
}
