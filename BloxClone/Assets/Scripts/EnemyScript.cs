using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveDistance = 1f; // Odleg�o�� przesuni�cia wroga
    public LayerMask obstacleLayer; // Warstwa przeszk�d
    public float speed = 5f;        // Pr�dko�� ruchu wroga

    public LayerMask spike;
    private SpikeScript spikeScript = null;
    public bool onSpike = false;
    public OneController playerController;

    private void Update()
    {
        if (onSpike)
        {
            CheckSpike();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Spike"))
        {
            onSpike = true;
            spikeScript = collider.gameObject.GetComponent<SpikeScript>();
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Spike"))
        {
            onSpike = true;
            spikeScript = collider.gameObject.GetComponent<SpikeScript>();
        }
    }

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
            Debug.Log("Enemy nie mo�e by� przesuni�ty. Eksterminacja.");
            Object.Destroy(gameObject);
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

    public void CheckSpike()
    {
        Debug.Log($"spikeScript: {spikeScript}");
        // Sprawd� liczb� ruch�w gracza z aktualnego stepMode w SpikeScript
        if (spikeScript != null) // Upewnij si�, �e spikeScript nie jest null
        {
            if (spikeScript.stepMode == SpikeScript.StepMode.Even && playerController.moveCount % 2 == 0)
            {
                Debug.Log("Enemy ma parzyst� liczb� ruch�w na spike.");
                Destroy(gameObject);
            }
            else if (spikeScript.stepMode == SpikeScript.StepMode.Odd && playerController.moveCount % 2 != 0)
            {
                Debug.Log("Enemy ma nieparzyst� liczb� ruch�w na spike.");
                Destroy(gameObject);
            }
            else if (spikeScript.stepMode == SpikeScript.StepMode.Always)
            {
                Debug.Log("Enemy zawsze dzia�a, niezale�nie od liczby ruch�w.");
                Destroy(gameObject);
            }
        }
    }
}
