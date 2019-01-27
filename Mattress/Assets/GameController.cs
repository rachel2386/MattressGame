using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private LayerMask _endGameLayerMask;

    private void Awake()
    {
        Instance = this;
    }

    public void EndGame ()
    {
        Camera.main.cullingMask = _endGameLayerMask;
        MattressGyroRotation.Instance.enabled = false;
        Camera.main.transform.SetParent(null);
        Destroy(PlayerMovement.Player.gameObject);
        MattressGyroRotation.Instance.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3 + Vector3.up * 0.3f;
        MattressGyroRotation.Instance.transform.DORotate(Vector3.up * 360f, 10.0f, RotateMode.LocalAxisAdd).SetLoops(-1);
    }

    public void StartGame ()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }
}
