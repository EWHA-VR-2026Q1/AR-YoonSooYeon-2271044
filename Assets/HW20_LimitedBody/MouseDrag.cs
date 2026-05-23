using UnityEngine;

public class MouseDrag : MonoBehaviour
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
        Ray ray =
            Camera.main.ScreenPointToRay(
                Input.mousePosition
            );

        RaycastHit hit;

        // 클릭 시작
        if (Input.GetMouseButtonDown(0))
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

        // 드래그 중
        if (Input.GetMouseButton(0) && isDragging)
        {
            Plane plane =
                new Plane(Vector3.up, Vector3.zero);

            float distance;

            if (plane.Raycast(ray, out distance))
            {
                Vector3 point =
                    ray.GetPoint(distance);

                targetPosition = point;
            }
        }

        // 클릭 끝
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            GetComponent<Renderer>().material.color =
                Color.white;
        }

        // Snap
        float dist =
            Vector3.Distance(
                targetPosition,
                snapPoint.position
            );

        if (dist < snapDistance)
        {
            targetPosition =
                snapPoint.position;
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