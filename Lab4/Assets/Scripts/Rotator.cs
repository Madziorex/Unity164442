using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    public float rotationSpeed = 50.0f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}