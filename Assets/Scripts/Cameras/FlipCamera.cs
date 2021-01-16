using Mirror;
using UnityEngine;

public class FlipCamera : MonoBehaviour
{

    public Camera camPlayer0;
    public Camera camPlayer1;

    public void Awake()
    {

        RTSPlayer player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
        Debug.Log($"Flip Cam Player ID  {player.GetPlayerID()} , Enemy ID {player.GetEnemyID()}");
        if (player.GetPlayerID() == 0)
        {
            camPlayer0.enabled = true;
            camPlayer1.enabled = false;

        }
        else
        {
            camPlayer1.enabled = true;
            camPlayer0.enabled = false;

        }
     }
}
