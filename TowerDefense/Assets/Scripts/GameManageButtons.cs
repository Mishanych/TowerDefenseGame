using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManageButtons : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void StartGame()
    {
        _animator.SetBool("StartFading", false);
        Debug.LogWarning("PLAY");
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        _animator.SetBool("StartFading", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
