using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimClipTime : MonoBehaviour
{

    public float attackTime;
    public float damageTime;
    public float deathTime;
    public float idleTime;

    private Animator anim;
    private AnimationClip clip;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("Error: Did not find anim!");
        }
        else
        {
            //Debug.Log("Got anim");
            // set anim speed = length / repeat attack dealy 
        }

        UpdateAnimClipTimes();
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Attacking":
                    attackTime = clip.length;
                    break;
                case "Damage":
                    damageTime = clip.length;
                    break;
                case "Falling Back Death":
                    deathTime = clip.length;
                    break;
                case "Idle":
                    idleTime = clip.length;
                    break;
            }
        }
        Debug.Log($"UpdateAnimClipTimes Falling Back Death:{deathTime} ");
    }
}