using UnityEngine;

public class Draggable : MonoBehaviour
{
    [Header("Objects with this script class can be\ngrabbed and dragged with the mouse.")]
    public bool disableOnStart = true;
    [DisplayWithoutEdit] public Canvas canvas;
    [DisplayWithoutEdit] private bool isVisible;

    private void OnValidate()
    {
        GetComponents();
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
        if (!canvas) canvas = GetComponentInParent<Canvas>();
    }

    public void SetCanvas(Canvas _canvas)
    {
        canvas = _canvas;
        transform.SetParent(canvas.transform, true);
    }
}
