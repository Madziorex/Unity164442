using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaxMoveDisplay : MonoBehaviour
{
    public OneController oneController;
    public TMP_Text maxMoveText;

    void Update()
    {
        if (oneController != null && maxMoveText != null)
        {
            int maxMove = oneController.maxMove;

            if (maxMove < 1)
            {
                maxMoveText.text = "X";
            }
            else
            {
                maxMoveText.text = maxMove.ToString();
            }
        }
    }
}
