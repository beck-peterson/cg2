using UnityEngine;
using UnityEngine.Events;

// This file was taken from youtuber Slide Factory at https://www.youtube.com/watch?v=I8Pb6Nhb4zE before having modifications added to it
public class ControllerButton : MonoBehaviour
{
    [System.Serializable]
    public class ButtonEvent : UnityEvent { }

    public float pressLength;
    public bool pressed;
    public ButtonEvent downEvent;

    Vector3 startPos;
    Rigidbody rb;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.0001f, transform.position.z); // This line was added to prevent the button from being stuck at the beginning of execution
    }

    void Update()
    {
        // If our distance is greater than what we specified as a press
        // set it to our max distance and register a press if we haven't already
        float distance = Mathf.Abs(transform.position.y - startPos.y);
        if (distance >= pressLength)
        {
            // Prevent the button from going past the pressLength
            transform.position = new Vector3(transform.position.x, startPos.y - pressLength - 0.0001f, transform.position.z); // The - 0.0001f was added to prevent the program from rapidly firing when the button is held down
            if (!pressed)
            {
                pressed = true;
                // If we have an event, invoke it
                downEvent?.Invoke();
            }
        }
        else
        {
            // If we aren't all the way down, reset our press
            pressed = false;
        }
        // Prevent button from springing back up past its original position
        if (transform.position.y > startPos.y)
        {
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        }
    }
}