using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMenuItem : MonoBehaviour
{
    public GameObject turretPrefab;

    public void SelectTurret(){
        TurretSetterController.ins.PlaceTurretHologram(turretPrefab);//usar arreglos para relacionar los botones con los objetos 
    }
}
