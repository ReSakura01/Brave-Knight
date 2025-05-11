using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [SerializeField] private GameObject geoPrefab;       // 拖入你的 Geo 预制体
    [SerializeField] private int geoDropCount = 5;       // 生成几个 Geo
    [SerializeField] private float spawnForce = 5f;      // 发射力度

    [Header("Attack details")]
    public Transform attacckCheck;
    public float attackCheckRadius;

    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;

    [Header("Attack info")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;

    public bool canBeCountered;

    public EnemyStateMachine stateMachine { get; private set; }
    public string lastAnimBoolName { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    private void SpawnGeo()
    {
        for (int i = 0; i < geoDropCount; i++)
        {
            GameObject geo = Instantiate(geoPrefab, transform.position, Quaternion.identity);

            // 发射一个随机方向的力（2D）
            Rigidbody2D rb = geo.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 forceDir = (Vector2.up + Random.insideUnitCircle * 0.5f).normalized;
                rb.AddForce(forceDir * spawnForce, ForceMode2D.Impulse);
            }
        }
    }

    public virtual void AssignLastAnimName(string name)
    {
        lastAnimBoolName = name;
    }

    public virtual void OpenCounterAttackWindow()
    {
        canBeCountered = true;
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeCountered = false;
    }

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual RaycastHit2D IsPlayerrDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
        Gizmos.DrawWireSphere(attacckCheck.position, attackCheckRadius);
    }
    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;

        float playerDir = -Mathf.Sign(PlayerManager.instance.player.transform.localRotation.y);

        rb.velocity = new Vector2(knockbackDirection.x * playerDir, knockbackDirection.y);

        yield return new WaitForSeconds(knockbackDuration);

        isKnocked = false;
    }

    public override void Die()
    {
        base.Die();

        SpawnGeo();
    }
}
