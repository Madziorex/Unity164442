using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderScript : MonoBehaviour
{
    public float moveDistance = 1f;
    public LayerMask obstacleLayer;
    public float speed = 5f;
    public AudioSource Audio;

    public bool TryMove(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + new Vector3(
            Mathf.Round(direction.x) * moveDistance,
            Mathf.Round(direction.y) * moveDistance,
            Mathf.Round(direction.z) * moveDistance
        );

        if (IsPositionFree(targetPosition))
        {
            Audio.Play();
            StartCoroutine(MoveToPosition(targetPosition));
            return true;
        }
        else
        {
            Debug.Log("Boulder nie mo¿e byæ przesuniêty. Pozycja zajêta.");
            return false;
        }
    }

    private bool IsPositionFree(Vector3 targetPosition)
    {
        Debug.Log($"Sprawdzanie pozycji: {targetPosition}");

        Collider[] colliders = Physics.OverlapBox(
            targetPosition,
            transform.localScale / 2 * 0.9f,
            Quaternion.identity,
            obstacleLayer
        );

        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                Debug.Log($"Kolizja z: {collider.name}");
            }
            return false;
        }

        return true;
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        float moveDuration = moveDistance / speed;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
