using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerStateManager player);
    public abstract void UpdateState(PlayerStateManager player);
    public abstract void OnCollisionEnter2D(PlayerStateManager player, Collision2D other);
    public abstract void OnCollisionStay2D(PlayerStateManager player, Collision2D other);
}
