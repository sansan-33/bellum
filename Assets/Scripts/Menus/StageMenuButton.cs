using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageMenuButton : MonoBehaviour, IPointerClickHandler
{
    //[SerializeField] private GameObject landingPagePanel = null;
    //[SerializeField] private GameObject stagePanel = null;
    [SerializeField] public string chapter = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        //stagePanel.SetActive(true);
        //landingPagePanel.SetActive(false);
        Debug.Log($"StageMenuButton OnPointerClick chapter {chapter}");
        StaticClass.Chapter = chapter;
        var buttons = FindObjectsOfType<StageMenuButton>();
        foreach(StageMenuButton button in buttons)
        {
            button.transform.GetChild(0).gameObject.SetActive(false);
        }
        this.transform.GetChild(0).gameObject.SetActive(true);
        //Mirror.NetworkManager.singleton.StartHost();
    }
}

