using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehaviour : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Color _lowHealthColor;
    [SerializeField] private Color _highHealthColor;


    public void SetHealth(float health, float maxHealth)
    {
        _slider.gameObject.SetActive(health < maxHealth);
        _slider.value = health;
        _slider.maxValue = maxHealth;

        _slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(_lowHealthColor, _highHealthColor, _slider.normalizedValue);
    }


}
