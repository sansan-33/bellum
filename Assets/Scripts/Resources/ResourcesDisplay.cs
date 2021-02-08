using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesDisplay : MonoBehaviour
{
    [SerializeField] private Image healthBarImage = null;
    int maxEleixer = 0;
    int currentEleixer;
    private CardDealer cardDealer;

    private void Start()
    {
        cardDealer = GameObject.FindGameObjectWithTag("DealManager").GetComponent<CardDealer>();
        maxEleixer = cardDealer.maxEleixer;
    }
    private void Update()
    {
        currentEleixer = cardDealer.eleixer;
        healthBarImage.fillAmount = (float)currentEleixer / (float)maxEleixer;
        
    }
}
