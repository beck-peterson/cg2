using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoreButton : MonoBehaviour
{
    public GameObject boards;
    private BoardController boardController;
    private TextMeshPro text;
    void Start()
    {
        text = GetComponent(typeof(TextMeshPro)) as TextMeshPro;
        boardController = boards.GetComponent(typeof(BoardController)) as BoardController;
    }

    void Update()
    {
        if (boardController.programRunning)
        {
            text.text = "SPD+";
        }
        else
        {
            text.text = "VAL+";
        }
    }
}
