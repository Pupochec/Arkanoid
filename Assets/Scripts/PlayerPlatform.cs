using UnityEngine;

public class PlayerPlatform : MonoBehaviour
{
    public float speed = 8f;
    public float leftBoundary = -5f;
    public float rightBoundary = 5f;

    public Vector3 normalScale = new Vector3(0.25f, 0.2f, 0f);
    public Vector3 bigScale = new Vector3(0.4f, 0.2f, 0f);

    private Vector3 targetPosition;
    private float platformHalfWidth;

    private void Start()
    {
        CalculatePlatformHalfWidth();
    }

    private void CalculatePlatformHalfWidth()
    {
        platformHalfWidth = transform.localScale.x / 2f;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            targetPosition.y = transform.position.y;
            targetPosition.x = Mathf.Clamp(targetPosition.x, leftBoundary + platformHalfWidth, rightBoundary - platformHalfWidth);
        }
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
            targetPosition.z = 0;
            targetPosition.y = transform.position.y;
            targetPosition.x = Mathf.Clamp(targetPosition.x, leftBoundary + platformHalfWidth, rightBoundary - platformHalfWidth);
        }

        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    public void BigPlatformEffect(float bonusTime)
    {
        transform.localScale = bigScale;
        CalculatePlatformHalfWidth();
        Invoke(nameof(NormalScalePlatform), bonusTime);
    }

    private void NormalScalePlatform()
    {
        transform.localScale = normalScale;
        CalculatePlatformHalfWidth();
    }
}