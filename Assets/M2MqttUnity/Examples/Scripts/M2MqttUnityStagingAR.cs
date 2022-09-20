/*
The MIT License (MIT)

Copyright (c) 2018 Giovanni Paolo Vigano'

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;
using UnityEngine.Events;

/// <summary>
/// Examples for the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt),
/// </summary>
namespace M2MqttUnity.Examples
{
    /// <summary>
    /// Script for testing M2MQTT with a Unity UI
    /// </summary>
    ///

    public struct MQTTMsg
    {
        public string topic;
        public string msg;

        public MQTTMsg(string topic, string msg)
        {
            this.topic = topic;
            this.msg = msg;
        }
    }
    
    [Serializable]
    public class MyMQTTEvent : UnityEvent<MQTTMsg>
    {
    }
    
    public class M2MqttUnityStagingAR : M2MqttUnityClient
    {
        [Tooltip("Set this to true to perform a testing cycle automatically on startup")]
        public bool autoTest = false;

        private List<MQTTMsg> eventMessages = new List<MQTTMsg>();

        public static MyMQTTEvent _mqttEvent;

        public long timeStamp;

        public bool isConnected = false;
        
        public void TestPublish()
        {
            client.Publish("StagingAR/test", System.Text.Encoding.UTF8.GetBytes("Test message"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            Debug.Log("Test message published");
        }

        public void MQTT_Publish(string topic, string msg)
        {
            client.Publish("StagingAR/" + topic, System.Text.Encoding.UTF8.GetBytes(msg), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        }
        
        protected override void OnConnected()
        {
            base.OnConnected();

            isConnected = true;
            
            if (autoTest)
            {
                TestPublish();
            }
        }

        protected override void SubscribeTopics()
        {
            client.Subscribe(new string[] { "StagingAR/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        protected override void UnsubscribeTopics()
        {
            client.Unsubscribe(new string[] { "StagingAR/#" });
        }
        

        protected override void Awake()
        {
            
            base.Awake();
            
            if (_mqttEvent == null)
                _mqttEvent = new MyMQTTEvent();
        }

        protected override void DecodeMessage(string topic, byte[] message)
        {
            string msg = System.Text.Encoding.UTF8.GetString(message);
            
            MQTTMsg mqttMsg = new MQTTMsg();
            mqttMsg.topic = topic;
            mqttMsg.msg = msg;
            
            StoreMessage(mqttMsg);
            
            if (topic == "StagingAR/test")
            {
                if (autoTest)
                {
                    autoTest = false;
                   // Disconnect();
                }
            }
        }

        private void StoreMessage(MQTTMsg eventMsg)
        {
            eventMessages.Add(eventMsg);
        }

        private void ProcessMessage(MQTTMsg msg)
        {
            //Debug.Log("Received: " + msg.msg + "in topic: " + msg.topic);
            if (msg.topic.Equals("StagingAR/timestamp"))
            {
                timeStamp = long.Parse(msg.msg);
            }
            _mqttEvent.Invoke(msg);
        }

        protected override void Update()
        {
            base.Update(); // call ProcessMqttEvents()

            if (eventMessages.Count > 0)
            {
                foreach (MQTTMsg msg in eventMessages)
                {
                    ProcessMessage(msg);
                }
                eventMessages.Clear();
            }
           
        }

        private void OnDestroy()
        {
            Disconnect();
            isConnected = false;
        }

        private void OnValidate()
        {
            if (autoTest)
            {
                autoConnect = true;
            }
        }
    }
}
