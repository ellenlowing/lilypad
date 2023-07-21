using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    // State variables
    PlayerBaseState currentState;
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerHopState HopState = new PlayerHopState();
    public PlayerAttackState AttackState = new PlayerAttackState();

    // Animation stuff
    public Animator animator;
    [SerializeField] public AnimationClip hopAnimationClip;

    // Global variables
    public Vector3 hopDirection = Vector3.zero;

    void Start()
    {
        currentState = IdleState;
        currentState.EnterState(this);

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        currentState.OnCollisionEnter2D(this, other);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        currentState.OnCollisionStay2D(this, other);
    }

    public void SetState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
