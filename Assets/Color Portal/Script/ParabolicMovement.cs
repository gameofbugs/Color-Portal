using UnityEngine;
using System.Collections;

public class ParabolicMovement : MonoBehaviour
{
    public Transform targetPosition;
    public float duration = 1f;
    public float arcHeight = 3f;
    public float rotationSpeed = 360f;

    private bool isMoving = false;

    public void StartMovement()
    {
        if (targetPosition == null)
        {
            return;
        }

        if (isMoving)
        {
            return;
        }
        StartCoroutine(MoveInParabola(targetPosition.position, duration, arcHeight));
    }

    public void StartMovement(Vector3 target)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveInParabola(target, duration, arcHeight));
        }
    }

    public void StartMovement(Vector3 target, float customDuration, float customHeight)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveInParabola(target, customDuration, customHeight));
        }
    }

    private IEnumerator MoveInParabola(Vector3 target, float duration, float height)
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;

            Vector3 currentPos = Vector3.Lerp(startPosition, target, progress);
            float parabola = Mathf.Sin(progress * Mathf.PI);
            currentPos.y += parabola * height;

            transform.position = currentPos;
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }
}