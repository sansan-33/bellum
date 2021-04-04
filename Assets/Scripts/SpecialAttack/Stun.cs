using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class Stun : MonoBehaviour
{
    private Unit[] enemyList;
    private TacticalBehavior TB;
    private Button SPButton;
    private RTSPlayer player;
    private SpCost spCost;

    private float enemyReFightTimer = -10000;
    public float SPCost = 10;
    public int buttonTicket;
    private bool SpawnedButton;
    private bool IsFrezzing;
    private bool CanUnFrezze = false;
    public int EnemyFrezzeTime = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
        spCost = FindObjectOfType<SpCost>();
        //Instantiate SpButton and is it already spawned
        SpawnedButton = FindObjectOfType<SpButton>().InstantiateSpButton(SpecialAttackDict.SpecialAttackType.Stun, GetComponent<Unit>());
        //SpButton will give unit a int to get back the button that it spwaned
        if (SpawnedButton) { SPButton = FindObjectOfType<SpButton>().GetButton(GetComponent<Unit>().SpBtnTicket).GetComponent<Button>(); }
        if (SPButton == null) { return; }
        SPButton.onClick.RemoveAllListeners();
        SPButton.onClick.AddListener(OnPointerDown);
        TB = GameObject.FindGameObjectWithTag("TacticalSystem").GetComponent<TacticalBehavior>();
    }
    public void OnPointerDown()
    {
        //Debug.Log("OnPointerDown");
        spCost.SPAmount -= (int)SPCost;
        
        //find all unit
        enemyList = FindObjectsOfType<Unit>();
       
        if (((RTSNetworkManager)NetworkManager.singleton).Players.Count == 1)//1 player mode
        {
           //Debug.Log("One Player Mide");
            foreach (Unit unit in enemyList)
            {  // Only Set on our side
                //Debug.Log($"Tag -- > {unit.tag}");
                if (unit.CompareTag("Player1") || unit.CompareTag("King1"))
                {
                    //stop enenmy
                    //Debug.Log("stop");
                    unit.GetComponent<UnitMovement>().CmdStop();
                    enemyReFightTimer = EnemyFrezzeTime;
                    IsFrezzing = true;
                 
                }
            }
        }
        else // Multi player seneriao
        {
            //Debug.Log($"OnPointerDown Defend SP Multi shieldList {shieldList.Length}");
            foreach (Unit unit in enemyList)
            {  // Only Set on our side
                if (unit.CompareTag("Player" + player.GetEnemyID()) || unit.CompareTag("King" + player.GetEnemyID()))
                {
                    //stop enenmy
                    unit.GetComponent<UnitMovement>().CmdStop();
                    enemyReFightTimer = EnemyFrezzeTime;
                    IsFrezzing = true;
                }
            }
        }
    }
    /*public bool Between(int num, int lower, int upper)
    {
        if(num > lower&& num < upper)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void AwakeEnemy()
    {
        TB.TryReinforce(null);
    }*/
        // Update is called once per frame
    void Update()
    {
        if(enemyReFightTimer > 0)
        {
            enemyReFightTimer -= Time.deltaTime;
        }
        else 
        {
            IsFrezzing = false;
        }
       if(IsFrezzing == true)
       {
            foreach (Unit unit in enemyList)
            {  
                if (unit.CompareTag("Player1") || unit.CompareTag("King1"))
                {
                    unit.GetComponent<UnitMovement>().CmdStop();  
                }
            }
        }
    }
}
