using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpikeScript;

public class SpikeAnimatorScript : MonoBehaviour
{
    public enum StepMode
    {
        Even,
        Odd,
        Always
    }

    public StepMode stepMode;
    public OneController playerGame;
    public Animator spikeAnimator;
    public bool spikeUp;
    private bool lastSpikeUpState;
    void Start()
    {
        if (playerGame == null)
        {
            playerGame = FindObjectOfType<OneController>();
        }
        spikeAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (spikeAnimator == null || playerGame == null) return;

        CheckSpikeUp();

        if (lastSpikeUpState != spikeUp)
        {
            Debug.Log($"SpikeUp changed: {spikeUp}");
            if (spikeUp)
            {
                spikeAnimator.SetTrigger("Up");
                spikeAnimator.ResetTrigger("Down");
            }
            else
            {
                spikeAnimator.SetTrigger("Down");
                spikeAnimator.ResetTrigger("Up");
            }
            lastSpikeUpState = spikeUp;
        }
    }

    public void CheckSpikeUp()
    {
        if (stepMode == StepMode.Even)
        {
            spikeUp = playerGame.moveCount % 2 == 0;
        }
        else if (stepMode == StepMode.Odd)
        {
            spikeUp = playerGame.moveCount % 2 != 0;
        }
        else if (stepMode == StepMode.Always)
        {
            spikeUp = true;
        }
    }
}
