using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

// Unless otherwise specified, everything in this file was created by Beck Peterson with or without the assistance of documentation from the Unity website

public class BoardController : MonoBehaviour
{
    public string[] programList = { "Factorial Iterative", "Factorial Recursive", "Fibonacci Iterative", "Fibonacci Recursive" };
    public int currentProgram = 0;
    public int input = 7;
    public string output = "";
    public Boolean programRunning = false;
    private float pauseTime = 1f;

    void Start()
    {

    }

    void Update()
    {

    }

    //////////
    //
    // These functions are renamed to make rebinding the buttons to the functions easier when I need to re bind everything when unity fails to load the script
    //
    //////////

    public void StartButton()
    {
        StartProgram();
    }

    public void StopButton()
    {
        CleanUpProgram();
    }

    public void MoreButton()
    {
        MorePressed();
    }

    public void LessButton()
    {
        LessPressed();
    }

    public void NextProgramButton()
    {
        NextProgram();
    }

    //////////
    //
    // Private functions
    //
    //////////

    private void MorePressed()
    {
        if (programRunning)
        {
            pauseTime /= 1.5f;
        }
        else
        {
            if (input < 10) input++;
        }
    }

    private void LessPressed()
    {
        if (programRunning)
        {
            pauseTime *= 1.5f;
        }
        else
        {
            if (input > 1) input--;
        }
    }

    private void CleanUpProgram()
    {
        StopAllCoroutines();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        output = "";
        programRunning = false;
    }

    private void StartProgram()
    {
        switch (programList[currentProgram])
        {
            case "Factorial Iterative":
                FactorialIterative(input);
                break;
            case "Factorial Recursive":
                FactorialRecursive(input);
                break;
            case "Fibonacci Iterative":
                FibonacciIterative(input);
                break;
            case "Fibonacci Recursive":
                FibonacciRecursive(input);
                break;
        }
        programRunning = true;
    }

    private void NextProgram()
    {
        CleanUpProgram();
        currentProgram = (currentProgram + 1) % programList.Length;
    }

    //////////
    //
    // Factorial Iterative
    //
    //////////

    //int Factorial(int number) {
    //    int product = 1;
    //    int i = 1;
    //    for (i = 1; i < number; i++) {
    //        product = product * i;
    //    }
    //    return product;
    //}

    private void FactorialIterative(int number)
    {
        CleanUpProgram();
        StartCoroutine(FactorialIterativeHelper(number));
    }

    private IEnumerator FactorialIterativeHelper(int number)
    {
        CoroutineWithData cd = new CoroutineWithData(this, FactorialIterative(number, 0));
        yield return cd.coroutine;
        output = "" + cd.result;
        programRunning = false;
    }

    private IEnumerator FactorialIterative(int number, int depth)
    {
        int product = 1;
        int i = 1;
        GameObject board = GameObject.CreatePrimitive(PrimitiveType.Cube);
        board.AddComponent<Panel>().init(depth, new string[] { "int FactorialIterative(number = " + number + ") {", "\tint product = " + product + ";", "\tint i = " + i + ";", "\tfor (i = 1; i < number; i++) {", "\t\tproduct = product * i;", "\t}", "\treturn product;", "}" });
        Panel panel = board.GetComponent(typeof(Panel)) as Panel;
        board.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(pauseTime);
        panel.nextLine(1);
        yield return new WaitForSeconds(pauseTime);
        panel.nextLine(1);
        yield return new WaitForSeconds(pauseTime);
        for (i = 1; i < number; i++) {
            panel.nextLine(1);
            yield return new WaitForSeconds(pauseTime);
            product = product * i;
            panel.updateCode(new string[] { "int FactorialIterative(number = " + number + ") {", "\tint product = " + product + ";", "\tint i = " + i + ";", "\tfor (i = 1; i < number; i++) {", "\t\tproduct = " + product + ";", "\t}", "\treturn product;", "}" });
            yield return new WaitForSeconds(pauseTime);
            panel.nextLine(-1);
            panel.updateCode(new string[] { "int FactorialIterative(number = " + number + ") {", "\tint product = " + product + ";", "\tint i = " + (i + 1) + ";", "\tfor (i = 1; i < number; i++) {", "\t\tproduct = product * i;", "\t}", "\treturn product;", "}" });
            yield return new WaitForSeconds(pauseTime);
        }
        panel.nextLine(3);
        yield return new WaitForSeconds(pauseTime);
        Destroy(board);
        yield return product;
    }

    //////////
    //
    // Factorial Recursive
    //
    //////////

