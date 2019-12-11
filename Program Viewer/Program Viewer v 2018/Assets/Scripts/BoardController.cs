using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

// Unless otherwise specified, everything in this file was created by Beck Peterson with or without the assistance of documentation from the Unity website

public class BoardController : MonoBehaviour
{
    public string[] code = { "int Fibonacci(int number) {", "\tif (number <= 1) {", "\t\treturn 1;", "\t} else {", "\t\tint secondToLast = Fibonacci(number - 2);", "\t\tint last = Fibonacci(number - 1);", "\t\treturn secondToLast + last;", "\t}", "}" };
    public string[] programList = { "Fibonacci", "Greatest Common Factor" };
    public int currentProgram = 0;
    public int input = 7;
    public string output = "";
    public Boolean programRunning = false;

    void Start()
    {

    }

    void Update()
    {

    }

    public void MorePressed()
    {
        if (programRunning)
        {
            pauseTime /= 1.5f;
        }
        else
        {
            input++;
        }
    }

    public void LessPressed()
    {
        if (programRunning)
        {
            pauseTime *= 1.5f;
        }
        else
        {
            input--;
        }
    }

    public void CleanUpProgram()
    {
        StopAllCoroutines();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        output = "";
        programRunning = false;
    }

    public void StartProgram()
    {
        switch (programList[currentProgram])
        {
            case "Fibonacci":
                Fibonacci(input);
                break;
            case "Greatest Common Factor":
                break;
        }
        programRunning = true;
    }
    
    public void NextProgram()
    {
        CleanUpProgram();
        currentProgram = (currentProgram + 1) % programList.Length;
        Debug.Log(programList[currentProgram] + " " + currentProgram + " " + programList.Length + " " + programList + " " + programList[1]);
    }

    void Fibonacci(int number)
    {
        CleanUpProgram();
        StartCoroutine(FibonacciHelper(number));
    }

    IEnumerator FibonacciHelper(int number)
    {
        CoroutineWithData cd = new CoroutineWithData(this, Fibonacci(number, 0));
        yield return cd.coroutine;
        output = "" + cd.result;
        programRunning = false;
    }

    //int Fibonacci(int number) {
    //    if (number <= 1) {
    //        return 1;
    //    } else {
    //        int secondToLast = Fibonacci(number - 2);
    //        int last = Fibonacci(number - 1);
    //        return secondToLast + last;
    //    }
    //}

    float pauseTime = 1f;
    IEnumerator Fibonacci(int number, int depth) {
        GameObject board = GameObject.CreatePrimitive(PrimitiveType.Cube);
        board.AddComponent<Panel>().init(depth, code);
        Panel panel = board.GetComponent(typeof(Panel)) as Panel;
        board.transform.parent = gameObject.transform;
        panel.updateCode(new string[] { "int Fibonacci(number = " + number + ") {", "\tif (number <= 1) {", "\t\treturn 1;", "\t} else {", "\t\tint secondToLast = Fibonacci(number - 2);", "\t\tint last = Fibonacci(number - 1);", "\t\treturn secondToLast + last;", "\t}", "}" });
        yield return new WaitForSeconds(pauseTime);
        if (number <= 1) {
            panel.nextLine(1);
            yield return new WaitForSeconds(pauseTime);
            Destroy(board);
            yield return 1;
        } else {
            panel.nextLine(3);
            yield return new WaitForSeconds(pauseTime);
            CoroutineWithData cd = new CoroutineWithData(this, Fibonacci(number - 2, depth + 1));
            yield return cd.coroutine;
            int secondToLast = (int) cd.result;
            panel.updateCode(new string[] { "int Fibonacci(number = " + number + ") {", "\tif (number <= 1) {", "\t\treturn 1;", "\t} else {", "\t\tint secondToLast = " + secondToLast + ";", "\t\tint last = Fibonacci(number - 1);", "\t\treturn secondToLast + last;", "\t}", "}" });
            yield return new WaitForSeconds(pauseTime);
            panel.nextLine(1);
            yield return new WaitForSeconds(pauseTime);
            cd = new CoroutineWithData(this, Fibonacci(number - 1, depth + 1));
            yield return cd.coroutine;
            int last = (int) cd.result;
            panel.updateCode(new string[] { "int Fibonacci(number = " + number + ") {", "\tif (number <= 1) {", "\t\treturn 1;", "\t} else {", "\t\tint secondToLast = " + secondToLast + ";", "\t\tint last = " + last + ";", "\t\treturn secondToLast + last;", "\t}", "}" });
            yield return new WaitForSeconds(pauseTime);
            panel.nextLine(1);
            yield return new WaitForSeconds(pauseTime);
            Destroy(board);
            yield return secondToLast + last;
        }
    }
}

public class Panel : MonoBehaviour
{
    public GameObject pointer;
    public GameObject[] lines = new GameObject[20];

    public void init(int stack, string[] code)
    {
        GameObject board = gameObject;
        board.name = "Board";
        board.transform.localPosition = new Vector3(-1f + stack / 3f, 1.3f, 1.6f);
        board.transform.localRotation = Quaternion.Euler(0f, -60f, 0f);
        board.transform.localScale = new Vector3(1f, 9 / 16f, 0.02f);

        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.name = "Pointer";
        pointer.transform.parent = board.transform;
        pointer.transform.localPosition = new Vector3(0f, 0.391f, -0.001f);
        pointer.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        pointer.transform.localScale = new Vector3(1.0001f, 0.045f, 1f);
        pointer.GetComponent<Renderer>().material.color = new Color32(255, 159, 0, 255);

        for (int i = 0; i < lines.Length && i < code.Length; i++)
        {
            GameObject line = new GameObject("Line" + i);
            lines[i] = line;
            line.transform.parent = board.transform;

            TextMeshPro text = line.AddComponent<TextMeshPro>();
            text.rectTransform.localPosition = new Vector3(0f, 0.437f - 0.046f * i, -0.51f);
            text.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            text.rectTransform.localScale = new Vector3(9 / 16f, 1f, 1f);
            text.rectTransform.sizeDelta = new Vector2(16 / 9f, 0.045f);
            text.text = code[i];
            text.fontSize = 0.4f;
            text.faceColor = new Color32(0, 0, 0, 255);
            text.fontStyle = FontStyles.Bold;
            text.margin = new Vector4(0.04f, 0f, 0.04f, 0f);
            text.enableWordWrapping = false;
            text.overflowMode = TextOverflowModes.Truncate;
        }
    }

    public void nextLine(int lines)
    {
        Vector3 position = pointer.transform.localPosition;
        pointer.transform.localPosition = new Vector3(position.x, position.y - 0.046f * lines, position.z);
    }

    public void updateCode(string[] code)
    {
        for (int i = 0; i < lines.Length && i < code.Length; i++)
        {
            TextMeshPro text = lines[i].GetComponent(typeof(TextMeshPro)) as TextMeshPro;
            text.text = code[i];
        }
    }
}

// This class implementation comes from Ted-Bigham at https://answers.unity.com/questions/24640/how-do-i-return-a-value-from-a-coroutine.html
public class CoroutineWithData
{
    public Coroutine coroutine { get; private set; }
    public object result;
    private IEnumerator target;

    public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
    {
        this.target = target;
        this.coroutine = owner.StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (target.MoveNext())
        {
            result = target.Current;
            yield return result;
        }
    }
}