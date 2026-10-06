using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [Header("Objects with this script class can be\ngrabbed and dragged with the mouse.")]
    public bool disableOnStart = true;
    [Header("Components")]
    [DisplayWithoutEdit] public Canvas canvas;
    [DisplayWithoutEdit] public RectTransform rectTransform;
    [DisplayWithoutEdit] public CanvasGroup canvasGroup;
    //[DisplayWithoutEdit] private bool isVisible;

    private void OnValidate()
    {
        //GetComponents();
    }
    private void Start()
    {
        GetComponents();
        if (disableOnStart)
        {
            enabled = false;
        }
    }

    void GetComponents()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetCanvas(Canvas _canvas)
    {
        canvas = _canvas;
        transform.SetParent(canvas.transform, true);
    }

    public void OnBeginDrag(PointerEventData _eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData _eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += _eventData.delta / canvas.scaleFactor; // Move position based on cursor movement, adjusted by canvas scale
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnDrop(PointerEventData _eventData)
    {
        Debug.Log("OnDrop");
    }
}
