using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{

    public UnityEvent downEvent;
    public UnityEvent upEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("TTS button pressed");
        downEvent?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("TTS button released");
        upEvent?.Invoke();
    }

    
}
