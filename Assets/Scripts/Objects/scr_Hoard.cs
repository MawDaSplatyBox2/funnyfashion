using UnityEngine;
using UnityEngine.EventSystems;

public class Hoard : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData _eventData)
    {
        Debug.Log("OnDrop");
    }
}
