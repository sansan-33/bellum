using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TotalHealthDisplay : NetworkBehaviour
{
    [SerializeField] private TMP_Text TotalPlayerhealth = null;
    [SerializeField] private TMP_Text TotalEnermyhealth = null;
    private int militarySize = 0;
    private int EnermymilitarySize = 0;
    float unitTimer = 1;
    int a = 1;

    private void Update()
    {
        //UnitTimer();
        //if (unitTimer < 6 && unitTimer > 4 && a < 2)
        //{
        TotalPlayerHealthdisplay();
        totalEnermyhealth();
        //}
    }
    private void UnitTimer()
    {

        unitTimer += Time.deltaTime;
    }
    private void TotalPlayerHealthdisplay()
    {
        militarySize = 0;
        GameObject[] armies = GameObject.FindGameObjectsWithTag("Player");
        a++;

        
        foreach (GameObject army in armies)
        {
            //if (unitTimer < 6 && unitTimer > 4)
            //{
            militarySize += army.GetComponent<Health>().getCurrentHealth();
            TotalPlayerhealth.text = militarySize.ToString();
            //}
           

        }


    }
    private void totalEnermyhealth()
    {
        EnermymilitarySize = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject EnermyArmy in enemies)
        {
            EnermymilitarySize += EnermyArmy.GetComponent<Health>().getCurrentHealth();
            TotalEnermyhealth.text = EnermymilitarySize.ToString();
        }

   
    }






}