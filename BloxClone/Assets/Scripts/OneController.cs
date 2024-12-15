using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

public class OneController : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveSpeed = 5f;
    public LayerMask obstacleLayer;
    public LayerMask spikeLayer;
    public MySceneManager MySceneManager;
    public AudioSource audioSource;
    public AudioSource damageSound;

    private Vector3 targetPosition;
    public int moveCount = 0;
    public bool isMoving = false;
    public bool isDead = false;
    public int maxMove = 37;

    public bool onSpike = false;
    private bool x = false;
    private SpikeScript spikeScript = null;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f && isDead == false)
        {
            isMoving = false;

            Vector3 moveDirection = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveDirection = new Vector3(0, 0, moveDistance);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveDirection = new Vector3(0, 0, -moveDistance);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveDirection = new Vector3(-moveDistance, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveDirection = new Vector3(moveDistance, 0, 0);
            }

            if (!IsObstacleInDirection(moveDirection, obstacleLayer) && moveDirection != Vector3.zero)
            {
                targetPosition += moveDirection;

                x = true;

                if (onSpike = true && !IsSpikeInDirection(moveDirection, spikeLayer))
                {
                    x = false;
                }
                
                maxMove--;
                moveCount++;

                Debug.Log("Liczba ruchów: " + maxMove);

                isMoving = true;
                CheckMaxMoves();
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (onSpike == true && x)
        {
            CheckSpike();
            x = false;
        }
    }

    private bool IsObstacleInDirection(Vector3 direction, LayerMask obstacleLayer)
    {
        Vector3 origin = transform.position;
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, obstacleLayer))
        {
            if (hit.collider.CompareTag("Boulder"))
            {
                BoulderScript boulder = hit.collider.GetComponent<BoulderScript>();
                if (boulder != null)
                {
                    boulder.TryMove(direction);
                    maxMove--;
                    moveCount++;
                    if (onSpike == true)
                    {
                        CheckSpike();
                    }
                }
            }
            else if (hit.collider.CompareTag("Enemy"))
            {
                EnemyScript enemy = hit.collider.GetComponent<EnemyScript>();
                if (enemy != null)
                {
                    enemy.TryMove(direction);
                    maxMove--;
                    moveCount++;
                    if (onSpike == true)
                    {
                        CheckSpike();
                    }
                }
            }
            return true;
        }
        return false;
    }

    private bool IsSpikeInDirection(Vector3 direction, LayerMask spikeLayer)
    {
        Vector3 origin = transform.position;
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, spikeLayer))
        {
            Debug.Log("Jest spike");
            if (hit.collider.CompareTag("Spike"))
            {
                SpikeScript spike = hit.collider.GetComponent<SpikeScript>();
            }
            return true;
        }
        return false;
    }

    private void CheckMaxMoves()
    {
        if (maxMove < 0)
        {
            audioSource.Play();
            isDead = true;

            var delay = 0.7f;
            
            Invoke(nameof(RestartScene), delay);
        }
    }

    private void RestartScene()
    {
        MySceneManager mySceneManager = FindObjectOfType<MySceneManager>();
        if (mySceneManager != null)
        {
            mySceneManager.Restart();
        }
        else
        {
            Debug.LogError("Nie znaleziono MySceneManager w scenie!");
        }
    }

    private void OnTriggerEnter (Collider collider)
    {
        if (collider.CompareTag("Spike"))
        {
            onSpike = true;
            spikeScript = collider.gameObject.GetComponent<SpikeScript>();
        }
        if (collider.CompareTag("Win"))
        {
            isDead = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("Wychodzi");
        if (collider.CompareTag("Spike"))
        {
            onSpike = false;
            spikeScript = null;
        }
    }

    public void CheckSpike()
    {
        Debug.Log($"spikeScript: {spikeScript}");
        if (spikeScript != null)
        {
            if (spikeScript.stepMode == SpikeScript.StepMode.Even && moveCount % 2 == 0)
            {
                Debug.Log("Gracz ma parzystą liczbę ruchów na spike.");
                damageSound.Play();
                maxMove--;
            }
            else if (spikeScript.stepMode == SpikeScript.StepMode.Odd && moveCount % 2 != 0)
            {
                Debug.Log("Gracz ma nieparzystą liczbę ruchów na spike.");
                damageSound.Play();
                maxMove--;
            }
            else if (spikeScript.stepMode == SpikeScript.StepMode.Always)
            {
                Debug.Log("Spike zawsze działa, niezależnie od liczby ruchów.");
                damageSound.Play();
                maxMove--;
            }
        }
    }
}