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

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
