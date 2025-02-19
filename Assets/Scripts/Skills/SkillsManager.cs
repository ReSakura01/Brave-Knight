using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager instance {  get; private set; }

    public Fireball_Skill fireball {  get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        fireball = GetComponent<Fireball_Skill>();
    }
}
