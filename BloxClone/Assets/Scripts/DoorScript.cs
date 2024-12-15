using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isLocked = true;
    public Animator doorAnimator;
    private Collider doorCollider;
    public AudioSource doorSound;

    void Start()
    {
        doorCollider = GetComponent<Collider>();
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
            doorCollider.enabled = false;
            doorSound.Play();
        }

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }
        else
        {
            Debug.Log("Collider drzwi wy³¹czony. Drzwi s¹ teraz otwarte!");
        }
    }
}