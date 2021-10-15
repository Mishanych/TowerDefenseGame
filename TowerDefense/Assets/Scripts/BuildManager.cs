using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    [SerializeField] public GameObject BuildPlace;
    [SerializeField] public GameObject Tower;
    [SerializeField] private GameObject _buildMenu;
    [SerializeField] private GameObject _extraMenu;

    public List<GameObject> ListOfBuildOptions;
    [HideInInspector] public TowerData CurrentTowerData;

    public bool TowerIsBuilded = false;


    public void CheckBuildOptionIcons(BuildOptionSettings currentOption)
    {
        for (int i = 0; i < ListOfBuildOptions.Count; i++)
        {
            var buildOptionSettings = ListOfBuildOptions[i].GetComponent<BuildOptionSettings>();
            if(buildOptionSettings != currentOption)
            {
                buildOptionSettings.SetBuildOptions();
            }
        }
    }

    public void OpenExtraMenu()
    {
        if (!_buildMenu.activeSelf)
        {
            _extraMenu.SetActive(true);
            var tmPro = _extraMenu.GetComponentInChildren<TextMeshProUGUI>();
            tmPro.text = (CurrentTowerData.BuildPrice / 2).ToString();
        }
    }

    public void SellTower()
    {
        if (TowerIsBuilded)
        {
            Tower.GetComponent<Image>().enabled = false;
            _extraMenu.SetActive(false);
            BuildPlace.SetActive(true);
            foreach (var option in ListOfBuildOptions)
            {
                option.GetComponent<BuildOptionSettings>().SetBuildOptions();
            }
            TowerIsBuilded = false;
            //money++
        }
    }
    
}
