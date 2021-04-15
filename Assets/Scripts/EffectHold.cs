using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHold : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject flameEffectDummy;
    public GameObject poisonEffect;
    public GameObject player;

    public bool enemyScript;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            if (gameObject == flameEffectDummy)
            {
                flameEffectDummy.transform.position = player.transform.position;

            }
            else
            {
                poisonEffect.transform.position = player.transform.position;
            }
        }
        else
        {
            Destroy(this);
        }
    }
}
