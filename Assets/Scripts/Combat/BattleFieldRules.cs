using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class BattleFieldRules : MonoBehaviour
{
    public  GameObject MiddleLine;
    private Camera mainCamera;
    RTSPlayer player;
    bool IsInFields = true;
    bool IsNotInField = false;
    void Start()
    {
        MiddleLine = GameObject.FindGameObjectWithTag("MiddleLine");
           player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
        mainCamera = Camera.main;
    }
    public bool IsInField(Transform unit)
    {
        //return false;
        
       
        if (((RTSNetworkManager)NetworkManager.singleton).Players.Count == 1)
        {
            IsInFields = true;
            IsNotInField = false;
        }
        else if  (player.GetPlayerID() == 1)
        {
            IsInFields = false;
            IsNotInField = true;
        }
        else
        {
            IsInFields = true;
            IsNotInField = false;
        }
        if (MiddleLine.transform.position.z> unit.position.z)
        {
          //  Debug.Log($"IsInFields-->{IsInFields}");
            return IsInFields;
        }
        else
        {
            //Debug.Log($"IsNotInFields-->{IsNotInField}");
            return IsNotInField;
        }
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
