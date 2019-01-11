using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public int maxHealth = 10;
    public int currentHealth;
    public int damage;
    public int armour;
    public float attackSpeed = 1f;
    public IDeathHandler deathHandler;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public float GetHealthPercentage()
    {
        return currentHealth / (float)maxHealth;
    }

    public virtual void Die()
    {
        deathHandler.EnemyDeath(gameObject);
    }
}
