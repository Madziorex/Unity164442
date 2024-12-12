using System.Collections;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public int speed = 300;
    bool isMoving = false;
    public Transform ghostPlayer;

    private void Update()
    {
        if (isMoving)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!IsObstacleInDirection(Vector3.forward))
                StartCoroutine(Roll(Vector3.forward));
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!IsObstacleInDirection(Vector3.back))
                StartCoroutine(Roll(Vector3.back));
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!IsObstacleInDirection(Vector3.left))
                StartCoroutine(Roll(Vector3.left));
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!IsObstacleInDirection(Vector3.right))
                StartCoroutine(Roll(Vector3.right));
        }
    }

    private bool IsObstacleInDirection(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction);
        float distance = 1.1f; // D³ugoœæ raycasta
        return Physics.Raycast(ray, distance);
    }

    IEnumerator Roll(Vector3 direction)
    {
        isMoving = true;

        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + direction / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        // Skopiuj aktualn¹ pozycjê i rotacjê do ghostPlayer
        CopyTransformData(transform, ghostPlayer);

        // Symulacja ruchu dla ghostPlayer
        ghostPlayer.RotateAround(rotationCenter, rotationAxis, 90);

        // Rozpocznij faktyczny ruch i stopniowo koryguj pozycjê na podstawie ghostPlayer
        while (remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }

        // Skoryguj pozycjê i rotacjê g³ównego gracza na podstawie ghostPlayer
        CopyTransformData(ghostPlayer, transform);

        isMoving = false;
    }

    private void CopyTransformData(Transform source, Transform target)
    {
        target.position = source.position;
        target.rotation = source.rotation;
    }
}