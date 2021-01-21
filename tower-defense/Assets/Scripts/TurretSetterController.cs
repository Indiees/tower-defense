using UnityEngine;

public class TurretSetterController : MonoBehaviour
{
    public static TurretSetterController ins;

    private new Camera camera;
    public ClickInteractionDetector clickInteractionDetector;

    [Header("RAYCAST")]
    public LayerMask layerToPlaceTurret;
    
    [Header("TURRETS")]
    private bool isReadyPlaceTurret;
    private GameObject turretSelected;
    private Vector3 turretPosition;

    [Header("HOLOGRAM")]
    public TurretHologram turretHologram;
    private Vector3 positionToPlaceTurret;

    private void Awake() {
        if(ins != null && ins != this)  
            Destroy(this.gameObject);
        else 
            ins = this;

        camera = Camera.main;
        clickInteractionDetector.onClick.AddListener(OnClick);
    }

    private void FixedUpdate(){
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 300, layerToPlaceTurret) && isReadyPlaceTurret){
            turretPosition = hit.point;
            positionToPlaceTurret = new Vector3(Mathf.FloorToInt(turretPosition.x), transform.position.y, Mathf.FloorToInt(turretPosition.z));
            turretHologram.gameObject.transform.position = positionToPlaceTurret;            
        }
    }

    public void PlaceTurretHologram(GameObject turret){
        turretSelected = turret;        
        turretHologram.gameObject.SetActive(true);
        turretHologram.canPlaceTurret = true;
        isReadyPlaceTurret = true;
    }    

    private void OnClick(){
        if(turretHologram.canPlaceTurret){
            Instantiate(turretSelected, positionToPlaceTurret, Quaternion.identity);
            turretHologram.canPlaceTurret = false;
            turretHologram.gameObject.SetActive(false);
            isReadyPlaceTurret = false;
        } 
    }
}
