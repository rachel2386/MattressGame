using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActiveSwitch : MonoBehaviour
{
    [SerializeField] float _switchInterval = 20.0f;

    [SerializeField] GameObject[] _groupA;
    [SerializeField] GameObject[] _groupB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnableObjectGroup (GameObject[] group)
    {
        foreach(GameObject go in group)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
