using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceController : MonoBehaviour
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
        text.text = "Program:\n" + boardController.programList[boardController.currentProgram] + "\nInput:\n" + boardController.input + "\nOutput:\n" + boardController.output;
    }
}
