using UnityEngine;

public class CharacterAnimationEventReciever : MonoBehaviour {

    public CharacterCombat combat;

	public void AttackHitEvent()
    {
        combat.AttackHitAnimationEvent();
    }
}
