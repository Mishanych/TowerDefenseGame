using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerData PlayerData;
    [SerializeField] private TextMeshProUGUI _healthAmountUI;
    [SerializeField] private TextMeshProUGUI _moneyAmountUI;
    [SerializeField] private TextMeshProUGUI _wavesAmountUI;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameObject _gameEndScreen;
    [SerializeField] private GameObject _gameOverText;
    [SerializeField] private GameObject _gameWonText;
    [SerializeField] private List<GameObject> _listOfBuildMenus;
    [SerializeField] private List<GameObject> _listOfExtraMenus;

    [HideInInspector] public int HealthAmount;
    [HideInInspector] public int MoneyAmount;
    [HideInInspector] public int EnemyWaves;
    [HideInInspector] public bool GameOver;
    [HideInInspector] public bool PlayerWon;
    [HideInInspector] public int NumberOfCurrWave;
    [HideInInspector] public List<GameObject> AllEnemies;
    [HideInInspector] public List<GameObject> AllDefenders;



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
        PlayerWon = false;
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
        _wavesAmountUI.text = "Waves " + NumberOfCurrWave.ToString() + "/" + EnemyWaves.ToString();
    }


    private void Update()
    {
        if (HealthAmount <= 0 && !GameOver)
        {
            GameOver = true;
            OpenGameEndScreen();
        }
        if(NumberOfCurrWave == EnemyWaves && !PlayerWon && !GameOver)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if(enemies.Length == 0)
            {
                PlayerWon = true;
                OpenGameEndScreen();
            }
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

    public void OpenGameEndScreen()
    {
        _gameEndScreen.SetActive(true);
        if(GameOver)
        {
            _gameOverText.SetActive(true);
            StartCoroutine(TextFlickering(_gameOverText));
        }

        if(PlayerWon)
        {
            _gameWonText.SetActive(true);
            StartCoroutine(TextFlickering(_gameWonText));
        }

    }

    private IEnumerator TextFlickering(GameObject text)
    {
        text.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        text.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        text.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        text.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        text.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        text.SetActive(true);

        yield return new WaitForSeconds(1f);
        GoMainMenu();
    }


    public void PauseGame()
    {
        _pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        Debug.LogWarning("Game is paused");
    }

    public void ResumeGame()
    {
        _pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        Debug.LogWarning("Game is resumed");
    }

    public void RestartGame()
    {
        _pauseScreen.SetActive(false);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        Debug.LogWarning("Game is restarted");
    }

    public void GoMainMenu()
    {
        _pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Debug.LogWarning("Go to the main menu");
    }
}
