using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupMessageDisplay : MonoBehaviour
{
    [SerializeField] public TMP_Text PopupMessage = null;
    [SerializeField] public GameObject PopupParentPanel = null;

    public void displayText(float timer, string message)
    {
        PopupParentPanel.SetActive(true);
        StartCoroutine(HandleDisplayText(timer, message));
    }

    IEnumerator HandleDisplayText(float timer, string message)
    {
        PopupMessage.text = message;
        yield return new WaitForSeconds(timer);
        PopupParentPanel.SetActive(false);

    }

}
