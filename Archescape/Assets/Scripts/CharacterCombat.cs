using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

    public float attackSpeed = 1f;
    public float attackDelay = 0.5f;
    public bool inCombat
    {
        get;
        private set;
    }

    private float attackCooldown = 0f;
    private const float combatCooldown = 5f;
    private float lastAttackTime;
    private CharacterStats myStats;
    private CharacterStats opponentStats;

    public event System.Action OnAttack;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;

        if(Time.time - lastAttackTime >= combatCooldown)
        {
            inCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if(attackCooldown <= 0f)
        {
            opponentStats = targetStats;

            if(OnAttack != null)
            {
                OnAttack.Invoke();
            }

            attackCooldown = 1f / attackSpeed;
            inCombat = true;
            lastAttackTime = Time.time;
        }
    }


    public void AttackHitAnimationEvent()
    {
        opponentStats.TakeDamage(myStats.damage.GetValue());

        if (opponentStats.currentHealth <= 0)
        {
            inCombat = false;
        }
    }
}
