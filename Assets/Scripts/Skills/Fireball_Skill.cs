using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Skill : Skill
{
    [Header("Skill info")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float castForce;

    public void CreateFireball()
    {
        GameObject newFireball = Instantiate(fireballPrefab, player.transform.position, transform.rotation);
        Fireball_Skill_Controller newFireballScript = newFireball.GetComponent<Fireball_Skill_Controller>();

        newFireballScript.SetupFireball(castForce);

        StartCoroutine(WaitFor(1.5f, newFireball));

    }

    public IEnumerator WaitFor(float time, GameObject gameObject)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
