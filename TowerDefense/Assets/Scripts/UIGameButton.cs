using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIGameButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent MouseClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        MouseClick?.Invoke();
    }
}
