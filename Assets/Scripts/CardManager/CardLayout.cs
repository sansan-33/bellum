using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLayout : MonoBehaviour
{
    [SerializeField] int cardindex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cardindex == 0) { this.GetComponent<RectTransform>().anchoredPosition = new Vector3(-588, 206, 0); }
        if (cardindex == 1) { this.GetComponent<RectTransform>().anchoredPosition = new Vector3(-508,206,0); }
        if (cardindex == 2) { this.GetComponent<RectTransform>().anchoredPosition = new Vector3(-404,206,0); }
        if (cardindex == 3) { this.GetComponent<RectTransform>().anchoredPosition = new Vector3(-306,206,0); }
        if (cardindex == 4) { this.GetComponent<RectTransform>().anchoredPosition = new Vector3(-182,206,0); }
        if (cardindex == 5) { this.GetComponent<RectTransform>().anchoredPosition = new Vector3(-82,206,0); }
    }
}
