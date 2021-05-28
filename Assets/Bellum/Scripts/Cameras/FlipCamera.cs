using System.Collections;
using Cinemachine;
using Mirror;
using UnityEngine;

public class FlipCamera : MonoBehaviour
{

    public CinemachineVirtualCamera camPlayer0;
    public CinemachineVirtualCamera camPlayer1;
    public GameObject groundPlayer0;
    public GameObject groundPlayer1;
    public Light lightPlayer0;
    public Light lightPlayer1;
    public Light lightSecondaryPlayer0;
    public Light lightSecondaryPlayer1;
    float lensSize = 28f;
    private CinemachineVirtualCamera camCurrent;
    private bool zooming = false;
    public void Awake()
    {

        if (NetworkClient.connection.identity == null) { return; }
        RTSPlayer player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
        //Debug.Log($"Flip Cam Player ID  {player.GetPlayerID()} , Enemy ID {player.GetEnemyID()}");
        if (player.GetPlayerID() == 0)
        {
            camPlayer0.enabled = true;
            camCurrent = camPlayer0;
            //lightPlayer0.enabled = true;
            //lightSecondaryPlayer0.enabled = true;
            groundPlayer0.SetActive(true);
            camPlayer1.enabled = false;
            //lightPlayer1.enabled = false;
            //lightSecondaryPlayer1.enabled = false;
            groundPlayer1.SetActive(false);
            StaticClass.IsFlippedCamera = false;
        }
        else
        {
            camPlayer1.enabled = true;
            camCurrent = camPlayer1;
            //lightPlayer1.enabled = true;
            //lightSecondaryPlayer1.enabled = true;
            groundPlayer1.SetActive(true);
            camPlayer0.enabled = false;
            //lightPlayer0.enabled = false;
            //lightSecondaryPlayer0.enabled = false;
            groundPlayer0.SetActive(false);
            StaticClass.IsFlippedCamera = true;
        }
     }
    public void Update()
    {
        if (zooming) return;
        StartCoroutine(ZoomCamera());
    }
    private IEnumerator ZoomCamera()
    {
        zooming = true;
        while (camCurrent.m_Lens.OrthographicSize > lensSize)
        {
            yield return new WaitForSeconds(0.01f);
            camCurrent.m_Lens.OrthographicSize -= 0.15f;
        }
    }
}
