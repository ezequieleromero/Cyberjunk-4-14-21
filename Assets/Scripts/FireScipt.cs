using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScipt : MonoBehaviour
{
    // Start is called before the first frame update]
    public bool flameLoop = false;
    public float flameRepeatAmount = 3;
    public bool objectOnFire = true;

    public GameObject flame;
    void Start()
    {
        if(!flameLoop)
        {
            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length * flameRepeatAmount);
        }
        else
        {
            //let animation loop until scene changes.
        }
    }

    // Update is called once per frame
    void Update()
    {
;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player") && (col.GetComponent<StatusEffectManager>() != null))
        {
            //GameObject flameObject = Instantiate(flame, col.transform.position, Quaternion.identity);
            if (!objectOnFire)
            {
                col.GetComponent<StatusEffectManager>().ApplyBurn();
            }
        }
    }
}
