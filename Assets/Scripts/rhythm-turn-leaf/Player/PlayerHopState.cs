using UnityEngine;

public class PlayerHopState : PlayerBaseState
{
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float stepSize = 1f;
    private float hopTime;
    private float wholeDuration;
    private float hopDuration;
    private float startHopTime;
    private float endHopTime;

    public override void EnterState(PlayerStateManager player)
    {
        player.animator.SetTrigger("hopping");
        startPosition = player.transform.position;
        endPosition = new Vector3(
            player.transform.position.x + player.hopDirection.x * stepSize,
            player.transform.position.y + player.hopDirection.y * stepSize,
            0f
        );
        hopTime = 0f;
        wholeDuration = player.hopAnimationClip.length;
        startHopTime = 3f / player.hopAnimationClip.frameRate;
        endHopTime = 5f / player.hopAnimationClip.frameRate;
        hopDuration = endHopTime - startHopTime;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if(hopTime < wholeDuration)
        {
            if(hopTime >= startHopTime && hopTime <= endHopTime)
            {
                player.transform.position = Vector3.Lerp(startPosition, endPosition, (hopTime - startHopTime) / hopDuration);
            }
            hopTime += Time.deltaTime;
        } 
        else 
        {
            player.SetState(player.IdleState);
        }
    }

    public override void OnCollisionEnter2D(PlayerStateManager player, Collision2D other)
    {

    }

    public override void OnCollisionStay2D(PlayerStateManager player, Collision2D other)
    {

    }
}
