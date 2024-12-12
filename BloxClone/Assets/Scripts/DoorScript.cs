using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isLocked = true; // Czy drzwi s� zablokowane?
    public Animator doorAnimator; // Referencja do animatora drzwi (opcjonalne, je�li drzwi maj� animacj�)
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
        Debug.Log("Drzwi zosta�y odblokowane!");

        if (doorCollider != null)
        {
            doorCollider.enabled = false; // Wy��cz collider
        }

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open"); // Otw�rz drzwi (animacja opcjonalna)
        }
        else
        {
            Debug.Log("Collider drzwi wy��czony. Drzwi s� teraz otwarte!");
        }
    }
}