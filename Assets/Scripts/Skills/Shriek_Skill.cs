using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shriek_Skill : Skill
{
    [Header("Skill info")]
    [SerializeField] private GameObject ShriekPrefab;
    [SerializeField] private float height;

    public void CreateShriek()
    {

        Vector3 headOffset = new Vector3(0, height, 0);
        GameObject newShriek = Instantiate(ShriekPrefab, player.transform.position + headOffset, transform.rotation);

        Shriek_Skill_Controller newShriekScript = newShriek.GetComponent<Shriek_Skill_Controller>();

        newShriekScript.SetupShriek();
    }
}
