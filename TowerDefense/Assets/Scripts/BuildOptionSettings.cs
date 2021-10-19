using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildOptionSettings : MonoBehaviour
{
    [SerializeField] private TowerData _towerData;
    [SerializeField] private GameObject _buildIcon;
    [SerializeField] private GameObject _buildPrice;
    [SerializeField] private GameObject _coinIcon;
    [SerializeField] private Sprite _confirmIcon;
    [SerializeField] private BuildManager _buildManager;

    private Image _currentBuildIcon;


    private void Awake()
    {
        _currentBuildIcon = _buildIcon.GetComponent<Image>();
        SetBuildOptions();
        
    }
    
    public void SetBuildOptions()
    {
        var btn = this.GetComponent<UIGameButton>();
        btn.EnableButton();
        if (GameManager.Instance.MoneyAmount < _towerData.BuildPrice)
        {
            btn.SwitchEnable();
        }



        if (!_buildPrice.activeSelf)
        {
            _buildPrice.SetActive(true);
        }

        if (!_buildIcon.activeSelf)
        {
            _buildIcon.SetActive(true);
        }

        if (!_coinIcon.activeSelf)
        {
            _coinIcon.SetActive(true);
        }

        _currentBuildIcon.sprite = _towerData.BuildOptionData.TowerIcon;


        var tmPro = _buildPrice.GetComponent<TextMeshProUGUI>();
        tmPro.text = _towerData.BuildOptionData.BuildPriceText;

    }

    public void ChooseOrSetUpBuilding()
    {
        if (_currentBuildIcon.sprite != _confirmIcon)
        {
            _currentBuildIcon.sprite = _confirmIcon;
            _buildPrice.SetActive(false);
            _coinIcon.SetActive(false);
        }
        else
        {
            this.transform.parent.gameObject.SetActive(false);
            _buildManager.TowerIsBuilded = true;
            GameManager.Instance.MoneyAmount -= _towerData.BuildPrice;
            Debug.LogWarning(GameManager.Instance.MoneyAmount);

            var tmPro = _buildManager.ExtraMenu.GetComponentInChildren<TextMeshProUGUI>();
            tmPro.text = _buildManager.CurrentTowerData.BuildPrice.ToString();
        }
    }
}
