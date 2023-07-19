using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform targetTransform;

    void Update()
    {
        transform.localPosition = new Vector3(
            targetTransform.localPosition.x,
            targetTransform.localPosition.y,
            transform.localPosition.z
        );
    }
}
