using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using M2MqttUnity.Examples;
using UnityEngine;

[Serializable]
public struct TabletData
{
    public string id;
    public long latestTimeStamp;
    public Vector3 worldPosition;
    public Vector3 positionRelativeToImageMarker;
    
    public TabletData(string id, long latestTimeStamp, Vector3 worldPosition,Vector3 positionRelativeToImageMarker)
    {
        this.id = id;
        this.latestTimeStamp = latestTimeStamp;
        this.worldPosition = worldPosition;
        this.positionRelativeToImageMarker = positionRelativeToImageMarker;
    }
}


public class MQTT_Multiplayer : MonoBehaviour
{
    public M2MqttUnityStagingAR mqttInterface;

    public List<TabletData> inComingListWithOutMe = new List<TabletData>();

    public List<TabletData> inActiveList = new List<TabletData>();
    
    public List<TabletData> lastIncomingList = new List<TabletData>();
    
    static string _tabletId = "";

    public int secondsOfInactivity = 10;
    
    private bool publishedOnce = true;

    private bool foundMyself; 

    public UI_TextUpdater debugInfo;
    
    
    public void Start()
    {
        _tabletId = CreateId();
        // _tabletId = CreateId();
    }

    public void Update()
    {
        if (mqttInterface.isConnected&& publishedOnce)
        {
            PublishToMyTopic();
            M2MqttUnityStagingAR._mqttEvent.AddListener(HandleMultiplayer);
            publishedOnce = false;
        }
    }

    public void HandleMultiplayer(MQTTMsg msg)
    {
        //push anything to this topic to init Multiplayer on a new Broker Instance
        // if (msg.topic.Contains("NeverPlayedBefore"))
        // {
        //     TabletData td = new TabletData(_tabletId, mqttInterface.timeStamp,new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        //     List<TabletData> tdList = new List<TabletData>();
        //     tdList.Add(td);
        //     mqttInterface.MQTT_Publish("MultiplayerData",JsonHelper.ToJson<TabletData>(tdList.ToArray()));
        // }

        if (msg.topic.Contains("TabletData"))
        {
            TabletData incomingTabletData = JsonUtility.FromJson<TabletData>(msg.msg);
            List<TabletData> newTabletDatas = new List<TabletData>();
            
            // get list of otherplayers
            
            if (CheckTimeStamp(incomingTabletData))
            {
                if (incomingTabletData.id.Equals(_tabletId))
                {
                    return;
                }

                if (inComingListWithOutMe.Count < 1)
                {
                    inComingListWithOutMe.Add(incomingTabletData);
                }
                else
                {
                    foreach (var td in inComingListWithOutMe)
                    {
                        if (td.id.Equals(incomingTabletData.id))
                        {
                            newTabletDatas.Add(incomingTabletData);
                        }
                        else
                        {
                            newTabletDatas.Add(td);
                        }
                    }
                    inComingListWithOutMe = newTabletDatas;
                }
                
            }
        }

        string debugString = inComingListWithOutMe.Aggregate("", (current, tabletData) => current + (tabletData.id + " \n"));

        debugInfo.UpdateOtherPlayers(debugString);
    }

    private bool CheckTimeStamp(TabletData td)
    {
        return td.latestTimeStamp >= mqttInterface.timeStamp - (secondsOfInactivity * 1000);
    }
    
    public void PublishToMyTopic()
    {
        mqttInterface.MQTT_Publish("tablets/" + _tabletId,"Tablet with Id:" + _tabletId + " is online");
    }

    public void UpdateMyData(Vector3 worldPosition, Vector3 positiontoImageTracker)
    {
        TabletData updatedData =
            new TabletData(_tabletId, mqttInterface.timeStamp, worldPosition, positiontoImageTracker);
        mqttInterface.MQTT_Publish("tablets/" + _tabletId + "/TabletData", JsonUtility.ToJson(updatedData));
    }
    
    string CreateId()
    {
        string id = SystemInfo.deviceUniqueIdentifier;
        return id;
    }
    
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.TabletData;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.TabletData = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.TabletData = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] TabletData;
    }
}
