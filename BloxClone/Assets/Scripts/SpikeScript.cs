using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public enum StepMode
    {
        Even,
        Odd,
        Always
    }

    public StepMode stepMode;

    private OneController playerController;
    private int lastMove;
    public bool onSpike = false;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            onSpike = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            onSpike = false;
        }
    }
}
