using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActiveSwitch : MonoBehaviour
{
    [SerializeField] float _switchInterval = 30.0f;
    [SerializeField] float _waitTime = 12.0f;

    [SerializeField] GameObject[] _groupA;
    [SerializeField] GameObject[] _groupB;
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(ObjectActiveSwitchCoroutine(true));
    }

    public void SetObjectGroupActive (GameObject[] group, bool active)
    {
        foreach(GameObject go in group)
        {
            go.SetActive(active);
        }
    }

    private IEnumerator ObjectActiveSwitchCoroutine (bool activeA)
    {
        SetObjectGroupActive(_groupA, activeA);
        SetObjectGroupActive(_groupB, !activeA);

        yield return new WaitForSeconds(_switchInterval);

        SetObjectGroupActive(_groupA, !activeA);

        yield return new WaitForSeconds(_waitTime);

        SetObjectGroupActive(_groupB, activeA);

        yield return new WaitForSeconds(_switchInterval);

        SetObjectGroupActive(_groupB, !activeA);

        yield return new WaitForSeconds(_waitTime);

        StartCoroutine(ObjectActiveSwitchCoroutine(activeA));
    }
}
