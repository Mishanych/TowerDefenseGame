using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tower : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameEvent OnTowerSelected;
    [SerializeField] private GameObject _tower;
    [SerializeField] private GameObject _buildPlace;
    [SerializeField] private BuildManager _buildManager;


    public void OnPointerClick(PointerEventData eventData)
    {
        OnTowerSelected?.RaiseEvent();
    }

    public void ShowTower(TowerData towerData)
    {

        if (_buildPlace.activeSelf)
        {
            _buildPlace.SetActive(false);
        }

        var img = _tower.GetComponent<Image>();
        img.sprite = towerData.TowerIcon;
        img.enabled = true;

        _buildManager.CurrentTowerData = towerData;
    }

}
