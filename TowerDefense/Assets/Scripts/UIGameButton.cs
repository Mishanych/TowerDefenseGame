using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGameButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent MouseClick;

    [SerializeField] bool _enabled = true;


    public void OnPointerClick(PointerEventData eventData)
    {
        if(_enabled)
            MouseClick?.Invoke();
    }

    public void SwitchEnable()
    {
        if (_enabled)
            DisableButton();
        else
            EnableButton();
    }

    public void DisableButton()
    {
        _enabled = false;
        foreach (Image btnImage in gameObject.GetComponentsInChildren<Image>())
        {
            if (btnImage != null)
            {
                var color = btnImage.color;
                color.a = 0.5f;
                btnImage.color = color;
            }
        }
    }

    public void EnableButton()
    {
        _enabled = true;
        foreach (Image btnImage in gameObject.GetComponentsInChildren<Image>())
        {
            if (btnImage != null)
            {
                var color = btnImage.color;
                color.a = 1f;
                btnImage.color = color;
            }
        }
    }
}
