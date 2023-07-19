using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FroggyNamespace;

public class MovePlayer : MonoBehaviour
{
    Vector2 jumpDirection = Vector2.zero;
    public bool onLilypad = true;
    Transform activeLilypadTransform = null;
    bool gameWon = false;
    
    [SerializeField]
    float stepSize;

    void Start()
    {
        
    }

    void Update()
    {
        if(onLilypad)
        {
            jumpDirection = Vector2.zero;

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
        }
    }

    public void Jump()
    {
        if(onLilypad)
        {
            transform.localPosition = new Vector3(
                transform.localPosition.x + jumpDirection.x * stepSize,
                transform.localPosition.y + jumpDirection.y * stepSize,
                transform.localPosition.z
            );
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Lilypad")
        {
            onLilypad = true; 
            activeLilypadTransform = other.gameObject.transform;
        }
        else if (other.gameObject.tag == "Finish")
        {
            // gameWon = true;
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
