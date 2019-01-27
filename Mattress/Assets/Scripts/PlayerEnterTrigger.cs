using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PlayerEnterTrigger : MonoBehaviour
{
    public UnityEvent PlayerEnter;
    public UnityEvent PlayerExit;

    private void OnEnable()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            PlayerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            PlayerExit?.Invoke();
        }
    }
}
