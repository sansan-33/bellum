using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpCost : MonoBehaviour
{
    public bool useSpCost = true;
    [HideInInspector]public int SPAmount = 0;
    private int MaxSpCost;
    private UnitMeta.UnitKey unitKey;
    private Image SPImage;
    private TMP_Text SPText;

    // Start is called before the first frame update
    void Start()
    {
        MaxSpCost = SPAmount;
        SPImage = GameObject.FindGameObjectWithTag("SP Bar").GetComponent<Image>();
        SPText = GameObject.FindGameObjectWithTag("SP Text").GetComponent<TextMeshProUGUI>();
    }
    public void UpdateSPAmount(int cost,Unit unit)
    {
       
        //Debug.Log("UpdateSPAmount");
        SPAmount += cost;
        SPText.text = (string)SPAmount.ToString();
        SPImage.fillAmount = (float)SPAmount / MaxSpCost;
        if (unit != null)
        {
           bool GettedValue = SpButtonManager.unitBtn.TryGetValue(unit.unitKey, out Button btn);
            if(GettedValue == true)
            {if(btn.GetComponent<SpCostDisplay>().useTimer == false)
                {
                   StartCoroutine(btn.GetComponent<SpCostDisplay>().AddSpCost());
                }
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
