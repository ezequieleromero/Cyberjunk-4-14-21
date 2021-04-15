using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerScript player;
    EnemyScript enemy;
    public List<int> burnTickTimers = new List<int>();
    public List<int> PoisTickTimers = new List<int>();
    public int burnDuration = 4;
    public int burnDamage = 5;
    public int poisonDuration = 4;
    public int poisonDamage = 5;
    public GameObject flameEffect;
    public GameObject poisonEffect;

    public bool enemyScript;

    void Start()
    {
        if (!enemyScript)
            player = GetComponent<PlayerScript>();
        else
            enemy = GetComponent<EnemyScript>();
    }

    public void ApplyBurn()
    {
        if (burnTickTimers.Count <= 0)
        {
            burnTickTimers.Add(burnDuration);
            StartCoroutine(Burn());
        }
        else
        {
            burnTickTimers.Add(burnDuration);
        }
    }
    IEnumerator Burn()
    {
        while (burnTickTimers.Count > 0)
        {
            GameObject flameObject = Instantiate(flameEffect, transform.position, Quaternion.identity);
            flameObject.GetComponent<EffectHold>().player = this.gameObject;
            for (int i = 0; i < burnTickTimers.Count; i++)
            {
                burnTickTimers[i]--;
            }
            if (!enemyScript)
                player.health -= burnDamage;
            else
                enemy.health -= burnDamage;
            burnTickTimers.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(1f);
            Destroy(flameObject);
        }
    }
    public void ApplyPoison()
    {
        if (PoisTickTimers.Count <= 0)
        {
            PoisTickTimers.Add(poisonDuration);
            StartCoroutine(Poison());
        }
        else
        {
            PoisTickTimers.Add(poisonDuration);
        }
    }
    IEnumerator Poison()
    {
        while (PoisTickTimers.Count > 0)
        {
            GameObject poisonObject = Instantiate(poisonEffect, transform.position, Quaternion.identity);
            poisonObject.GetComponent<EffectHold>().player = this.gameObject;
            for (int i = 0; i < PoisTickTimers.Count; i++)
            {
                PoisTickTimers[i]--;
            }
            if (!enemyScript)
                player.health -= poisonDamage;
            else
                enemy.health -= poisonDamage;
            PoisTickTimers.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(1f);
            Destroy(poisonObject);
        }
    }
}
