using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    //public bool checkEven = true; // Pole wyboru: true dla parzystych, false dla nieparzystych
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
    //public Animator spikeAnimator;
    //public bool spikeUp;
    private bool lastSpikeUpState;

    //private void Update()
    //{
    //    CheckSpikeUp();

    //    if (spikeAnimator != null && lastSpikeUpState != spikeUp)
    //    {
    //        if (spikeUp)
    //        {
    //            spikeAnimator.SetTrigger("Up");
    //        }
    //        else
    //        {
    //            spikeAnimator.SetTrigger("Down");
    //        }

    //        lastSpikeUpState = spikeUp; // Aktualizuj poprzedni stan
    //    }
    //}

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

    //public void CheckSpikeUp()
    //{
    //    if (playerController != null)
    //    {
    //        if (stepMode == StepMode.Even && playerController.moveCount % 2 == 0)
    //        {
    //            spikeUp = true;
    //        }
    //        else if (stepMode == StepMode.Even)
    //        {
    //            spikeUp = false;
    //        }
    //        else if (stepMode == StepMode.Odd && playerController.moveCount % 2 != 0)
    //        {
    //            spikeUp = true;
    //        }
    //        else if (stepMode == StepMode.Odd)
    //        {
    //            spikeUp = false;
    //        }
    //        else if (stepMode == StepMode.Always)
    //        {
    //            spikeUp = true;
    //        }
    //    }
    //}
}