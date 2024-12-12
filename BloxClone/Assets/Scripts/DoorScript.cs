using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isLocked = true; // Czy drzwi s¹ zablokowane?
    public Animator doorAnimator; // Referencja do animatora drzwi (opcjonalne, jeœli drzwi maj¹ animacjê)
    private Collider doorCollider; // Referencja do collidera drzwi

    void Start()
    {
        doorCollider = GetComponent<Collider>(); // Pobierz collider drzwi
        if (doorCollider == null)
        {
            Debug.LogError("Brak Collidera na obiekcie drzwi!");
        }
    }

    public void Unlock()
    {
        isLocked = false;
        Debug.Log("Drzwi zosta³y odblokowane!");

        if (doorCollider != null)
        {
            doorCollider.enabled = false; // Wy³¹cz collider
        }

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open"); // Otwórz drzwi (animacja opcjonalna)
        }
        else
        {
            Debug.Log("Collider drzwi wy³¹czony. Drzwi s¹ teraz otwarte!");
        }
    }
}