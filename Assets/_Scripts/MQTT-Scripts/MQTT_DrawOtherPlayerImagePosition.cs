using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MQTT_DrawOtherPlayerImagePosition : MonoBehaviour
{

    public GameObject otherPlayerPrefab;

    private List<TabletData> otherPlayer;
    [SerializeField]
    private List<GameObject> otherPlayers;
    private MQTT_Multiplayer mqttMultiplayer;
    
    // Start is called before the first frame update
    private void Start()
    {
        mqttMultiplayer = FindObjectOfType<MQTT_Multiplayer>();
    }

    // Update is called once per frame
    private void Update()
    {
        otherPlayer = mqttMultiplayer.inComingListWithOutMe;

        SetupOtherPlayers(otherPlayer);

        if (otherPlayers.Count <= 0) return;
        
        UpdateListofOtherplayers();

    }

    private void UpdateListofOtherplayers()
    {
        foreach (var opGO in otherPlayers)
        {
            //updates position in scene should be lerped in the gameObject/otherplayerScript
            foreach (var td in otherPlayer.Where(td => td.id.Equals(opGO.name)))
            {
                opGO.transform.position = td.positionRelativeToImageMarker;
            }
            
            //not tested yet - cleans out all in active players
            foreach (var tdIap in mqttMultiplayer.inActiveList.Where(td => td.id.Equals(opGO.name)))
            {
                Destroy(opGO);
            }
        }
    }

    void SetupOtherPlayers(List<TabletData> td)
    {
        GameObject otherPlayerRef;

        foreach (var op in otherPlayer.Where(op => !CheckIfPlayerGameObjectExists(op)))
        {
            otherPlayerRef = Instantiate(otherPlayerPrefab);
            otherPlayerRef.name = op.id;
            otherPlayers.Add(otherPlayerRef);
        }
    }
    
    private bool CheckIfPlayerGameObjectExists(TabletData op)
    {
        return otherPlayers.Any(otherPlayerGO => op.id.Equals(otherPlayerGO.name));
    }
}
