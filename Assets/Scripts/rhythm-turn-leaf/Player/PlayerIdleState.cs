using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private Transform lilypadTransform;

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entering Idle state");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.hopDirection = Vector3.zero;
        player.transform.localEulerAngles = Vector3.zero;
        
        float attackHorizontal = Input.GetAxisRaw("Horizontal_L");
        float attackVertical = Input.GetAxisRaw("Vertical_L");
        bool attacking = attackHorizontal != 0f || attackVertical != 0f;
        player.animator.SetBool("attacking", attacking);

        if(attacking)
        {
            player.SetState(player.AttackState);
            if(attackHorizontal == 1f)
            {
                player.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
            } else if (attackHorizontal == -1f)
            {
                player.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
            } else if (attackVertical == -1f)
            {
                player.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
            }
        }
        else if(lilypadTransform != null)
        {
            float rotZ = lilypadTransform.eulerAngles.z % 360;
            if(rotZ == 0f) 
            {
                player.hopDirection.y = 1f;
            } 
            else if (rotZ == 180f)
            {
                player.hopDirection.y = -1f;
            } 
            else if (rotZ == 90f)
            {
                player.hopDirection.x = -1f;
            } 
            else if (rotZ == 270f)
            {
                player.hopDirection.x = 1f;
            }
            player.transform.parent.eulerAngles = new Vector3(0f, 0f, rotZ + 90f);
        }
        
    }

    public override void OnCollisionEnter2D(PlayerStateManager player, Collision2D other)
    {
    }

    public override void OnCollisionStay2D(PlayerStateManager player, Collision2D other)
    {
        if(other.gameObject.CompareTag("Lilypad"))
        {
            lilypadTransform = other.gameObject.transform;
        }
    }
}
