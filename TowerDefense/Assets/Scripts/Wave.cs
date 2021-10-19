using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour
{
    [SerializeField] private List<WaveData> _allWaves;
    [SerializeField] private List<GameObject> _spawnPoints;
    [SerializeField] private GameObject _background;
    

    public Image SkullIcon;

    private WaveData _currWave;
    private bool _isReadyForWaves = false;

    private int _amountOfWaves;
    private int _numberOfCurrWave;

    private float _coolDown;

    private void Start()
    {
        _amountOfWaves = GameManager.Instance.EnemyWaves;
        _coolDown = _allWaves[0].WaveDuration;
        _currWave = _allWaves[0];
        _numberOfCurrWave = 1;
        GameManager.Instance.NumberOfCurrWave = _numberOfCurrWave;
    }

    private void Update()
    {
        if(AbleToStartNewWave() && _numberOfCurrWave - 1 != _amountOfWaves && _numberOfCurrWave != 1)
        {
            SetupNewWave();
            StartWave();
        }

        if (_coolDown > 0 && _isReadyForWaves)
            _coolDown -= Time.deltaTime;
        
    }


    private bool AbleToStartNewWave()
    {
        if (_coolDown <= 0)
            return true;
        return false;
    }

    private void SetupNewWave()
    {
        
        _currWave = _allWaves[_numberOfCurrWave - 1];
        _coolDown = _currWave.WaveDuration;
        GameManager.Instance.NumberOfCurrWave = _numberOfCurrWave;
    }



    public void OpenWaveInfo()
    {
        //info about enemies
        SkullIcon.color = Color.red;
        _isReadyForWaves = true;
    }

    public void StartWave()
    {
        if(_isReadyForWaves && SkullIcon.color == Color.red)
        {
            _background.SetActive(false);
            SkullIcon.gameObject.SetActive(false);
            Debug.LogWarning("Wave: " + _numberOfCurrWave);


            for (int i = 0; i < _currWave.ListOfEnemiesInWaveInfo.Count; i++)
            {
                for (int j = 0; j < _currWave.ListOfEnemiesInWaveInfo[i].NumberOfEnemies; j++)
                {
                    var enemy = Instantiate(_currWave.ListOfEnemiesInWaveInfo[i].Enemy, transform.parent);
                    enemy.transform.SetParent(transform.parent);

                    var spawnPoint = _spawnPoints[_currWave.ListOfEnemiesInWaveInfo[i].IndexOfSpawnPoint];
                    var rect = spawnPoint.GetComponent<RectTransform>();
                    enemy.transform.localPosition = SetRandomSpawn(spawnPoint.transform.localPosition.x, spawnPoint.transform.localPosition.y, rect.rect.width, rect.rect.height);

                }
            }

            _numberOfCurrWave++;
        }
    }

    private Vector3 SetRandomSpawn(float posX, float posY, float width, float height)
    {
        float x = Random.Range(posX - (width / 2), posX + (width / 2));
        float y = Random.Range(posY - (height / 2), posY + (height / 2));

        return new Vector3(x, y, 0);
    }

    

}
