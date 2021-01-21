using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickInteractionDetector : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent onClick;

    public void OnPointerClick(PointerEventData pointerEventData){
        onClick.Invoke();
    }
}
