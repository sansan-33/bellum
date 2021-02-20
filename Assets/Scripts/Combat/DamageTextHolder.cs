using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class DamageTextHolder : NetworkBehaviour
{
    public Color CriticalColor = new Color();
    public Color NormalColor = new Color();
    public Color WeakColor = new Color();

    [SyncVar]
    public string displayText = "Default";
    [SyncVar]
    public Color displayColor = Color.green;
    [SyncVar]
    public Quaternion displayRotation ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
