using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderDifferentColor : MonoBehaviour
{
    public Transform transform;
    public bool alternateColors;
    public Color primaryColor;
    public Color secondaryColor;
    private bool primary = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPreRender()
    {
        if (primary/*alternateColors && System.DateTime.Now.Second % 20 < 10*/)
        {
            foreach (Transform child in transform) child.gameObject.GetComponent<Renderer>().material.SetColor("_Color", secondaryColor);
        } else
        {
            foreach (Transform child in transform) child.gameObject.GetComponent<Renderer>().material.SetColor("_Color", primaryColor);
        }
        if (alternateColors) primary = !primary;
    }

    void OnPostRender()
    {
        
    }
}
