using Mirror;
using UnityEngine;

public class UnitAnimator : NetworkBehaviour
{
    [SerializeField] public NetworkAnimator networkAnim;
    AnimatorClipInfo[] m_CurrentClipInfo;
    [SyncVar(hook = nameof(HookStateControl))] private AnimState currentState;
    public enum AnimState { IDLE, ATTACK, DEFEND, RUN  };
    bool isAttacking = false;
   
    public override void OnStartServer()
    {
        if (networkAnim == null)
        {
            networkAnim = GetComponent<NetworkAnimator>();
        }
    }
    void ChangeAnimationState(AnimState newState)
    {
        //if (newState == UnitAnimator.AnimState.DEFEND) return;
        Debug.Log($"ChangeAnimationState current {currentState} new {newState.ToString()}");
        if (currentState == newState) return;
        networkAnim.animator.StopPlayback();
        networkAnim.animator.Play(newState.ToString());
        currentState = newState;
    }
    public void StateControl(AnimState newState)
    {
        if (newState == AnimState.ATTACK) {
            if (!isAttacking) {
                isAttacking = true;
                ChangeAnimationState(newState);
            }
            m_CurrentClipInfo = networkAnim.animator.GetCurrentAnimatorClipInfo(0);

            //Debug.Log($"GetCurrentAnimatorClipInfo {m_CurrentClipInfo[0].clip.name } {m_CurrentClipInfo[0].clip.length}");
            Invoke("AttackCompleted", m_CurrentClipInfo[0].clip.length);
            return;
        }
        ChangeAnimationState(newState);

    }
    public void HookStateControl(AnimState oldSate, AnimState newState)
    {
        StateControl(newState);
    }
    private void AttackCompleted()
    {
        isAttacking = false;
        currentState = AnimState.IDLE;
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
                //Debug.Log($" Set Float {type} speed {networkAnim.animator.speed} , repeat attack delay: {animSpeed} , clip name {clipName} ,clip length {clipLength}");
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
        //Debug.Log($"Unit Animator {tag} {name} CMD set bool {animationType} {state}");
        ServerSetBool(animationType,state);
    }

    [Server]
    public void ServerSetBool(string animationType, bool state)
    {
        //Debug.Log($"Unit Animator {tag} {name}  Server set bool {animationType} {state}");
        networkAnim.animator.SetBool(animationType, state);
        //anim.SetBool(animationType, state);
    }

}


/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 5f;

    private Animator animator;

    private float xAxis;
    private float yAxis;
    private Rigidbody2D rb2d;
    private bool isJumpPressed;
    private float jumpForce = 850;
    private int groundMask;
    private bool isGrounded;
    private string currentAnimaton;
    private bool isAttackPressed;
    private bool isAttacking;

    [SerializeField]
    private float attackDelay = 0.3f;

    //Animation States
    const string PLAYER_IDLE = "Player_idle";
    const string PLAYER_RUN = "Player_run";
    const string PLAYER_JUMP = "Player_jump";
    const string PLAYER_ATTACK = "Player_attack";
    const string PLAYER_AIR_ATTACK = "Player_air_attack";

    //=====================================================
    // Start is called before the first frame update
    //=====================================================
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Ground");

    }

    //=====================================================
    // Update is called once per frame
    //=====================================================
    void Update()
    {
        //Checking for inputs
        xAxis = Input.GetAxisRaw("Horizontal");

        //space jump key pressed?
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }

        //space Atatck key pressed?
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            isAttackPressed = true;
        }
    }

    //=====================================================
    // Physics based time step loop
    //=====================================================
    private void FixedUpdate()
    {
        //check if player is on the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //------------------------------------------

        //Check update movement based on input
        Vector2 vel = new Vector2(0, rb2d.velocity.y);

        if (xAxis < 0)
        {
            vel.x = -walkSpeed;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (xAxis > 0)
        {
            vel.x = walkSpeed;
            transform.localScale = new Vector2(1, 1);

        }
        else
        {
            vel.x = 0;

        }

        if (isGrounded && !isAttacking)
        {
            if (xAxis != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }

        //------------------------------------------

        //Check if trying to jump 
        if (isJumpPressed && isGrounded)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);
        }

        //assign the new velocity to the rigidbody
        rb2d.velocity = vel;


        //attack
        if (isAttackPressed)
        {
            isAttackPressed = false;

            if (!isAttacking)
            {
                isAttacking = true;

                if (isGrounded)
                {
                    ChangeAnimationState(PLAYER_ATTACK);
                }
                else
                {
                    ChangeAnimationState(PLAYER_AIR_ATTACK);
                }


                Invoke("AttackComplete", attackDelay);


            }


        }

    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    //=====================================================
    // mini animation manager
    //=====================================================
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }

}
*/