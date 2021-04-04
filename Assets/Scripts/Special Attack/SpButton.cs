using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpButton : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    public int buttonOffSet;
    public RectTransform FirstCardPos;
    private GameObject button;
    private int buttonCount;
    private List<SpecialAttackDict.SpType> spawnedButtonSpType = new List<SpecialAttackDict.SpType>();
    private List<GameObject> spawnedSpButton = new List<GameObject>();
    void Start()
    {
        
    }
    public bool InstantiateSpButton(SpecialAttackDict.SpType spType,Transform unit)
    {
        //only spawn one button for each type of Sp
        if (!spawnedButtonSpType.Contains(spType))
        {
            spawnedButtonSpType.Add(spType);
            buttonCount++;
            // spawn the button 
            button = Instantiate(buttonPrefab, transform);
            //Set button pos
            button.GetComponent<RectTransform>().anchoredPosition = new Vector3(FirstCardPos.anchoredPosition.x + buttonOffSet * buttonCount, FirstCardPos.anchoredPosition.y, 0);
            spawnedSpButton.Add(button);
            // tell unit where is the button in the list
            unit.GetComponent<Unit>().SpBtnTicket = buttonCount - 1;
            return true;
        }
        else
        {
            return false;
        }
    }
    public GameObject GetButton(int Ticket)
    {
        return spawnedSpButton[Ticket];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
