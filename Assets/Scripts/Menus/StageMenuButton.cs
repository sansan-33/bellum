using System;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageMenuButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public string chapter = null;
    [SerializeField] public TMP_Text chapter_text = null;
   
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"StageMenuButton OnPointerClick chapter {chapter}");
        StaticClass.Chapter = chapter;
        UnitMeta.Race race = (UnitMeta.Race)Enum.Parse(typeof(UnitMeta.Race), (int.Parse(chapter) - 1).ToString() );
        chapter_text.text = race.ToString();
        var buttons = FindObjectsOfType<StageMenuButton>();
        foreach(StageMenuButton button in buttons)
        {
            button.transform.GetChild(0).gameObject.SetActive(false);
        }
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}

