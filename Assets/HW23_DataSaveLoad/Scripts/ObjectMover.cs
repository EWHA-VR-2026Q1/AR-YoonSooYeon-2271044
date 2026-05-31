using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    void OnMouseDown()
    {
        transform.position += Vector3.right;
    }
}