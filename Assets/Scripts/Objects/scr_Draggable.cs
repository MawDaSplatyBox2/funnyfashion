using UnityEngine;

public class Draggable : MonoBehaviour
{
    [Header("Objects with this script class can be\ngrabbed and dragged with the mouse.")]
    public bool disableOnStart = true;

    private void Start()
    {
        if (disableOnStart)
        {
            enabled = false;
        }
    }
}
