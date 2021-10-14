using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildOptionProperties : MonoBehaviour
{
    public GameObject PriceLabel;
    public GameObject ConfirmIcon;

    public string NameOfBuilding;

    [HideInInspector] public int Price;

    private void Awake()
    {
        var tmPro = PriceLabel.GetComponent<TextMeshProUGUI>();
        Price = Convert.ToInt32(tmPro.text);
    }
}
