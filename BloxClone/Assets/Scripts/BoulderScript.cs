using UnityEngine;

public class BoulderScript : MonoBehaviour
{
    public float moveDistance = 1f; // Odleg³oœæ przesuniêcia bouldera
    public LayerMask obstacleLayer; // Warstwa przeszkód
    public float speed = 5f; // Prêdkoœæ ruchu bouldera

    public bool TryMove(Vector3 direction)
    {
        // Wylicz now¹ pozycjê w oparciu o aktualn¹ pozycjê i dok³adny kierunek
        Vector3 targetPosition = transform.position + new Vector3(
            Mathf.Round(direction.x) * moveDistance,
            Mathf.Round(direction.y) * moveDistance,
            Mathf.Round(direction.z) * moveDistance
        );

        // SprawdŸ, czy nowa pozycja jest wolna
        if (IsPositionFree(targetPosition))
        {
            StartCoroutine(MoveToPosition(targetPosition));
            return true; // Ruch mo¿liwy
        }
        else
        {
            Debug.Log("Boulder nie mo¿e byæ przesuniêty. Pozycja zajêta.");
            return false; // Ruch niemo¿liwy
        }
    }

    private bool IsPositionFree(Vector3 targetPosition)
    {
        Debug.Log($"Sprawdzanie pozycji: {targetPosition}");

        Collider[] colliders = Physics.OverlapBox(
            targetPosition,
            transform.localScale / 2 * 0.9f, // Nieco mniejszy box dla bezpieczeñstwa
            Quaternion.identity,
            obstacleLayer
        );

        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                Debug.Log($"Kolizja z: {collider.name}");
            }
            return false; // Miejsce zajête
        }

        return true; // Brak przeszkód
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        float moveDuration = moveDistance / speed; // Czas ruchu bazuj¹cy na odleg³oœci i prêdkoœci

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ustaw dok³adn¹ pozycjê
    }
}
