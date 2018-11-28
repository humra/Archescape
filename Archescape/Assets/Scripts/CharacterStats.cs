using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public int maxHealth = 10;
    public int currentHealth;
    public int damage;
    public int armour;
    public float attackSpeed = 1f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(transform.name + " takes " + damageAmount + " damage.");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died");
    }
}
