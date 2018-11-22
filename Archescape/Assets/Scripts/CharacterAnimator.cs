using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    public AnimationClip replacableAttackAnimation;
    public AnimationClip[] defaultAttackAnimationSet;
    public AnimatorOverrideController overrideController;

    protected AnimationClip[] currentAttackAnimationSet;
    protected Animator animator;
    private NavMeshAgent agent;
    protected CharacterCombat combat;

    [SerializeField]
    private float dampTime = 0.1f;

	protected virtual void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        if(overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

        }
        animator.runtimeAnimatorController = overrideController;
        currentAttackAnimationSet = defaultAttackAnimationSet;

        combat.OnAttack += OnAttack;
	}
	
	protected virtual void Update () {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, dampTime, Time.deltaTime);

        animator.SetBool("inCombat", combat.inCombat);
	}

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimationSet.Length);
        overrideController[replacableAttackAnimation.name] = currentAttackAnimationSet[attackIndex];
    }
}
