using System.Collections;
using UnityEngine;

public class ScreenTopDown : MonoBehaviour
{
    [SerializeField] private RectTransform topPanel;
    [SerializeField] private RectTransform bottomPanel;
    private float moveDuration = 1.2f;
    [SerializeField] private bool CloseStart;

    private Vector2 topOpenPos;
    private Vector2 topClosePos;
    private Vector2 bottomOpenPos;
    private Vector2 bottomClosePos;
    public float Duration => moveDuration;

    private void Awake()
    {
        topOpenPos = new Vector2(0, topPanel.rect.height);
        topClosePos = Vector2.zero;

        bottomOpenPos = new Vector2(0, -bottomPanel.rect.height);
        bottomClosePos = Vector2.zero;


        if(CloseStart)
        {
            topPanel.anchoredPosition = topClosePos;
            bottomPanel.anchoredPosition = bottomClosePos;
        }
        else
        {
            topPanel.anchoredPosition = topOpenPos;
            bottomPanel.anchoredPosition = bottomOpenPos;
        }
    }
    private void Start()
    {
        if(CloseStart)
        {
            StartCoroutine(Open());
        }
    }
    public IEnumerator Close()
    {
        float elapsed = 0f;

        Vector2 topStart = topPanel.anchoredPosition;
        Vector2 bottomStart = bottomPanel.anchoredPosition;

        while (elapsed < moveDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / moveDuration;

            topPanel.anchoredPosition = Vector2.Lerp(topStart, topClosePos, t);
            bottomPanel.anchoredPosition = Vector2.Lerp(bottomStart, bottomClosePos, t);

            yield return null;
        }

        topPanel.anchoredPosition = topClosePos;
        bottomPanel.anchoredPosition = bottomClosePos;
    }

    public IEnumerator Open()
    {
        float elapsed = 0f;

        Vector2 topStart = topPanel.anchoredPosition;
        Vector2 bottomStart = bottomPanel.anchoredPosition;

        while (elapsed < moveDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / moveDuration;

            topPanel.anchoredPosition = Vector2.Lerp(topStart, topOpenPos, t);
            bottomPanel.anchoredPosition = Vector2.Lerp(bottomStart, bottomOpenPos, t);

            yield return null;
        }

        topPanel.anchoredPosition = topOpenPos;
        bottomPanel.anchoredPosition = bottomOpenPos;
    }
}