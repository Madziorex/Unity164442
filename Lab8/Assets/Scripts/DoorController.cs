using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{

    private Animator anim;
    void Start()
    {
        anim = this.transform.parent.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider obj)
    {
        Debug.Log("Trigger entered by: " + obj.name);
        anim.SetBool("isOpen", true);
    }

    void OnTriggerExit(Collider obj)
    {
        Debug.Log("Trigger exited by: " + obj.name);
        anim.SetBool("isOpen", false);
    }
}