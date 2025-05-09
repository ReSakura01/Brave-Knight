using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shriek_Skill_AnimationTrigger : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        Destroy(transform.parent.gameObject);
    }
}
