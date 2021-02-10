using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Healing : MonoBehaviour
{
    private TacticalBehavior tb;
    private RTSPlayer player;
    private GameObject capsule;
    private int healingRange = 5;

    // Start is called before the first frame update
    void Start()
    {
        tb = FindObjectOfType<TacticalBehavior>();
        player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (tb.GetBehaviorSelectionType() != TacticalBehavior.BehaviorSelectionType.Defend) { return; }
        
        GameObject[] armies = GameObject.FindGameObjectsWithTag("Player" + player.GetPlayerID());
        capsule = GameObject.FindGameObjectWithTag("PlayerBase" + player.GetPlayerID());
        foreach (GameObject army in armies)
        {
            if ((capsule.transform.position - army.transform.position).sqrMagnitude < healingRange * healingRange)
                army.GetComponent<Health>().Healing(Health.healingSpeed);
        }
        
    }
}
