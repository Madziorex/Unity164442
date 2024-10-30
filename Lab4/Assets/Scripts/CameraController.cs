using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5.0f;
    private float fixedHeight;

    void Start()
    {
        fixedHeight = transform.position.y;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x, fixedHeight, player.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}