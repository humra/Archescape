using UnityEngine;

public class CombatManager : MonoBehaviour {

    public PlayerStats playerStats;
    public CharacterStats enemyStats;
    public bool playerBeingAttacked = false;
    public bool enemyBeingAttacked = false;

    private float playerAttackTimestamp;
    private float enemyAttackTimestamp;

    private void Update()
    {
        if(playerStats == null || enemyStats == null)
        {
            return;
        }

        if(Time.time - playerAttackTimestamp >= playerStats.attackSpeed && enemyBeingAttacked)
        {
            int damage = playerStats.damage - enemyStats.armour;
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            enemyStats.TakeDamage(damage);

            Debug.Log("Enemy takes " + damage + " damage");

            playerAttackTimestamp = Time.time;
        }
        if (Time.time - enemyAttackTimestamp >= enemyStats.attackSpeed && playerBeingAttacked)
        {
            int damage = enemyStats.damage - playerStats.armour;
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            playerStats.TakeDamage(damage);

            Debug.Log("Player takes " + damage + " damage");

            enemyAttackTimestamp = Time.time;
        }
    }

    public void StopAllCombat()
    {
        playerBeingAttacked = false;
        enemyBeingAttacked = false;
        enemyStats = null;
    }

    public void ResetTimestamps()
    {
        playerAttackTimestamp = Time.time;
        enemyAttackTimestamp = Time.time;
    }
}
