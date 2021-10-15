using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _listOfBuildMenus;
    [SerializeField] private List<GameObject> _listOfExtraMenus;


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

    public void CloseBuildMenu()
    {
        for (int i = 0; i < _listOfBuildMenus.Count; i++)
        {
            if (_listOfBuildMenus[i].activeSelf)
            {
                _listOfBuildMenus[i].SetActive(false);
                var buildManager = _listOfBuildMenus[i].transform.parent.gameObject.GetComponent<BuildManager>();
                buildManager.Tower.GetComponent<Image>().enabled = false;
                foreach (var option in buildManager.ListOfBuildOptions)
                {
                    option.GetComponent<BuildOptionSettings>().SetBuildOptions();
                }
                buildManager.BuildPlace.SetActive(true);
            }
        }

        for (int i = 0; i < _listOfExtraMenus.Count; i++)
        {
            if (_listOfExtraMenus[i].activeSelf)
                _listOfExtraMenus[i].SetActive(false);
        }
    }
}
