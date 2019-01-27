using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class MattressHitEffect : MonoBehaviour
{
    [SerializeField] private AudioClip[] _hitSounds;
    [SerializeField] private Sprite[] _overlayDirtList;
    [SerializeField] private CanvasGroup _dirtOverlayCanvas;
    [SerializeField] private Image _leftDirtImage;
    [SerializeField] private Image _rightDirtImage;

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
        Vector3 rayDir = raycastHit.point - PlayerMovement.Player.transform.position;
        if (Vector3.Dot(rayDir, PlayerMovement.Player.transform.right) > 0)
        {
            _leftDirtImage.gameObject.SetActive(false);
            _rightDirtImage.gameObject.SetActive(true);
        }
        else
        {
            _leftDirtImage.gameObject.SetActive(true);
            _rightDirtImage.gameObject.SetActive(false);
        }
        _leftDirtImage.sprite = _overlayDirtList[Random.Range(0, _overlayDirtList.Length)];
        _leftDirtImage.sprite = _overlayDirtList[Random.Range(0, _overlayDirtList.Length)];

        _dirtOverlayCanvas.alpha = 1;
        _dirtOverlayCanvas.DOKill();
        _dirtOverlayCanvas.DOFade(0, 0.5f);
    }
}
