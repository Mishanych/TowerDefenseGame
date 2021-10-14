using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _listOfBuildMenus;


    private static GameManager _instance;

    public static GameManager Instance()
    {
        if (_instance == null)
            _instance = new GameManager();
        return _instance;
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

        buildMenu.SetActive(true);
    }
}
