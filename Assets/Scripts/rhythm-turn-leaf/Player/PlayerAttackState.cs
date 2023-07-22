using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }

    public override void OnCollisionEnter2D(PlayerStateManager player, Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
    }

    public override void OnCollisionStay2D(PlayerStateManager player, Collision2D other)
    {

    }
}
