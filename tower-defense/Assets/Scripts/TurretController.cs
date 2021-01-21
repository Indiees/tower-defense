using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("TURRET")]
    public Transform canon;
    public Transform canonJoint;
    [Header("SHOOTING")]    
    public AudioClip shootClip;
    public GameObject bulletPrefab;
    public int bulletsAmount = 10;
    public float intervalToShoot = 2.0f;
    public float rangeToAttack = 2.0f;
    private List<GameObject> bullets = new List<GameObject>();
    private float counterToNextShoot;
    private bool canShoot;
    [Header("DETECTION")]
    public LayerMask enemyLayer;
    private Transform target;

    private void Start() {
        GameObject bulletsContainer = new GameObject("BulletsCotainer"+gameObject.name);
        for (int i = 0; i < bulletsAmount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.SetActive(false);
            bullets.Add(bullet);
            bullet.transform.SetParent(bulletsContainer.transform);
        }
    }

    private void Update() {
        Collider[] enemies = Physics.OverlapSphere(transform.position, rangeToAttack, enemyLayer);
        if(enemies.Length > 0){
            target = enemies[0].gameObject.transform; 
            canShoot = true;           
            if(Vector3.Distance(transform.position, target.position) < rangeToAttack && canShoot && target != null){
                canon.transform.LookAt(target, Vector3.up);
                counterToNextShoot += Time.deltaTime;
                if(counterToNextShoot >= intervalToShoot){
                    Shoot();
                    counterToNextShoot = 0;
                }   
            }else{
                canShoot = false;
            }
        }
    }    

    private GameObject GetFreeBullet(){
        for (int i = 0; i < bullets.Count; i++)
            if(!bullets[i].activeSelf) return bullets[i];

        return null;
    }

    private void PrepareBullet(GameObject bullet){
        bullet.transform.position = canonJoint.position;
        bullet.transform.forward = canonJoint.forward;
        bullet.SetActive(true);
    }

    private void Shoot(){
        GameObject bullet = GetFreeBullet();
        PrepareBullet(bullet);        
        AudioManager.ins.PlayClip(shootClip); 
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeToAttack);
    }
}
