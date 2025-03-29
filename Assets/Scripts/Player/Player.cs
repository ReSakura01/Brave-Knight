using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack details")]
    public Transform attacckCheck;
    public float attackCheckRadius;
    public GameObject slashEffect1, slashEffect2, slashEffectAlt1;
    public float notAttackedDuration;
    private float notAttackedTimer;
    public float blinkInterval;

    public bool isBusy { get; private set; }

    [Header("Move info")]
    public float moveSpeed = 4f;
    public float jumpForce;

    [Header("Dash info")]
    public bool isDashing;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir = -1;

    [Header("WallSlide info")]
    public float wallSlideForce;

    public bool fromWall = false;

    [Header("Fireball info")]
    public Vector2 fireballCastSpeed;

    public SkillsManager skill {  get; private set; }
    public GameObject fireball { get; private set; }

    #region State
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerFireballCastState fireballCastState { get; private set; }
    public PlayerStunedState stunedState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        fallState  = new PlayerFallState (stateMachine, this, "Jump");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        dashState = new PlayerDashState(stateMachine, this, "Dash");
        wallSlideState = new PlayerWallSlideState(stateMachine, this, "WallSlide");
        wallJumpState = new PlayerWallJumpState(stateMachine, this, "DoubleJump");
        primaryAttackState = new PlayerPrimaryAttackState(stateMachine, this, "Attack");
        fireballCastState = new PlayerFireballCastState(stateMachine, this, "Fireball");
        stunedState = new PlayerStunedState(stateMachine, this, "Stuned");
        deadState = new PlayerDeadState(stateMachine, this, "Die");
    }

    protected override void Start()
    {
        base.Start();

        skill = SkillsManager.instance;

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base .Update();

        stateMachine.currentState.Update();

        if (!isBusy)
        {
            CheckForDashInput();
            CheckForFireballCastInput();
        }

        notAttackedTimer -= Time.deltaTime;

        CheckForSpike();
    }

    private IEnumerator BlinkEffect()
    {
        for (int i = 0; i < notAttackedDuration / (2 * blinkInterval); i ++) // ÉÁË¸ 5 ´Î
        {
            sr.color = new Color(1f, 1f, 1f, 0.2f); // ±äÍ¸Ã÷
            yield return new WaitForSeconds(blinkInterval);
            sr.color = new Color(1f, 1f, 1f, 1f); // »Ö¸´
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;

        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);

        yield return new WaitForSeconds(knockbackDuration);

        isKnocked = false;
    }

    public override void DamageEffect()
    {
        base.DamageEffect();

        if (stats.currentHealth <= 0) return;

        StartCoroutine(BlinkEffect());
        notAttackedTimer = notAttackedDuration;
        stateMachine.ChangeState(stunedState);
    }

    #region EffectTrigger
    public virtual void OpenSlashEffect()
    {
        slashEffect1.SetActive(true);
        slashEffect2.SetActive(true);
    }

    public virtual void CloseSlashEffect()
    {
        slashEffect1.SetActive(false);
        slashEffect2.SetActive(false);
    }

    public virtual void OpenSlashEffectAlt()
    {
        slashEffectAlt1.SetActive(true);
    }

    public virtual void CloseSlashEffectAlt()
    {
        slashEffectAlt1.SetActive(false);
    }
    #endregion

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    #region SkillCheck
    private void CheckForFireballCastInput()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            stateMachine.ChangeState(fireballCastState);
        }
    }

    private void CheckForDashInput()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            if (IsWallDetected())
            {
                fromWall = true;
                dashDir = -facingDir;
            }

            stateMachine.ChangeState(dashState);
        }
    }

    private void CheckForSpike()
    {
        if (IsSpikeDetected() && notAttackedTimer < 0)
            DamageEffect();
    }

    #endregion

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(attacckCheck.position, attackCheckRadius);
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }
}
