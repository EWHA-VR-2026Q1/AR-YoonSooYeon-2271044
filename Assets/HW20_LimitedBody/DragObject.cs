using UnityEngine;

public class DragObject : MonoBehaviour
{
    private bool isDragging = false;

    private Vector3 targetPosition;

    public Transform snapPoint;

    public float snapDistance = 0.2f;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        // 터치 시작
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Ray ray =
                Camera.main.ScreenPointToRay(touch.position);

            RaycastHit hit;

            // Tap
            if (touch.phase == TouchPhase.Began)
            {
                if (
                    Physics.Raycast(ray, out hit) &&
                    hit.collider.gameObject == gameObject
                )
                {
                    isDragging = true;

                    GetComponent<Renderer>().material.color =
                        Color.red;
                }
            }

            // Drag
            if (
                touch.phase == TouchPhase.Moved &&
                isDragging
            )
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                float distance;

                if (plane.Raycast(ray, out distance))
                {
                    Vector3 point = ray.GetPoint(distance);

                    targetPosition = point;
                }
            }

            // 손 떼기
            if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;

                GetComponent<Renderer>().material.color =
                    Color.white;
            }
        }

        // Snap
        float dist =
            Vector3.Distance(
                targetPosition,
                snapPoint.position
            );

        if (dist < snapDistance)
        {
            targetPosition = snapPoint.position;
        }

        // Lerp
        transform.position =
            Vector3.Lerp(
                transform.position,
                targetPosition,
                Time.deltaTime * 5f
            );
    }
}