    //int Factorial(int number) {
    //    if (number <= 1) {
    //        return 1;
    //    } else {
    //        int smallerFactorial = Factorial(number - 1);
    //        return number * smallerFactorial;
    //    }
    //}

    private void FactorialRecursive(int number)
    {
        CleanUpProgram();
        StartCoroutine(FactorialRecursiveHelper(number));
    }

    private IEnumerator FactorialRecursiveHelper(int number)
    {
        CoroutineWithData cd = new CoroutineWithData(this, FactorialRecursive(number, 0));
        yield return cd.coroutine;
        output = "" + cd.result;
        programRunning = false;
    }

    private IEnumerator FactorialRecursive(int number, int depth)
    {
        GameObject board = GameObject.CreatePrimitive(PrimitiveType.Cube);
        board.AddComponent<Panel>().init(depth, new string[] { "int FactorialRecursive(number = " + number + ") {", "\tif (number <= 1) {", "\t\treturn 1;", "\t} else {", "\t\tint smallerFactorial = Factorial(number - 1);", "\t\treturn number * smallerFactorial;", "\t}", "}" });
        Panel panel = board.GetComponent(typeof(Panel)) as Panel;
        board.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(pauseTime);
        if (number <= 1) {
            panel.nextLine(1);
            yield return new WaitForSeconds(pauseTime);
            Destroy(board);
            yield return 1;
        } else {
            panel.nextLine(3);
            yield return new WaitForSeconds(pauseTime);
            CoroutineWithData cd = new CoroutineWithData(this, FactorialRecursive(number - 1, depth + 1));
            yield return cd.coroutine;
            int smallerFactorial = (int) cd.result;
            panel.updateCode(new string[] { "int FactorialRecursive(number = " + number + ") {", "\tif (number <= 1) {", "\t\treturn 1;", "\t} else {", "\t\tint smallerFactorial = " + smallerFactorial + ";", "\t\treturn number * smallerFactorial;", "\t}", "}" });
            yield return new WaitForSeconds(pauseTime);
            panel.nextLine(1);
            yield return new WaitForSeconds(pauseTime);
            Destroy(board);
            yield return number * smallerFactorial;
        }
    }

    //////////
    //
    // Fibonacci Iterative
    //
    //////////

    //int Fibonacci(int number) {
    //    int a = 0;
    //    int b = 1;
    //    for (int i = 0; i < number; i++) {
    //        int temp = a;
    //        a = b;
    //        b = temp + b;
    //    }
    //    return a;
    //}

        private void FibonacciIterative(int number)
    {
        CleanUpProgram();
        StartCoroutine(FibonacciIterativeHelper(number));
    }

    private IEnumerator FibonacciIterativeHelper(int number)
    {
        CoroutineWithData cd = new CoroutineWithData(this, FibonacciIterative(number, 0));
        yield return cd.coroutine;
        output = "" + cd.result;
        programRunning = false;
    }

