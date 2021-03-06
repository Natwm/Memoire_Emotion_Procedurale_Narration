using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] private float unZoomValue;
    [SerializeField] private float zoomValue;

    [SerializeField] private Vector3 resetPosition;

    public float time;
    public float strengh;
    public int vibrato;
    public float random;

    public GameObject terminal;

    public float UnZoomValue { get => unZoomValue; set => unZoomValue = value; }

    void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instance of same Singleton : CameraManager");
        else
            instance = this;
    }

    private void Start()
    {
        UnZoomValue = Camera.main.orthographicSize;
        resetPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            terminal.SetActive(!terminal.activeSelf);
        }
    }

    public void ResetPosition()
    {
        transform.DOMove(resetPosition, 0.5f);
        LerpZoomFunction(unZoomValue, 1);
    }

    public IEnumerator MoveCameraToTarget(Vector3 PlayerPostion, float speed = 1f)
    {
        Vector3 targetPosition = new Vector3(PlayerPostion.x, PlayerPostion.y, -10f);
        transform.DOMove(targetPosition,speed);
        yield return new WaitForSeconds(speed * 1 / 3);
        StartCoroutine(LerpZoomFunction(zoomValue, speed));
        yield return new WaitForSeconds(speed * 2 / 3);
    }

    public IEnumerator LerpZoomFunction(float endValue, float duration)
    {
        float time = 0;
        float startValue = Camera.main.orthographicSize;

        while (time < duration)
        {
            Camera.main.orthographicSize = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        Camera.main.orthographicSize = endValue;
    }

    public void ShakeFeedback() 
    {
        transform.DOShakeRotation(time, strengh, vibrato, random, true);
        CanvasManager.instance.SelectedCharacterPanel.transform.parent.GetComponent<RectTransform>().DOShakePosition(time, strengh + 72, vibrato, random, true);
    }
}
