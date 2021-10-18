using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerData PlayerData;
    [SerializeField] private TextMeshProUGUI _healthAmountUI;
    [SerializeField] private TextMeshProUGUI _moneyAmountUI;
    [SerializeField] private TextMeshProUGUI _wavesAmountUI;
    [SerializeField] private List<GameObject> _listOfBuildMenus;
    [SerializeField] private List<GameObject> _listOfExtraMenus;

    [HideInInspector] public int HealthAmount;
    [HideInInspector] public int MoneyAmount;
    [HideInInspector] public int EnemyWaves;
    [HideInInspector] public bool GameOver;


    
    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        GameOver = false;
        SetupPlayerProperties();
        DisplayPlayerProperties();
    }


    private void SetupPlayerProperties()
    {
        HealthAmount = PlayerData.HealthAmount;
        MoneyAmount = PlayerData.MoneyAmount;
        EnemyWaves = PlayerData.EnemyWaves;
    }
    private void DisplayPlayerProperties()
    {
        _healthAmountUI.text = HealthAmount.ToString();
        _moneyAmountUI.text = MoneyAmount.ToString();
        _wavesAmountUI.text = "Waves 0/" + EnemyWaves.ToString();
    }


    private void Update()
    {
        if(HealthAmount <= 0)
        {
            GameOver = true;
        }
        DisplayPlayerProperties();
    }

    public void OpenBuildMenu(GameObject buildMenu)
    {
        for (int i = 0; i < _listOfBuildMenus.Count; i++)
        {
            if (_listOfBuildMenus[i].activeSelf)
            {
                _listOfBuildMenus[i].SetActive(false);
            }
        }

        for (int i = 0; i < _listOfExtraMenus.Count; i++)
        {
            if (_listOfExtraMenus[i].activeSelf)
            {
                _listOfExtraMenus[i].SetActive(false);
            }
        }

        buildMenu.SetActive(true);
    }

    public void CloseBuildMenu()
    {
        for (int i = 0; i < _listOfBuildMenus.Count; i++)
        {
            if (_listOfBuildMenus[i].activeSelf)
            {
                _listOfBuildMenus[i].SetActive(false);
                var buildManager = _listOfBuildMenus[i].transform.parent.gameObject.GetComponent<BuildManager>();
                buildManager.Tower.GetComponent<Image>().enabled = false;
                //foreach (var option in buildManager.ListOfBuildOptions)
                //{
                //    option.GetComponent<BuildOptionSettings>().SetBuildOptions();
                //}
                buildManager.BuildPlace.SetActive(true);
            }
        }

        for (int i = 0; i < _listOfExtraMenus.Count; i++)
        {
            if (_listOfExtraMenus[i].activeSelf)
                _listOfExtraMenus[i].SetActive(false);
        }
    }

    public void OpenExtraMenu(GameObject extraMenu)
    {
        for (int i = 0; i < _listOfExtraMenus.Count; i++)
        {
            if (_listOfExtraMenus[i].activeSelf)
            {
                _listOfExtraMenus[i].SetActive(false);
            }
        }

        for (int i = 0; i < _listOfBuildMenus.Count; i++)
        {
            if (_listOfBuildMenus[i].activeSelf)
            {
                _listOfBuildMenus[i].SetActive(false);
            }
        }

        extraMenu.SetActive(true);
    }
}
