using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public float rotationSpeed = 5f; // Prêdkoœæ animacji obrotu
    public Vector3 gridSize = new Vector3(1, 1, 1); // Wielkoœæ siatki
    private bool isMoving = false; // Flaga ruchu
    public bool sleepy = false;

    void Update()
    {
        if (!isMoving)
        {
            // Wykrycie kierunku ruchu
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                StartCoroutine(Roll(Vector3.forward));
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                StartCoroutine(Roll(Vector3.back));
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                StartCoroutine(Roll(Vector3.left));
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                StartCoroutine(Roll(Vector3.right));
        }
    }

    private IEnumerator Roll(Vector3 direction)
    {
        isMoving = true;

        Vector3 anchor;

        if (IsVertical())
        {
            anchor = transform.position + (direction / 2) + (Vector3.down * 1f);
            sleepy = false;
        }
        else
        {
            if (sleepy == true)
            {
                anchor = transform.position + direction;
            }
            else
            {
                anchor = transform.position + (direction) + (Vector3.down * 0.5f);
                sleepy = true;
            }
        }

        // Okreœlenie osi obrotu
        Vector3 axis = Vector3.Cross(Vector3.up, direction);

        // Oblicz k¹t obrotu
        for (int i = 0; i < 90 / rotationSpeed; i++)
        {
            transform.RotateAround(anchor, axis, rotationSpeed);
            yield return new WaitForFixedUpdate();
        }

        // Korekta pozycji koñcowej
        transform.RotateAround(anchor, axis, 90 % rotationSpeed);
        Vector3 roundedPosition = new Vector3(
            Mathf.Round(transform.position.x / gridSize.x) * gridSize.x,
            Mathf.Round(transform.position.y / gridSize.y) * gridSize.y,
            Mathf.Round(transform.position.z / gridSize.z) * gridSize.z
        );

        // Dostosowanie wysokoœci w zale¿noœci od orientacji bloku
        float adjustedHeight = IsVertical() ? 1f : 0.5f; // 1.5 dla pionowej orientacji, 0.5 dla poziomej
        //roundedPosition.y = adjustedHeight;
        Vector3 temp = transform.position;
        temp.y = adjustedHeight;
        transform.position = temp;

        isMoving = false;
    }

    private bool IsVertical()
    {
        // Sprawdzenie, czy blok jest w pozycji pionowej
        return Mathf.Abs(transform.up.y) > 0.9f;
    }
}
