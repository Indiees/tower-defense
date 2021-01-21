using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "New Wave", order = 0)]
public class EnemyWave : ScriptableObject {
    public GameObject[] enemies;
}