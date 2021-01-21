using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsManager : MonoBehaviour{
    public static ShipsManager ins;

    [Header("IA")]
    public Transform[] targetPoints;
    public Transform startPoint;

    [Header("WAVES")]
    public List<EnemyWave> enemyWaves = new List<EnemyWave>();
    public int currentWave = 0;

    public float intervalBetweenWaves = 10;
    public float counterToNewWave = 0;
    private bool canStartTimer;
    public int totalEnemiesToKill;
    public int totalEnemiesKilled;

    private void Awake() {
        if(ins != null && ins != this) Destroy(this.gameObject);
        else ins = this;

        for (int i = 0; i < enemyWaves.Count; i++)
        {
            for (int j = 0; j < enemyWaves[i].enemies.Length; j++)
            {
                totalEnemiesToKill ++;
            }
        }
    }

    private void Update() {
        if(currentWave <= enemyWaves.Count-1 && canStartTimer && !GameManager.ins.isGameOver){            
            counterToNewWave += Time.deltaTime;
            if(counterToNewWave >= intervalBetweenWaves){
                SelectWave();
                counterToNewWave = 0;
            }  
        }
    }

    public void CheckEnemiesToWin(){
        totalEnemiesKilled ++;
        if(totalEnemiesKilled == totalEnemiesToKill)
            GameManager.ins.WinGame();
    }

    public void SelectWave(){
        StartCoroutine(InitWave(enemyWaves[currentWave]));
        currentWave ++;
    }

    private IEnumerator InitWave(EnemyWave wave){
        canStartTimer = false;
        StartCoroutine(GameManager.ins.NewWave());
        for (int i = 0; i < wave.enemies.Length; i++)
        {
            Instantiate(wave.enemies[i], startPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
        canStartTimer = true;
        yield return null;
    }
}
