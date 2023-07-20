using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLeafDirection : MonoBehaviour
{
    public bool isActive = false;

    [SerializeField]
    private bool randomizeAngle = true;

    void Start()
    {
        if(randomizeAngle) transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Floor(Random.Range(0f, 3f)) * 90f);
    }

    void Update()
    {
        if(isActive)
        {
            if(Input.GetAxisRaw("Horizontal") != 0) {
                transform.localEulerAngles = new Vector3(0f, 0f, -Input.GetAxisRaw("Horizontal") * 90f);
            }
            if(Input.GetAxisRaw("Vertical") != 0) {
                transform.localEulerAngles = new Vector3(0f, 0f, 90f - Input.GetAxisRaw("Vertical") * 90f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            isActive = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            isActive = false;
        }
    }
}
