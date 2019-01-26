using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Player;

    [Header("Movement Settings")]
    [SerializeField] private float _forwardForceFactor = 100000f;
    [SerializeField] private float _rightForceFactor = 100000f;
    [SerializeField] private float _movementGyroThreshold = 0.08f;
    [Header("Rotation Settings")]
    [SerializeField] Vector2 _yawRange = new Vector2(-3.13f, 3.13f);

    private Rigidbody _rigibody;

    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody>();
        
        if (Player == null)
        {
            Player = this;
        }
    }

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
        if (Mathf.Abs(gyroVector.x) > _movementGyroThreshold)
        {
            float forwardBeyongThreshold = (gyroVector.x / Mathf.Abs(gyroVector.x)) * (Mathf.Abs(gyroVector.x) - _movementGyroThreshold);
            _rigibody.AddForce(-forwardBeyongThreshold * transform.forward * _forwardForceFactor, ForceMode.Force);
        }

        if (Mathf.Abs(gyroVector.z) > _movementGyroThreshold)
        {
            float rightBeyongThreshold = (gyroVector.z / Mathf.Abs(gyroVector.z)) * (Mathf.Abs(gyroVector.z) - _movementGyroThreshold);
            _rigibody.AddForce(rightBeyongThreshold * transform.right * _rightForceFactor, ForceMode.Force);
        }

        float y = Mathf.Lerp(180f, -180f, (gyroVector.y - _yawRange.x) / (_yawRange.y - _yawRange.x));
        _rigibody.DOKill();
        _rigibody.DORotate(Vector3.up * y, interval, RotateMode.Fast).SetEase(Ease.Linear);
    }
}
