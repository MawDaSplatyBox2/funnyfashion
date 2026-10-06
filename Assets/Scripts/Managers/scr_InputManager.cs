using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * Code Sources:
 * https://gamedevbeginner.com/how-to-move-an-object-with-the-mouse-in-unity-in-2d/
 */

public class InputManager : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    static public InputManager instance;
    public GameObject paintContent;
    public GameObject hoardContent;
    public Draggable selectedObject;
    [DisplayWithoutEdit] private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Cancel if instance exists
        if (instance)
        if (instance.isActiveAndEnabled)
        {
            Destroy(this);
            return;
        }

        // Set instance
        instance = this;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        TrySelectObject(mousePosition);
        TryDeselectObject(mousePosition);
        TryMoveObject(mousePosition);
    }

    #region Try Mouse Stuff
    private void TrySelectObject(Vector3 _mousePosition)
    {
        if (Input.GetMouseButtonDown(0) && Physics2D.OverlapPoint(_mousePosition))
        {
            Collider2D[] results = Physics2D.OverlapPointAll(_mousePosition);
            Draggable highestDraggable = GetHighestDraggable(results);

            if (highestDraggable)
            {
                // Confirm selection of object
                selectedObject = highestDraggable;

                offset = selectedObject.transform.position - _mousePosition;

                //selectedObject.SetCanvas(paintCanvas);
                selectedObject.transform.SetParent(paintContent.transform, true);
                selectedObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    private void TryDeselectObject(Vector3 _mousePosition)
    {
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            if (!selectedObject.IsDestroyed())
            {
                selectedObject.GetComponent<Collider2D>().enabled = true;
            }
            selectedObject = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
    }

    private void TryMoveObject(Vector3 _mousePosition)
    {
        if (selectedObject)
        {
            selectedObject.transform.position = _mousePosition + offset;
        }
    }

    Draggable GetHighestDraggable(Collider2D[] _results)
    {
        int highestValue = 0;
        Draggable highestDraggable = null;

        foreach (Collider2D col in _results)
        {
            // Ignore objects without Draggable class
            if (col.gameObject.GetComponent<Draggable>() == null) continue;

            // Check if Draggable is rendered above existing selection
            Canvas ren = col.gameObject.GetComponent<Draggable>().canvas;
            if (highestDraggable == null || (ren && ren.sortingOrder > highestValue))
            {
                highestValue = ren.sortingOrder;
                highestDraggable = col.gameObject.GetComponent<Draggable>();
            }
        }

        // Return highest Draggable found
        return highestDraggable;
    }
    #endregion
}
