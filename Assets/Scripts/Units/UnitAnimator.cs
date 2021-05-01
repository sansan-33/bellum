using Mirror;
using UnityEngine;

public class UnitAnimator : NetworkBehaviour
{
    [SerializeField] public NetworkAnimator networkAnim;
    //[SerializeField] public Animator anim;
    AnimatorClipInfo[] m_CurrentClipInfo;

    public override void OnStartServer()
    {
        if (networkAnim == null)
        {
            networkAnim = GetComponent<NetworkAnimator>();
        }
        //networkAnim.animator.SetFloat("MoveSpeed", speed); //will act as multiplier to the speed of the run animation clip
        //networkAnim.animator.SetBool("IsMoving", true);
    }
    public void trigger(string type)
    {
        CmdTrigger(type);
    }
    public void trigger(string type, float animSpeed)
    {
        CmdTrigger(type);
        //SetFloat("animSpeed", animSpeed);
    }
    [Command]
    public void CmdTrigger(string animationType)
    {
        ServerTrigger(animationType);
    }

    [Server]
    public void ServerTrigger(string animationType)
    {
        networkAnim.SetTrigger(animationType);
    }
    public void SetFloat(string type, float animSpeed)
    {
        if (animSpeed > 0f)
        {
            float clipLength = 0f;
            string clipName = "";
            m_CurrentClipInfo = networkAnim.animator.GetCurrentAnimatorClipInfo(0);
            foreach (AnimatorClipInfo animatorClipInfos in m_CurrentClipInfo)
            {
                if (animatorClipInfos.clip.name.ToLower().Contains("attack"))
                {
                    clipLength = animatorClipInfos.clip.length;
                    clipName = animatorClipInfos.clip.name;
                    break;
                }
            }
            if (clipLength > 0f)
            {
                networkAnim.animator.SetFloat(type, animSpeed > 1 ? 2 : clipLength / animSpeed);
                //networkAnim.animator.speed = animSpeed / clipLength;
                Debug.Log($" Set Float {type} speed {networkAnim.animator.speed} , repeat attack delay: {animSpeed} , clip name {clipName} ,clip length {clipLength}");
            }
        }
    }
    public void SetBool(string type, bool state)
    {
        //networkAnim.animator.SetBool(type, state);
        networkAnim.animator.SetBool(type, state);
        //CmdSetBool(type, state);
    }
    [Command]
    public void CmdSetBool(string animationType, bool state)
    {
        Debug.Log($"Unit Animator {tag} {name} CMD set bool {animationType} {state}");
        ServerSetBool(animationType,state);
    }

    [Server]
    public void ServerSetBool(string animationType, bool state)
    {
        Debug.Log($"Unit Animator {tag} {name}  Server set bool {animationType} {state}");
        networkAnim.animator.SetBool(animationType, state);
        //anim.SetBool(animationType, state);
    }

}
