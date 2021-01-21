using UnityEngine;
using UnityEngine.AI;

public class ShipController : MonoBehaviour, IDamagable
{
    private NavMeshAgent agent;
    public AudioClip dieClip;
    public GameObject dieParticle;

    [Header("HEALTH")]
    public int health = 5;

    private void Start() {
        Transform targetPoint = ShipsManager.ins.targetPoints[Random.Range(0, ShipsManager.ins.targetPoints.Length)];
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPoint.position);
    }

    private void Update() {
        if(agent.remainingDistance != Mathf.Infinity && agent.pathStatus==UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance==0){
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage){
        if(health > 0)
            health -= damage;
        else
            Die();
    }

    private void Die(){
        AudioManager.ins.PlayClip(dieClip);
        Instantiate(dieParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("goal"))
            GameManager.ins.GameOver();
    }
}
