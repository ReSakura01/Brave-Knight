using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAnimationTrigger : MonoBehaviour
{
    private Enemy_Wandering enemy => GetComponentInParent<Enemy_Wandering>();

    private void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attacckCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats target = hit.GetComponent<PlayerStats>();

                enemy.stats.DoDamage(target);
            }
        }

    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
