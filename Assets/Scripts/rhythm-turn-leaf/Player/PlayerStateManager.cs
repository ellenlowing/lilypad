using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    // State variables
    PlayerBaseState currentState = null;
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerHopState HopState = new PlayerHopState();
    public PlayerAttackState AttackState = new PlayerAttackState();
    public PlayerDrownState DrownState = new PlayerDrownState();

    // Animation stuff
    public Animator animator;
    [SerializeField] public AnimationClip hopAnimationClip;

    // Collision stuff
    public BoxCollider2D boxCollider;

    // Global variables
    public Vector3 hopDirection = Vector3.zero;

    void Start()
    {
        currentState = IdleState;
        currentState.EnterState(this);

        animator = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(currentState != null)
        {
            currentState.OnCollisionEnter2D(this, other);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(currentState != null)
        {
            currentState.OnCollisionStay2D(this, other);
        }
    }

    public void SetState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
