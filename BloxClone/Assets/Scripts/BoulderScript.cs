using UnityEngine;

public class BoulderScript : MonoBehaviour
{
    public float moveDistance = 1f; // Odleg�o�� przesuni�cia bouldera
    public LayerMask obstacleLayer; // Warstwa przeszk�d
    public float speed = 5f; // Pr�dko�� ruchu bouldera

    public bool TryMove(Vector3 direction)
    {
        // Wylicz now� pozycj� w oparciu o aktualn� pozycj� i dok�adny kierunek
        Vector3 targetPosition = transform.position + new Vector3(
            Mathf.Round(direction.x) * moveDistance,
            Mathf.Round(direction.y) * moveDistance,
            Mathf.Round(direction.z) * moveDistance
        );

        // Sprawd�, czy nowa pozycja jest wolna
        if (IsPositionFree(targetPosition))
        {
            StartCoroutine(MoveToPosition(targetPosition));
            return true; // Ruch mo�liwy
        }
        else
        {
            Debug.Log("Boulder nie mo�e by� przesuni�ty. Pozycja zaj�ta.");
            return false; // Ruch niemo�liwy
        }
    }

    private bool IsPositionFree(Vector3 targetPosition)
    {
        Debug.Log($"Sprawdzanie pozycji: {targetPosition}");

        Collider[] colliders = Physics.OverlapBox(
            targetPosition,
            transform.localScale / 2 * 0.9f, // Nieco mniejszy box dla bezpiecze�stwa
            Quaternion.identity,
            obstacleLayer
        );

        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                Debug.Log($"Kolizja z: {collider.name}");
            }
            return false; // Miejsce zaj�te
        }

        return true; // Brak przeszk�d
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        float moveDuration = moveDistance / speed; // Czas ruchu bazuj�cy na odleg�o�ci i pr�dko�ci

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ustaw dok�adn� pozycj�
    }
}
