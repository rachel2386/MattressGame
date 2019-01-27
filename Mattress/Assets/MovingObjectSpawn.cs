using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingObjectSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectList;
    [SerializeField] private float _width = 0;
    [SerializeField] private float _movingDistance = 50f;
    [SerializeField] private float _movingSpeed = 2f;
    [SerializeField] private float _spawnMaxInterval = 7.0f;
    [SerializeField] private float _spawnIntervalRandomness = 3.0f;

    private float _timer = 0;
    private float _randomIntervalOffset;

    private void Start()
    {
        _randomIntervalOffset = Random.Range(0f, _spawnIntervalRandomness);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _spawnMaxInterval - _randomIntervalOffset)
        {
            _timer = 0;
            _randomIntervalOffset = Random.Range(0f, _spawnIntervalRandomness);
            GameObject movingObject = Instantiate(_objectList[Random.Range(0, _objectList.Length)], transform.position + transform.right * Random.Range(-_width/2, _width/2), transform.rotation);
            movingObject.transform.DOMove(transform.position + transform.forward * _movingDistance, _movingDistance / _movingSpeed)
                .OnComplete(() => { Destroy(movingObject); });
        }
    }
}
