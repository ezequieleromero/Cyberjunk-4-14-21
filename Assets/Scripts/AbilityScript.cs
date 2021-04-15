using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityScript : MonoBehaviour
{
    PlayerScript parentScript;
    GameObject abilityManager;
    _VariableManager _VariableManager;
    public GameObject dashEffect;

    /* Abilities
     * Dash (Cooldown) - makes you invulnerable
     * Rapid Fire (Cooldown) - scales shooting rate
     * Fire Rate (Passive Scale) - scales shooting rate
     * 
     * 
     * 
     * 
     * 
     * Slo-mo maybe
     */

    float xMov;
    float yMov;

    //Dash Ability
    public float dashCooldownSpeed;
    float dashTime = 30;
    float tempDash = 0;
    float initialSpeed;
    float newSpeed;
    bool earlyQueue = false;
    bool dashQueued = false;

    GameObject dashIcon;
    MaskScript maskDash;
    public Text dashText;

    //Rapid Fire Ability
    public float rapidFireCooldownSpeed;
    float rapidFireTime = 100;
    float tempRapidFire = 0;
    float initialFireRate;
    bool initialGunType;
    bool rapidFire;

    GameObject rapidFireIcon;
    MaskScript maskRapidFire;
    public Text rapidFireText;

    // Start is called before the first frame update
    void Start()
    {
        parentScript = GetComponent<PlayerScript>();

        abilityManager = GameObject.Find("VariableManager");

        if (abilityManager)
        {
            _VariableManager = abilityManager.GetComponent<_VariableManager>();
        }

        //Dash Icon
        dashIcon = GameObject.Find("DashIcon");
        maskDash = dashIcon.GetComponent<MaskScript>();
        dashText = GameObject.Find("DashText").GetComponent<Text>();

        //Rapid Fire Icon
        rapidFire = false;
        rapidFireIcon = GameObject.Find("RapidFireIcon");
        maskRapidFire = rapidFireIcon.GetComponent<MaskScript>();
        rapidFireText = GameObject.Find("RapidFireText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!abilityManager)
        {
            abilityManager = GameObject.Find("VariableManager");

            if (abilityManager)
            {
                _VariableManager = abilityManager.GetComponent<_VariableManager>();
                Debug.Log("Ability Manager Found");
            }
        }
        else
        {
            //Dash Ability/////////////////////////////////////////////////////////////////////
            dashText.text = _VariableManager.dashTemp.ToString();

            //Mask
            maskDash.xVal = _VariableManager.dashCooldown * 70;

            if (maskDash.xVal > 70 && _VariableManager.dashTemp < _VariableManager.dashCount)
            {
                maskDash.xVal = 70;
                _VariableManager.dashTemp++;

                if (_VariableManager.dashTemp < _VariableManager.dashCount)
                {
                    _VariableManager.dashCooldown = 0;
                }
                else
                {
                    _VariableManager.dashCooldown = 1;
                }
            }

            if (parentScript.dash)
            {
                tempDash++;
                parentScript.moveSpeed = ((newSpeed - initialSpeed) * (1 - (tempDash / dashTime))) + initialSpeed;

                Instantiate(dashEffect, transform.position + new Vector3(0, -0.2f, 0), Quaternion.identity);

                if (tempDash >= dashTime)
                {
                    tempDash = 0;
                    parentScript.dash = false;
                    parentScript.moveSpeed = initialSpeed;
                    parentScript.invincible = false;
                    parentScript.movement.x = Input.GetAxisRaw("Horizontal"); //Input for x movement
                    parentScript.movement.y = Input.GetAxisRaw("Vertical"); //Input for y movement
                }

                //Dash Queue
                if (Input.GetKeyDown("space"))
                {
                    earlyQueue = true;
                }
            }

            xMov = parentScript.movement.x;
            yMov = parentScript.movement.y;

            //Dash Mechanism
            if (((Input.GetKeyDown("space") || dashQueued) && !parentScript.dash) && _VariableManager.dashTemp > 0 && (xMov != 0 || yMov != 0))
            {
                _VariableManager.dashTemp--;
                _VariableManager.dashCooldown = 0;

                dashQueued = false;

                //Get player's current velocity
                initialSpeed = parentScript.moveSpeed;

                //Stop player
                parentScript.dash = true;

                //Set player velocity
                parentScript.movement.x = xMov;
                parentScript.movement.y = yMov;
                parentScript.moveSpeed *= 6;
                newSpeed = parentScript.moveSpeed;
                parentScript.invincible = true;
            }

            if (earlyQueue)
            {
                earlyQueue = false;
                dashQueued = true;
            }

            //Rapid Fire Ability/////////////////////////////////////////////////////////////////////
            rapidFireText.text = _VariableManager.rapidFireTemp.ToString();

            //Mask
            maskRapidFire.xVal = _VariableManager.rapidFireCooldown * 70;

            if (maskRapidFire.xVal > 70 && _VariableManager.rapidFireTemp < _VariableManager.rapidFireCount)
            {
                maskRapidFire.xVal = 70;
                _VariableManager.rapidFireTemp++;

                if (_VariableManager.rapidFireTemp < _VariableManager.rapidFireCount)
                {
                    _VariableManager.rapidFireCooldown = 0;
                }
                else
                {
                    _VariableManager.rapidFireCooldown = 1;
                }
            }

            if (rapidFire)
            {
                GunScript weapon = parentScript.weapon.GetComponent<GunScript>();

                tempRapidFire++;

                if (tempRapidFire >= rapidFireTime)
                {
                    tempRapidFire = 0;
                    rapidFire = false;

                    //Reset Fire Rate

                    weapon.automaticDelay = weapon.initialDelay;
                    weapon.automaticAssault = weapon.initialAutomaticAssault;
                }
                else
                {
                    weapon.automaticDelay = weapon.initialDelay / 2;
                    weapon.automaticAssault = true;
                }
            }

            xMov = parentScript.movement.x;
            yMov = parentScript.movement.y;

            //Dash Mechanism
            if ((Input.GetKeyDown("q") && !rapidFire) && _VariableManager.rapidFireTemp > 0)
            {
                Debug.Log("Rapid Fire Activated!");

                _VariableManager.rapidFireTemp--;
                _VariableManager.rapidFireCooldown = 0;

                //Start Rapid Fire
                rapidFire = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_VariableManager.dashTemp < _VariableManager.dashCount)
        {
            _VariableManager.dashCooldown += dashCooldownSpeed;
        }
        else
        {
            _VariableManager.dashCooldown = 1;
        }

        if (_VariableManager.rapidFireTemp < _VariableManager.rapidFireCount)
        {
            _VariableManager.rapidFireCooldown += rapidFireCooldownSpeed;
        }
        else
        {
            _VariableManager.rapidFireCooldown = 1;
        }
    }
}
