using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    public bool mouseOver;
    public bool clicked;
    public bool clickable;

    // Start is called before the first frame update
    void Start()
    {
        mouseOver = false;
        clicked = false;
    }

    void OnMouseOver()
    {
        mouseOver = true;

        if (Input.GetMouseButtonDown(0) && clickable)
        {
            clicked = true;
            Debug.Log("Clicked");
        }
    }

    void OnMouseExit()
    {
        mouseOver = false;
    }
}
