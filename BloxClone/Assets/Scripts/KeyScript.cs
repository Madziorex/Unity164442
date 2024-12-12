using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public DoorScript linkedDoor; // Referencja do drzwi, kt�re ten klucz otwiera

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Zak�adamy, �e gracz ma tag "Player"
        {
            Debug.Log("Klucz zosta� zebrany!");

            if (linkedDoor != null)
            {
                linkedDoor.Unlock(); // Odblokuj drzwi
            }

            Destroy(gameObject); // Usu� klucz z gry
        }
    }
}