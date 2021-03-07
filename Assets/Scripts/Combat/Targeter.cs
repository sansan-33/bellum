using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Targeter : NetworkBehaviour
{
    private Targetable target;
    public enum AttackType { Shoot, Slash, Magic, Heal, Nothing };
    public AttackType targeterAttackType;

    public Targetable GetTarget()
    {
        //Debug.Log($"Targeter-->{target}");
        return target;
    }

    public override void OnStartServer()
    {
        GameOverHandler.ServerOnGameOver += ServerHandleGameOver;
        targeterAttackType = AttackType.Nothing;
    }

    public override void OnStopServer()
    {
        GameOverHandler.ServerOnGameOver -= ServerHandleGameOver;
    }

    [Command]
    public void CmdSetTarget(GameObject targetGameObject)
    {
        
        if (!targetGameObject.TryGetComponent<Targetable>(out Targetable newTarget)) { return; }
       
        target = newTarget;
        //Debug.Log($"{this.transform.GetComponent<Unit>().unitType}Target-->{target}");
    }
    [Command]
    public void CmdSetAttackType( AttackType attackType)
    {
        targeterAttackType = attackType;
    }
    [Server]
    public void ClearTarget()
    {
        target = null;
    }

    [Server]
    private void ServerHandleGameOver()
    {
        ClearTarget();
    }
    private void Update()
    {
        //Debug.Log($"Update Tag{this.tag}///////////////////////{this.transform.GetComponent<Unit>().unitType}Target-->{target}");
    }
}
