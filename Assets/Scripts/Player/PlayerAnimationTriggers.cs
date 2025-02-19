using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attacckCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
                hit.GetComponent<Enemy>().Damage();
        }
    }

    private void FireballCast()
    {
        SkillsManager.instance.fireball.CreateFireball();
    }   
    private void OpenSlashEffect() => player.OpenSlashEffect();
    private void CloseSlashEffect() => player.CloseSlashEffect();

    private void OpenSlashEffectAlt() => player.OpenSlashEffectAlt();
    private void CloseSlashEffectAlt() => player.CloseSlashEffectAlt();
}
