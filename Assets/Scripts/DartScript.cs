﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player") && (col.GetComponent<StatusEffectManager>() != null))
        {
            col.GetComponent<StatusEffectManager>().ApplyPoison();
            Destroy(gameObject);
        }
    }
}
