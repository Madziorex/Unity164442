using UnityEngine;
using TMPro; // U�ywane do obs�ugi TextMeshPro

public class MaxMoveDisplay : MonoBehaviour
{
    public OneController oneController; // Referencja do komponentu OneController
    public TMP_Text maxMoveText;        // Referencja do TextMeshPro UI

    void Update()
    {
        if (oneController != null && maxMoveText != null)
        {
            int maxMove = oneController.maxMove;

            // Wy�wietl "X", je�li maxMove jest mniejsze od 1, w przeciwnym razie wy�wietl liczb�
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
