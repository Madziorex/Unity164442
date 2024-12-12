using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public DoorScript linkedDoor; // Referencja do drzwi, które ten klucz otwiera

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Zak³adamy, ¿e gracz ma tag "Player"
        {
            Debug.Log("Klucz zosta³ zebrany!");

            if (linkedDoor != null)
            {
                linkedDoor.Unlock(); // Odblokuj drzwi
            }

            Destroy(gameObject); // Usuñ klucz z gry
        }
    }
}