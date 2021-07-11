using System;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MissionButton : MonoBehaviour, IPointerDownHandler , IPointerClickHandler
{
    [SerializeField] public string mission = null;
    [SerializeField] public GameObject loadingPanel = null;
    [SerializeField] public StageMenu stageMenu = null;
    [SerializeField] public GameObject stagePanel = null;
    [SerializeField] public TMP_Text desc = null;
    [SerializeField] public ScrollRect parentScrollRect = null;
    bool canClick = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canClick == true)
        {
            // add code for click here
            goToMission();
        }
        canClick = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (parentScrollRect.velocity.magnitude > 0)
        {
            canClick = false;
        }
    }
    private void goToMission()
    {
        Debug.Log($"MissionButton clicked, chapter {StaticClass.Chapter} - {mission}");
        StaticClass.Mission = mission;
        StaticClass.enemyRace = (UnitMeta.Race)(Int32.Parse(StaticClass.Chapter) - 1);
        stagePanel.SetActive(false);
        loadingPanel.SetActive(true);
        stageMenu.setStageMission(true);
        NetworkClient.connection.identity.GetComponent<RTSPlayer>().CmdStartMission(StaticClass.Chapter, mission);
    }
}

