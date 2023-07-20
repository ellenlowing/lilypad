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

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        jumpDirection = new Vector3(1f, 0f, 0f);
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
        if(!hopping)
        {
            anim.SetTrigger("hopping");
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
        float duration = 7f / 8f; // frames in sequence / sprite fps
        float startHopTime = 3f / 8f;
        float endHopTime = 5f / 8f;
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
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Lilypad")
        {
            onLilypad = true; 
            activeLilypadTransform = other.gameObject.transform;
        }
        else if (other.gameObject.tag == "Finish")
        {
            LeafConductor.instance.currentGameState = GameState.Win;
        }   
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Lilypad")
        {
            onLilypad = false;
        }
    }
}
