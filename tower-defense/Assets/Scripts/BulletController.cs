using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 5.0f;
    public int damage;

    private void OnEnable() {
        Invoke("Deactivate", 3);    
    }

    private void Update() {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void Deactivate(){
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("enemy")){
            other.gameObject.GetComponent<IDamagable>().TakeDamage(damage);
            gameObject.SetActive(false);
        } 
    }
}
