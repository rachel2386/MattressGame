using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSync : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = _targetTransform.rotation;
        transform.position = _targetTransform.position;
    }
}
