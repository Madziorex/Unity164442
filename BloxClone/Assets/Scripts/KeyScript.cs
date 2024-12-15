using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public DoorScript linkedDoor;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Klucz zosta� zebrany!");

            if (linkedDoor != null)
            {
                linkedDoor.Unlock();
            }

            Destroy(gameObject);
        }
    }
}