using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private GameObject _buildPlace;
    [SerializeField] private List<GameObject> _listOfBuildOptions;

    private GameObject _currentBuilding;
    private GameObject _building;


    public void CheckConfirmButtonState(BuildOptionProperties buildOptionProperties)
    {
        _buildPlace.SetActive(false);
        ShowBuilding(buildOptionProperties);

        if (!buildOptionProperties.ConfirmIcon.activeSelf)
        {
            ShowOrHideConfirmButton(buildOptionProperties, true);

            for (int i = 0; i < _listOfBuildOptions.Count; i++)
            {
                var buildOP = _listOfBuildOptions[i].GetComponent<BuildOptionProperties>();
                if (buildOP != buildOptionProperties)
                {
                    ShowOrHideConfirmButton(buildOP, false);
                }
            }
        }
        else
        {
            _listOfBuildOptions[0].transform.parent.gameObject.SetActive(false);
            //money--
        }
    }


    private void ShowOrHideConfirmButton(BuildOptionProperties buildOptionProperties, bool show)
    {
        foreach (Transform childTransform in buildOptionProperties.gameObject.GetComponentInChildren<Transform>())
        {
            childTransform.gameObject.SetActive(!show);
        }

        buildOptionProperties.ConfirmIcon.SetActive(show);
    }


    private void ShowBuilding(BuildOptionProperties buildOptionProperties)
    {
        if(_building != null)
        {
            Destroy(_building);
        }
        _currentBuilding = Resources.Load("BuildingPrefabs/" + buildOptionProperties.NameOfBuilding) as GameObject;

        var localTransform = _currentBuilding.transform;
        _building = Instantiate(_currentBuilding, _currentBuilding.transform.position, Quaternion.identity, gameObject.transform);

        _building.transform.SetParent(gameObject.transform);
        _building.transform.localPosition = localTransform.position;
        _building.transform.localScale = localTransform.localScale;
    }
}
