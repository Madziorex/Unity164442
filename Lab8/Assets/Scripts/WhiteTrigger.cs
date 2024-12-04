using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTrigger : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = this.transform.parent.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider obj)
    {
        Debug.Log("Trigger entered by: " + obj.name);
        anim.SetBool("isHere", true);
    }

    void OnTriggerExit(Collider obj)
    {
        Debug.Log("Trigger exited by: " + obj.name);
        anim.SetBool("isHere", false);
    }
}
