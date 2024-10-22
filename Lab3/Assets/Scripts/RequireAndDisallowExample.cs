using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class RequireAndDisallowExample : MonoBehaviour
{
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("Rigidbody component is present as required.");
        }
        else
        {
            Debug.LogError("Rigidbody component is missing, which should not happen due to [RequireComponent].");
        }
    }
}
