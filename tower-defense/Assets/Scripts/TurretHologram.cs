using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHologram : MonoBehaviour{
    public bool canPlaceTurret = true;
    public Renderer rend;
    private Material material;

    private void Awake() {
        material = rend.material;
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("road") || other.CompareTag("turret")){
            canPlaceTurret = false;
            material.color = new Color32(255, 0, 0, 80);    
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("road") || other.CompareTag("turret")){
            canPlaceTurret = true;
            material.color = new Color32(0, 255, 0, 150);  
        }
    }
}
