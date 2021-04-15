﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetScript : MonoBehaviour
{
    public bool collided = false;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        collided = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collision")
        {
            collided = true;
        }
    }
}
