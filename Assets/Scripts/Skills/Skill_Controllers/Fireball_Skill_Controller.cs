using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Skill_Controller : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetupFireball(float _force)
    {
        float playerfacing = PlayerManager.instance.player.facingDir;

        if (PlayerManager.instance.player.facingRight)
        {
            transform.Rotate(0, 180, 0);
        }

        rb.velocity = new Vector2(_force * playerfacing, 0);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Enemy>()?.Damage();
    }
}
