using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _VariableManager : MonoBehaviour
{
    public static _VariableManager Instance { get; private set; }

    //Variables///////////////////////////

    //Dash Ability
    public int dashCount;
    public float dashTemp;
    public float dashCooldown;

    //Rapid Fire Ability
    public int rapidFireCount;
    public float rapidFireTemp;
    public float rapidFireCooldown;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            //Initialize all variable values////////////////////////

            //Dash ability
            dashCount = 2;
            dashTemp = dashCount;
            dashCooldown = 0;

            //Rapid Fire Ability
            rapidFireCount = 3;
            rapidFireTemp = rapidFireCount;
            rapidFireCooldown = 0;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}