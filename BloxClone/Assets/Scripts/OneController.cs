using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

public class OneController : MonoBehaviour
{
    public float moveDistance = 1f; // Odległość ruchu na krok
    public float moveSpeed = 5f;    // Prędkość ruchu
    public LayerMask obstacleLayer; // Warstwa przeszkód
    public LayerMask spikeLayer;

    private Vector3 targetPosition; // Docelowa pozycja
    public int moveCount = 0;      // Licznik ruchów
    public bool isMoving = false;  // Czy obiekt aktualnie się porusza
    public int maxMove = 37;

    public bool onSpike = false;
    private bool x = false;
    private SpikeScript spikeScript = null;

    public Text moveCounterText;    // Referencja do tekstu licznika (opcjonalne)

    void Start()
    {
        // Inicjalna pozycja celu to aktualna pozycja
        targetPosition = transform.position;

        // Aktualizacja tekstu licznika na UI (jeśli dostępny)
        UpdateMoveCounter();
    }

    void Update()
    {
        // Sprawdzaj, czy obiekt osiągnął docelową pozycję
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            isMoving = false; // Ruch zakończony

            Vector3 moveDirection = Vector3.zero;

            // Obsługa wejścia (klawisze kierunkowe)
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) // W górę
            {
                moveDirection = new Vector3(0, 0, moveDistance);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) // W dół
            {
                moveDirection = new Vector3(0, 0, -moveDistance);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) // W lewo
            {
                moveDirection = new Vector3(-moveDistance, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) // W prawo
            {
                moveDirection = new Vector3(moveDistance, 0, 0);
            }

            if (!IsObstacleInDirection(moveDirection, obstacleLayer) && moveDirection != Vector3.zero)
            {
                // Ustaw nową docelową pozycję
                targetPosition += moveDirection;

                x = true;

                if (onSpike = true && !IsSpikeInDirection(moveDirection, spikeLayer))
                {
                    x = false;
                }
                
                maxMove--;
                moveCount++;
                // Zwiększ licznik ruchów

                // Aktualizuj licznik w UI i konsoli
                UpdateMoveCounter();
                Debug.Log("Liczba ruchów: " + maxMove);

                // Oznacz, że obiekt aktualnie się porusza
                isMoving = true;
                CheckMaxMoves();
            }
            // Sprawdź, czy na drodze jest przeszkoda
            
        }

        // Płynne przechodzenie do docelowej pozycji
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (onSpike == true && x)
        {
            CheckSpike();
            x = false;
        }
    }

    // Funkcja sprawdzająca, czy w danym kierunku jest przeszkoda
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
                    // Jeśli boulder może się przesunąć, pozwól graczowi go popchnąć
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
                    // Jeśli boulder może się przesunąć, pozwól graczowi go popchnąć
                    enemy.TryMove(direction);
                    maxMove--;
                    moveCount++;
                    if (onSpike == true)
                    {
                        CheckSpike();
                    }
                }
            }
            return true; // Inny obiekt na drodze
        }
        return false; // Brak przeszkód
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
            return true; // Inny obiekt na drodze
        }
        return false; // Brak przeszkód
    }



    // Funkcja aktualizująca tekst licznika (jeśli podano referencję)
    public void UpdateMoveCounter()
    {
        if (moveCounterText != null)
        {
            moveCounterText.text = "" + maxMove;
        }
    }

    private void CheckMaxMoves()
    {
        if (maxMove < 0)
        {
            Debug.Log("Brak ruchów! Obiekt został zniszczony.");
            Destroy(gameObject); // Zniszcz obiekt gracza
        }
    }

    private void OnTriggerEnter (Collider collider)
    {
        if (collider.CompareTag("Spike"))
        {
            onSpike = true;
            spikeScript = collider.gameObject.GetComponent<SpikeScript>();
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
        // Sprawdź liczbę ruchów gracza z aktualnego stepMode w SpikeScript
        if (spikeScript != null) // Upewnij się, że spikeScript nie jest null
        {
            if (spikeScript.stepMode == SpikeScript.StepMode.Even && moveCount % 2 == 0)
            {
                Debug.Log("Gracz ma parzystą liczbę ruchów na spike.");
                maxMove--;
            }
            else if (spikeScript.stepMode == SpikeScript.StepMode.Odd && moveCount % 2 != 0)
            {
                Debug.Log("Gracz ma nieparzystą liczbę ruchów na spike.");
                maxMove--;
            }
            else if (spikeScript.stepMode == SpikeScript.StepMode.Always)
            {
                Debug.Log("Spike zawsze działa, niezależnie od liczby ruchów.");
                maxMove--;
            }
        }
    }
}