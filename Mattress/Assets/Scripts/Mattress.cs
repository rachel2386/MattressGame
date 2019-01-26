using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mattress : MonoBehaviour
{
    [SerializeField] Vector2 _pinchRange = new Vector2(-3.13f, 3.13f);
    [SerializeField] Vector2 _rollRange = new Vector2(-3.13f, 3.13f);
    [SerializeField] Vector2 _yawRange = new Vector2(-3.13f, 3.13f);

    private void OnEnable()
    {
        UDPListenser.UDPReceived += OnGyroDataReceived;
    }

    private void OnDisable()
    {
        UDPListenser.UDPReceived -= OnGyroDataReceived;
    }

    private void OnGyroDataReceived(string dataString, float interval)
    {
        string[] latestData = dataString.Split(',');
        float z = Mathf.Lerp(180f, -180f, (float.Parse(latestData[1]) - _rollRange.x) / (_rollRange.y - _rollRange.x));
        float y = Mathf.Lerp(180f, -180f, (float.Parse(latestData[3]) - _yawRange.x) / (_yawRange.y - _yawRange.x));
        float x = Mathf.Lerp(180f, -180f, (float.Parse(latestData[2]) - _pinchRange.x) / (_pinchRange.y - _pinchRange.x));

        if (interval > 0.1f)
        {
            transform.DORotate(new Vector3(x, y, z), interval, RotateMode.Fast).SetEase(Ease.Linear);
        }
        else
        {
            transform.rotation = Quaternion.Euler(x, y, z);
        }
    }
}
