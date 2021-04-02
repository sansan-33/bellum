using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Shield : NetworkBehaviour
{
    [SerializeField] private ParticleSystem ShieldEffect;
    private ParticleSystem.MainModule ShieldEffectMain;
    private ParticleSystem ShieldEffects;
    private List<ParticleSystem> ShieldEffectList = new List<ParticleSystem>();
    [SyncVar]
    public float shieldHealth = 0;
    void Start()
    {
        
    }
    [Command]
    public void CmdSetShieldHealth(int shieldHealth)
    {
        this.shieldHealth = shieldHealth;
        //ebug.Log($"gameobject {this.gameObject.name} {this.shieldHealth} / {shieldHealth}");
        for (int i = 10; i > 0; i--)
        {
            ShieldEffects = Instantiate(ShieldEffect, transform);
            ShieldEffects.transform.localScale = new Vector3(5, 5, 5);
            ShieldEffectMain = ShieldEffects.main;
            ShieldEffects.Pause();
            ShieldEffectList.Add(ShieldEffects);
        }
       

        Debug.Log(ShieldEffects);
    }
    // Update is called once per frame
    void Update()
    {
        if(shieldHealth <= 0)
        {
           foreach(ParticleSystem Shied in ShieldEffectList)
            {
                Destroy(Shied);
            }
        }
    }
}
