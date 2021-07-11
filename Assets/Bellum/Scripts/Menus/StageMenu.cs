using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Localization.Settings;

public class StageMenu : MonoBehaviour
{
    [SerializeField] public Sprite[] ChapterTitleSprites = new Sprite[4];
    [SerializeField] public GameObject titleObject;
    private bool IS_STAGE_MISSION = false;

    private void Start()
    {
        RTSNetworkManager.ClientOnConnected += HandleClientConnected;
        RTSPlayer.AuthorityOnPartyOwnerStateUpdated += AuthorityHandlePartyOwnerStateUpdated;
        RTSPlayer.ClientOnInfoUpdated += ClientHandleInfoUpdated;
        Mirror.NetworkManager.singleton.StartHost();
        StageMenuButton.TabClicked += HandleTabClicked;
        StaticClass.Chapter = "1";
        StaticClass.EventRankingID = StaticClass.Chapter;

    }

    private void OnDestroy()
    {
        RTSNetworkManager.ClientOnConnected -= HandleClientConnected;
        RTSPlayer.AuthorityOnPartyOwnerStateUpdated -= AuthorityHandlePartyOwnerStateUpdated;
        RTSPlayer.ClientOnInfoUpdated -= ClientHandleInfoUpdated;
        StageMenuButton.TabClicked -= HandleTabClicked;
        LeaveLobby();
    }

    private void HandleClientConnected()
    {
    }
    private void HandleTabClicked(int chapterIndex)
    {
        titleObject.GetComponent<Image>().sprite = ChapterTitleSprites[chapterIndex];
        UnitMeta.Race race = (UnitMeta.Race)Enum.Parse(typeof(UnitMeta.Race), chapterIndex.ToString());
        //titleObject.GetComponentInChildren<TMP_Text>().text = race.ToString();

        // Localization
        AsyncOperationHandle<string> op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(LanguageSelectionManager.STRING_TEXT_REF, race.ToString().ToLower(), null);
        if (op.IsDone)
        {
            titleObject.GetComponentInChildren<TMP_Text>().text = op.Result;
        }
        else
        {
            op.Completed += (o) => titleObject.GetComponentInChildren<TMP_Text>().text = o.Result;
        }
    }

    private void ClientHandleInfoUpdated()
    {
        NetworkClient.connection.identity.GetComponent<RTSPlayer>().CmdSetUserInfo(StaticClass.UserID, StaticClass.playerRace.ToString(), StaticClass.TotalPower);
        //Debug.Log($"Stage Menu ClientHandleInfoUpdated {StaticClass.UserID} , {StaticClass.playerRace.ToString()} , {StaticClass.TotalPower}");
        //List<RTSPlayer> players = ((RTSNetworkManager)NetworkManager.singleton).Players;
    }

    private void AuthorityHandlePartyOwnerStateUpdated(bool state)
    {
        
    }
    public void setStageMission(bool value)
    {
        IS_STAGE_MISSION = value;
    }

    public void LeaveLobby()
    {
        if (StaticClass.Chapter != null && StaticClass.Mission != null ) { return; }
        if (!IS_STAGE_MISSION) { return; }

        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.offlineScene = "Scene_Main_Menu";
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();
            SceneManager.LoadScene("Scene_Main_Menu");
        }
    }
}
