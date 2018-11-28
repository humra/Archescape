using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    protected Animator animator;
    private NavMeshAgent agent;

    [SerializeField]
    private float dampTime = 0.1f;

	protected virtual void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
	}
	
	protected virtual void Update () {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, dampTime, Time.deltaTime);
	}
}
