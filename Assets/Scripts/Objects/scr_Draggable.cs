using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DraggableType
{
    Item,
    ScaleAnchor,
}
public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IScrollHandler
{
    [Header("Objects with this script class can be\ngrabbed and dragged with the mouse.")]
    public bool disableOnStart = true;
    public DraggableType draggableType = DraggableType.Item;
    [DisplayWithoutEdit] public bool isDragging = false;
    [Header("Components")]
    public ResizeImage resizableImage;
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

    #region Drag
    public void OnBeginDrag(PointerEventData _eventData)
    {
        Debug.Log("OnBeginDrag");
        // Change appearance and raycast effects
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        // Cache dragging state
        isDragging = true;
        // Move onto parent
        if (resetToParent) transform.SetParent(resetToParent, true);
        MoveSibling(_eventData.button);
    }

    public void OnDrag(PointerEventData _eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += _eventData.delta / canvas.scaleFactor; // Move position based on cursor movement, adjusted by canvas scale
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        Debug.Log("OnEndDrag");
        // Change appearance and raycast effects
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        // Cache dragging state
        isDragging = false;
        // See scr_Hoard.cs for changing parent on drop
        // Move onto parent
        if (dropOnParent) transform.parent = dropOnParent;
    }
    #endregion

    public void OnScroll(PointerEventData _eventData)
    {
        if (_eventData.scrollDelta != Vector2.zero)
        {
            resizableImage.scrollAmount += new Vector2(
                Mathf.Clamp(_eventData.scrollDelta.x, -1f, 1f),
                Mathf.Clamp(_eventData.scrollDelta.y, -1f, 1f)
                );
            if (resizableImage.scalingType == ResizeImageType.Classic) resizableImage.UpdateClassicScale();
        }
    }
    private void DoScroll(PointerEventData _eventData, string _debugstr)
    {
        Debug.Log("Scrolling " + _debugstr);
        if (_eventData.scrollDelta != Vector2.zero)
        {
            resizableImage.scrollAmount += _eventData.scrollDelta;
        }
    }
    public void OnPointerDown(PointerEventData _eventData)
    {
        Debug.Log("OnPointerDown");
        MoveSibling(_eventData.button);
        switch (_eventData.button)
        {
            case PointerEventData.InputButton.Left:
                transform.SetAsLastSibling();
                break;
            case PointerEventData.InputButton.Right:
                transform.SetAsFirstSibling();
                break;
        }

    }

    public void OnDrop(PointerEventData _eventData)
    {
        Debug.Log("OnDrop");
    }

    private void MoveSibling(PointerEventData.InputButton _button)
    {
        switch (_button)
        {
            case PointerEventData.InputButton.Left:
                transform.SetAsLastSibling();
                break;
            case PointerEventData.InputButton.Right:
                transform.SetAsFirstSibling();
                break;
        }
    }
}
