using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DraggableType
{
    Item,
    ScaleAnchor,
}
public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [Header("Objects with this script class can be\ngrabbed and dragged with the mouse.")]
    public bool disableOnStart = true;
    public DraggableType draggableType = DraggableType.Item;
    [Header("Components")]
    public Transform resetToParent;
    public Transform dropOnParent;
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
        if (!resetToParent) resetToParent = transform.parent;
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
        // Change appearance and raycast effects
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        // Move onto parent
        if (resetToParent) transform.SetParent(resetToParent, true);
        transform.SetAsLastSibling(); // This also moves on top of other instances on the same sorting layer
    }

    public void OnDrag(PointerEventData _eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += _eventData.delta / canvas.scaleFactor; // Move position based on cursor movement, adjusted by canvas scale

        switch (draggableType)
        {
            case DraggableType.ScaleAnchor:

                break;
        }
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        Debug.Log("OnEndDrag");
        // Change appearance and raycast effects
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        // See scr_Hoard.cs for changing parent on drop
        // Move onto parent
        if (dropOnParent) transform.parent = dropOnParent;
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
