using System;
using Mirror;
using UnityEngine;

public class GreatWallController : NetworkBehaviour
{
    public static event Action<string, string> GateOpened;
    [SyncVar(hook = nameof(HandleDynamicBlock))]
    private bool IS_BLOCKED = false;

    //==================================== Set Skill For Unit
    public void GateOpen(string playerid, string doorIndex)
    {
        if (isServer)
            RpcGateOpen(playerid, doorIndex);
        else
            CmdGateOpen(playerid, doorIndex);
    }
    [Command]
    public void CmdGateOpen(string playerid, string doorIndex)
    {
        ServerGateOpen(playerid, doorIndex);
    }
    [ClientRpc]
    public void RpcGateOpen(string playerid, string doorIndex)
    {
        HandleGateOpen(playerid, doorIndex);
    }
    [Server]
    public void ServerGateOpen(string playerid, string doorIndex)
    {
        HandleGateOpen(playerid, doorIndex);
    }
    private void HandleGateOpen(string playerid, string doorIndex)
    {
        GateOpened?.Invoke("" + playerid, doorIndex);
    }
    /*
    void OnEnable()
    {
        foreach (MeshRenderer wall in GetComponentsInChildren<MeshRenderer>())
        {
            wall.enabled = false;
        }
    }
    */
    public void dynamicBlock(bool is_block)
    {
        IS_BLOCKED = is_block;
    }
    private void HandleDynamicBlock(bool old_block, bool new_block)
    {
        transform.GetChild(0).gameObject.SetActive(new_block);
        // Recalculate only the first grid graph
        var graphToScan = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan(graphToScan);
    }

}
