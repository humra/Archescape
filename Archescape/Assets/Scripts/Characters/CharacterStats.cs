public class CharacterStats {

    public int maxHealth;
    public int currentHealth;
    public int damage;
    public int armour;
    public float attackSpeed;

    public CharacterStats() { }

    public CharacterStats(EnemyType type)
    {
        switch(type)
        {
            case EnemyType.Skeleton:
                maxHealth = 25;
                currentHealth = maxHealth;
                damage = 5;
                armour = 3;
                attackSpeed = 1f;
                break;
            default:
                break;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }

    public float GetHealthPercentage()
    {
        return currentHealth / (float)maxHealth;
    }
}
