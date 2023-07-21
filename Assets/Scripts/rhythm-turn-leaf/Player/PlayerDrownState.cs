using UnityEngine;

public class PlayerDrownState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.animator.SetBool("gameLost", true);
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }

    public override void OnCollisionEnter2D(PlayerStateManager player, Collision2D other)
    {
        
    }

    public override void OnCollisionStay2D(PlayerStateManager player, Collision2D other)
    {

    }
}
