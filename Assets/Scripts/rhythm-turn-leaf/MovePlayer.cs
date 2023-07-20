using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FroggyNamespace;

public class MovePlayer : MonoBehaviour
{
    Vector3 jumpDirection = Vector3.zero;
    public bool onLilypad = true;
    public bool hopping = false;
    Transform activeLilypadTransform = null;
    
    [SerializeField]
    float stepSize;

    private Animator anim;
    private float hopFrame;
    private float systemToSpriteFPS;    
    private float numHopFrames; // num of frames in sequence *TIMES* sample rate of animation clip 

    void Start()
    {
        anim = GetComponent<Animator>();
        // jumpDirection = new Vector3(1f, 0f, 0f);

        hopFrame = 99999;
    }

    void Update()
    {

        systemToSpriteFPS = (1f / Time.deltaTime) / 8f; // TODO change 8f into get clip's fps
        numHopFrames = 7f * systemToSpriteFPS;

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
        
        if(hopFrame < numHopFrames)
        {
            float startHopFrame = 3f * systemToSpriteFPS;
            float endHopFrame = 5f * systemToSpriteFPS;
            float startToEndFrameDuration = endHopFrame - startHopFrame - 1;
            if(hopFrame >= startHopFrame && hopFrame < endHopFrame) // defines #4-6 frames
            {
                transform.Translate(jumpDirection * ( stepSize / startToEndFrameDuration ), Space.World);
            }
            hopFrame += 1f;
        }
        
    }

    public void Jump()
    {
        if(!hopping)
        {
            anim.SetTrigger("hopping");
            StartCoroutine(MoveOneUnit());
        }
    }

    IEnumerator MoveOneUnit()
    {
        hopFrame = 0f;
        hopping = true;
        yield return new WaitWhile(() => hopFrame < numHopFrames); // 7 = num of frames in hop sequence
        hopping = false;
        Debug.Log("finish jumping");
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
