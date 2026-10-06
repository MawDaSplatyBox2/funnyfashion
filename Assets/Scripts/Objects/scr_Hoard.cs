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
            _eventData.pointerDrag.transform.SetParent(hoardHoard, true);
        }
    }
}
