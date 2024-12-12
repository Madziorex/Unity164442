using UnityEngine;
using TMPro; // U¿ywane do obs³ugi TextMeshPro

public class MaxMoveDisplay : MonoBehaviour
{
    public OneController oneController; // Referencja do komponentu OneController
    public TMP_Text maxMoveText;        // Referencja do TextMeshPro UI

    void Update()
    {
        if (oneController != null && maxMoveText != null)
        {
            int maxMove = oneController.maxMove;

            // Wyœwietl "X", jeœli maxMove jest mniejsze od 1, w przeciwnym razie wyœwietl liczbê
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
