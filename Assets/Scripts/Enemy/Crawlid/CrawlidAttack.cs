using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CrawlidAttack : MonoBehaviour
{
    private Enemy_Crawlid enemy => GetComponentInParent<Enemy_Crawlid>();


    public void Update()
    {
        if (enemy.stats.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            PlayerStats target = other.GetComponent<PlayerStats>();

            enemy.stats.DoDamage(target);
        }
    }
}