    private IEnumerator FibonacciIterative(int number, int depth)
    {
        int a = 1;
        int b = 1;
        int i = 0;
        GameObject board = GameObject.CreatePrimitive(PrimitiveType.Cube);
        board.AddComponent<Panel>().init(depth, new string[] { "int FibonacciIterative(number = " + number + ") {", "\tint a = " + a + ";", "\tint b = " + b + ";", "\tint i = " + i + ";", "\tfor (i = 0; i < number; i++) {", "\t\tint temp = a;", "\t\ta = b;", "\t\tb = temp + b;", "\t}", "\treturn a;", "}" });
        Panel panel = board.GetComponent(typeof(Panel)) as Panel;
        board.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(pauseTime);
        panel.nextLine(1);
        yield return new WaitForSeconds(pauseTime);
        panel.nextLine(1);
        yield return new WaitForSeconds(pauseTime);
        panel.nextLine(1);
        yield return new WaitForSeconds(pauseTime);
        for (i = 0; i < number; i++)
        {
            panel.nextLine(1);
            yield return new WaitForSeconds(pauseTime);
            int temp = a;
            panel.updateCode(new string[] { "int FibonacciIterative(number = " + number + ") {", "\tint a = " + a + ";", "\tint b = " + b + ";", "\tint i = " + i + ";", "\tfor (i = 0; i < number; i++) {", "\t\tint temp = " + temp + ";", "\t\ta = b;", "\t\tb = temp + b;", "\t}", "\treturn a;", "}" });
            yield return new WaitForSeconds(pauseTime);
            panel.nextLine(1);
            yield return new WaitForSeconds(pauseTime);
            a = b;
            panel.updateCode(new string[] { "int FibonacciIterative(number = " + number + ") {", "\tint a = " + a + ";", "\tint b = " + b + ";", "\tint i = " + i + ";", "\tfor (i = 0; i < number; i++) {", "\t\tint temp = " + temp + ";", "\t\ta = " + a + ";", "\t\tb = temp + b;", "\t}", "\treturn a;", "}" });
            yield return new WaitForSeconds(pauseTime);
            panel.nextLine(1);
            yield return new WaitForSeconds(pauseTime);
            b = temp + b;
            panel.updateCode(new string[] { "int FibonacciIterative(number = " + number + ") {", "\tint a = " + a + ";", "\tint b = " + b + ";", "\tint i = " + i + ";", "\tfor (i = 0; i < number; i++) {", "\t\tint temp = " + temp + ";", "\t\ta = " + a + ";", "\t\tb = " + b + ";", "\t}", "\treturn a;", "}" });
            yield return new WaitForSeconds(pauseTime);
            panel.nextLine(-3);
            panel.updateCode(new string[] { "int FibonacciIterative(number = " + number + ") {", "\tint a = " + a + ";", "\tint b = " + b + ";", "\tint i = " + (i + 1) + ";", "\tfor (i = 0; i < number; i++) {", "\t\tint temp = a;", "\t\ta = b;", "\t\tb = temp + b;", "\t}", "\treturn a;", "}" });
            yield return new WaitForSeconds(pauseTime);
        }
        panel.nextLine(5);
        yield return new WaitForSeconds(pauseTime);
        Destroy(board);
        yield return a;
    }

    //////////
    //
    // Fibonacci Recursive
    //
    //////////

    //int Fibonacci(int number) {
    //    if (number <= 1) {
    //        return 1;
    //    } else {
    //        int secondToLast = Fibonacci(number - 2);
    //        int last = Fibonacci(number - 1);
    //        return secondToLast + last;
    //    }
    //}

    private void FibonacciRecursive(int number)
    {
        CleanUpProgram();
        StartCoroutine(FibonacciRecursiveHelper(number));
    }

    private IEnumerator FibonacciRecursiveHelper(int number)
    {
        CoroutineWithData cd = new CoroutineWithData(this, FibonacciRecursive(number, 0));
        yield return cd.coroutine;
        output = "" + cd.result;
        programRunning = false;
    }

    private IEnumerator FibonacciRecursive(int number, int depth) {
        GameObject board = GameObject.CreatePrimitive(PrimitiveType.Cube);
        board.AddComponent<Panel>().init(depth, new string[] { "int FibonacciRecursive(number = " + number + ") {", "\tif (number <= 1) {", "\t\treturn 1;", "\t} else {", "\t\tint secondToLast = FibonacciRecursive(number - 2);", "\t\tint last = FibonacciRecursive(number - 1);", "\t\treturn secondToLast + last;", "\t}", "}" });
        Panel panel = board.GetComponent(typeof(Panel)) as Panel;
        board.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(pauseTime);
        if (number <= 1) {
            panel.nextLine(1);
            yield return new WaitForSeconds(pauseTime);
            Destroy(board);
            yield return 1;
        } else {
            panel.nextLine(3);
            yield return new WaitForSeconds(pauseTime);
            CoroutineWithData cd = new CoroutineWithData(this, FibonacciRecursive(number - 2, depth + 1));
            yield return cd.coroutine;
            int secondToLast = (int) cd.result;
            panel.updateCode(new string[] { "int FibonacciRecursive(number = " + number + ") {", "\tif (number <= 1) {", "\t\treturn 1;", "\t} else {", "\t\tint secondToLast = " + secondToLast + ";", "\t\tint last = FibonacciRecursive(number - 1);", "\t\treturn secondToLast + last;", "\t}", "}" });
            yield return new WaitForSeconds(pauseTime);
            panel.nextLine(1);
            yield return new WaitForSeconds(pauseTime);
            cd = new CoroutineWithData(this, FibonacciRecursive(number - 1, depth + 1));
            yield return cd.coroutine;
            int last = (int) cd.result;
            panel.updateCode(new string[] { "int FibonacciRecursive(number = " + number + ") {", "\tif (number <= 1) {", "\t\treturn 1;", "\t} else {", "\t\tint secondToLast = " + secondToLast + ";", "\t\tint last = " + last + ";", "\t\treturn secondToLast + last;", "\t}", "}" });
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