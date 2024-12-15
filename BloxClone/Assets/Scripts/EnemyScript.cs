using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveDistance = 1f;
    public LayerMask obstacleLayer;
    public float speed = 5f;

    public LayerMask spike;
    private SpikeScript spikeScript = null;
    public bool onSpike = false;
    public OneController playerController;
    public GameObject Audio;

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
        Vector3 targetPosition = transform.position + new Vector3(
            Mathf.Round(direction.x) * moveDistance,
            Mathf.Round(direction.y) * moveDistance,
            Mathf.Round(direction.z) * moveDistance
        );

        if (IsPositionFree(targetPosition))
        {
            StartCoroutine(MoveToPosition(targetPosition));
            return true;
        }
        else
        {
            AudioSource audioSource = Audio.GetComponent<AudioSource>();
            Debug.Log("Enemy nie mo¿e byæ przesuniêty. Eksterminacja.");
            audioSource.Play();
            Object.Destroy(gameObject);
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

    public void CheckSpike()
    {
        AudioSource audioSource = Audio.GetComponent<AudioSource>();
        if (spikeScript != null)
        {
            if (spikeScript.stepMode == SpikeScript.StepMode.Even && playerController.moveCount % 2 == 0)
            {
                Debug.Log("Enemy ma parzyst¹ liczbê ruchów na spike.");
                audioSource.Play();
                Destroy(gameObject);
            }
            else if (spikeScript.stepMode == SpikeScript.StepMode.Odd && playerController.moveCount % 2 != 0)
            {
                Debug.Log("Enemy ma nieparzyst¹ liczbê ruchów na spike.");
                audioSource.Play();
                Destroy(gameObject);
            }
            else if (spikeScript.stepMode == SpikeScript.StepMode.Always)
            {
                Debug.Log("Enemy zawsze dzia³a, niezale¿nie od liczby ruchów.");
                audioSource.Play();
                Destroy(gameObject);
            }
        }
    }
}
