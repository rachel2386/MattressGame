using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MattressGyroRotation : MonoBehaviour
{
    [SerializeField] Vector2 _pinchRange = new Vector2(-3.13f, 3.13f);
    [SerializeField] Vector2 _rollRange = new Vector2(-3.13f, 3.13f);

    private void OnEnable()
    {
        UDPListenser.UDPReceived += OnGyroDataReceived;
    }

    private void OnDisable()
    {
        UDPListenser.UDPReceived -= OnGyroDataReceived;
    }

    private void OnGyroDataReceived(Vector3 gyroVector, float interval)
    {
        float z = Mathf.Lerp(180f, -180f, (gyroVector.z - _rollRange.x) / (_rollRange.y - _rollRange.x));
        //float y = Mathf.Lerp(180f, -180f, (gyroVector.y - _yawRange.x) / (_yawRange.y - _yawRange.x));
        float x = Mathf.Lerp(180f, -180f, (gyroVector.x - _pinchRange.x) / (_pinchRange.y - _pinchRange.x));

        DOTween.Kill(transform);
        transform.DOLocalRotate(new Vector3(x, 0, z), interval, RotateMode.Fast).SetEase(Ease.Linear);

        /*
        if (interval > 0.1f)
        {
            transform.DORotate(new Vector3(x, y, z), interval, RotateMode.Fast).SetEase(Ease.Linear);
        }
        else
        {
            transform.rotation = Quaternion.Euler(x, y, z);
        }
        */
    }
}
