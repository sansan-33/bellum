using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuStageButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private GameObject multiplayerPanel = null;
    [SerializeField] private GameObject stagePanel = null;
    public enum ButtonType {STAGE,MULTI};
    public ButtonType currentButton;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(currentButton == ButtonType.MULTI) 
            multiplayerPanel.SetActive(true);
        else
            stagePanel.SetActive(true);

        landingPagePanel.SetActive(false);
        Mirror.NetworkManager.singleton.StartHost();
    }
}

