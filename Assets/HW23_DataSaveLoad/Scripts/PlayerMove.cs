using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveDistance = 1f;
    public float rotateSpeed = 0.2f;

    private Vector2 lastTouchPos;
    private bool dragging = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPos = touch.position;
                dragging = true;
            }

            if (touch.phase == TouchPhase.Moved && dragging)
            {
                float deltaX =
                    touch.position.x - lastTouchPos.x;

                transform.Rotate(
                    0,
                    deltaX * rotateSpeed,
                    0);

                lastTouchPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                dragging = false;
            }
        }
    }

    public void MoveForward()
    {
        transform.position +=
            transform.forward * moveDistance;
    }
}