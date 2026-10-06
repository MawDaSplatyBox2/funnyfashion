using UnityEngine;

/*
 * Code Sources:
 * https://gamedevbeginner.com/how-to-move-an-object-with-the-mouse-in-unity-in-2d/
 */

public class InputManager : MonoBehaviour
{
    public GameObject selectedObject;
    [DisplayWithoutEdit] private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        TrySelectObject(mousePosition);
        TryDeselectObject();
        TryMoveObject(mousePosition);
    }

    #region Try Mouse Stuff
    private void TrySelectObject(Vector3 _mousePosition)
    {
        if (Input.GetMouseButtonDown(0) && Physics2D.OverlapPoint(_mousePosition))
        {
            Collider2D[] results = Physics2D.OverlapPointAll(_mousePosition);
            Collider2D highestCollider = GetHighestDraggable(results);

            if (highestCollider)
            {
                selectedObject = highestCollider.transform.gameObject;

                offset = selectedObject.transform.position - _mousePosition;
            }
        }
    }

    private void TryDeselectObject()
    {
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }
    }

    private void TryMoveObject(Vector3 _mousePosition)
    {
        if (selectedObject)
        {
            selectedObject.transform.position = _mousePosition + offset;
        }
    }

    Collider2D GetHighestDraggable(Collider2D[] _results)
    {
        int highestValue = 0;
        Collider2D highestDraggable = null;

        foreach (Collider2D col in _results)
        {
            // Ignore objects without Draggable class
            if (col.gameObject.GetComponent<Draggable>() == null) continue;

            // Check if Draggable is rendered above existing selection
            Renderer ren = col.gameObject.GetComponent<Renderer>();
            if (highestDraggable == null || (ren && ren.sortingOrder > highestValue))
            {
                highestValue = ren.sortingOrder;
                highestDraggable = col;
            }
        }

        // Return highest Draggable found
        return highestDraggable;
    }
    #endregion
}
