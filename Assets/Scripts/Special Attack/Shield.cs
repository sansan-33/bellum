using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private ParticleSystem ShieldEffect;
    public float shieldHealth = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shieldHealth > 0)
        {
            Instantiate(ShieldEffect, this.transform).transform.localScale = new Vector3(5, 5, 5);
        }
    }
}
