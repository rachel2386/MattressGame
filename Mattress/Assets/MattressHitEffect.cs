using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MattressHitEffect : MonoBehaviour
{
    [SerializeField] private AudioClip[] _hitSounds;

    AudioSource _audioSource;

    private void OnEnable()
    {
        MattressDyer.MattressHit += OnMattressHit;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        MattressDyer.MattressHit -= OnMattressHit;
    }

    public void OnMattressHit (RaycastHit raycastHit)
    {
        _audioSource.PlayOneShot(_hitSounds[UnityEngine.Random.Range(0, _hitSounds.Length)]);
    }
}
