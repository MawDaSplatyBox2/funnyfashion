using UnityEngine;

public class Hoard : MonoBehaviour
{
    public Canvas canvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        Debug.Log("collision detected");
        Draggable draggable = _collision.gameObject.GetComponent<Draggable>();
        if (draggable)
        {
            draggable.SetCanvas(this.canvas);
        }
    }
}
