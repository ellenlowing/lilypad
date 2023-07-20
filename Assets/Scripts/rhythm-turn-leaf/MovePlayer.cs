using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FroggyNamespace;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    Vector3 jumpDirection = Vector3.zero;
    public bool onLilypad = true;
    public bool hopping = false;
    Transform activeLilypadTransform = null;
    
    [SerializeField]
    float stepSize;

    private Animator animator;

    [SerializeField]
    private AnimationClip hopAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!hopping)
        {
            jumpDirection = Vector3.zero;
            float lilyRotationZ = activeLilypadTransform.localEulerAngles.z % 360;
            if(lilyRotationZ == 0f) 
            {
                jumpDirection.y = 1f;
            } 
            else if (lilyRotationZ == 180f)
            {
                jumpDirection.y = -1f;
            } 
            else if (lilyRotationZ == 90f)
            {
                jumpDirection.x = -1f;
            } 
            else if (lilyRotationZ == 270f)
            {
                jumpDirection.x = 1f;
            }
            transform.localEulerAngles = new Vector3(0f, 0f, lilyRotationZ + 90f);
        } 

        // FOR DEBUG
        // if(Input.GetKeyDown("space"))
        // {
        //     Jump();
        // }
    }

    public void Jump()
    {
        if(!hopping && LeafConductor.instance.currentGameState != GameState.Lose)
        {
            animator.SetTrigger("hopping");
            StartCoroutine(Hop(stepSize));
        }
    }

    IEnumerator Hop(float numSteps)
    {
        hopping = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(
            transform.position.x + jumpDirection.x * stepSize,
            transform.position.y + jumpDirection.y * stepSize,
            0f
        );
        float time = 0;
        float duration = hopAnimation.length; // frames in sequence / sprite fps
        float startHopTime = 3f / hopAnimation.frameRate; // 3 is the start frame in sprite sheet that hop motion takes place
        float endHopTime = 5f / hopAnimation.frameRate; // 5 is landing frame
        float hopDuration = endHopTime - startHopTime;
        while(time < duration)
        {
            if(time >= startHopTime && time <= endHopTime)
            {
                transform.position = Vector3.Lerp(startPos, endPos, (time - startHopTime) / hopDuration);
            }
            time += Time.deltaTime;
            yield return null;
        }
        hopping = false;

        // check at the end of a hop and see if it's not on lilypad
        if(!onLilypad)
        {
            Debug.Log("You lose!");
            LeafConductor.instance.currentGameState = GameState.Lose;
            animator.SetBool("gameLost", true);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Lilypad"))
        {
            onLilypad = true; 
            activeLilypadTransform = other.gameObject.transform;
            // Debug.Log("enter lilypad " + other.gameObject.name);
        }
        
        if (other.gameObject.CompareTag("Finish"))
        {
            LeafConductor.instance.currentGameState = GameState.Win;
        }   
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Lilypad"))
        {
            onLilypad = true;
            // Debug.Log("staying lilypad " + other.gameObject.name);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Lilypad"))
        {
            onLilypad = false;
            // Debug.Log("leaving lilypad " + other.gameObject.name);
        }
    }
}
