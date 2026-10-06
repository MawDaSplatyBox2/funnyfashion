using UnityEngine;

public class ImageCollider2D : MonoBehaviour
{
    //public Collider2D collider;
    public bool refresh;

    private void OnValidate()
    {
        Util.SetBoxCollider(gameObject);
        refresh = false;
    }

    private void Start()
    {
        Util.SetBoxCollider(gameObject);
        refresh = false;
    }
}
