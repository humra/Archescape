using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactible {

    public IEnemyHandler enemyHandler;

    private PlayerManager playerManager;
    private CharacterStats myStats;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    private void OnMouseDown()
    {
        enemyHandler.SetPlayerFocus(this.gameObject);
    }

    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();

        if(playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
}
