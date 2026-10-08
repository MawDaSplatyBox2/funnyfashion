using UnityEngine;
using UnityEngine.EventSystems;

public class Hoard : MonoBehaviour, IDropHandler
{
    public Transform hoardHoard;

    public void OnDrop(PointerEventData _eventData)
    {
        Debug.Log("OnDrop");
        if (_eventData.pointerDrag != null)
        {
            //_eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            if (_eventData.pointerDrag.gameObject.GetComponent<Draggable>().hoardable)
            _eventData.pointerDrag.transform.SetParent(hoardHoard, true);
        }
    }
}
