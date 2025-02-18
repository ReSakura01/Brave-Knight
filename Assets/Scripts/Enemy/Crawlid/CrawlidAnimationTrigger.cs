using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlidAnimationTrigger : MonoBehaviour
{
    private Enemy_Crawlid enemy => GetComponentInParent<Enemy_Crawlid>();
    private void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }
}
