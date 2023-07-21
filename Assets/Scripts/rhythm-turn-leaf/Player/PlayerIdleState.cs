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
        
        float rotZ = lilypadTransform.localEulerAngles.z % 360;
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
        player.transform.localEulerAngles = new Vector3(0f, 0f, rotZ + 90f);
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
