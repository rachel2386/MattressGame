using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Player;

    public Collider BaseCollider { get; private set; }

    [Header("Movement Settings")]
    [SerializeField] private float _stepLength = 0.05f;
    /*
    [SerializeField] private float _forwardForceFactor = 100000f;
    [SerializeField] private float _rightForceFactor = 100000f;
    [SerializeField] private float _movementGyroThreshold = 0.08f;
    */
    [Header("Rotation Settings")]
    [SerializeField] Vector2 _yawRange = new Vector2(-3.13f, 3.13f);

    private Rigidbody _rigibody;
    private object _movementTweenTarget = new object();

    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody>();
        
        if (Player == null)
        {
            Player = this;
        }

        BaseCollider = GetComponent<Collider>();
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
        Vector3 walkDirection = Vector3.zero;
        if (Mathf.Abs(gyroVector.x) > Mathf.Abs(gyroVector.z))
        {
            if (gyroVector.x > 0)
            {
                walkDirection = -transform.forward;
            }
            else
            {
                walkDirection = transform.forward;
            }
        }
        else
        {
            if (gyroVector.z > 0)
            {
                walkDirection = transform.right;
            }
            else
            {
                walkDirection = -transform.right;
            }
        }

        walkDirection.y = 0;


        /*
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
        */

        float y = Mathf.Lerp(180f, -180f, (gyroVector.y - _yawRange.x) / (_yawRange.y - _yawRange.x));
        DOTween.Kill(_movementTweenTarget);
        _rigibody.DOMove(_rigibody.position + walkDirection.normalized * _stepLength, interval).SetEase(Ease.Linear).SetTarget(_movementTweenTarget);
        _rigibody.DORotate(Vector3.up * y, interval, RotateMode.Fast).SetEase(Ease.Linear).SetTarget(_movementTweenTarget);

        if (!DOTween.IsTweening(Camera.main.transform, true))
        {
            Camera.main.transform.DOLocalJump(Camera.main.transform.localPosition, 0.01f, 1, interval * 6f).SetEase(Ease.Linear);
        }
    }
}
