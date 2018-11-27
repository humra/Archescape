using UnityEngine;

public class CharacterStats : MonoBehaviour {

    public int maxHealth = 10;
    public int currentHealth
    {
        get;
        private set;
    }
    public Stat damage;
    public Stat armour;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        damageAmount -= armour.GetValue();
        damageAmount = Mathf.Clamp(damageAmount, 0, int.MaxValue);

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
