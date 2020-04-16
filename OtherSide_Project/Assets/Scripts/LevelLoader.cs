using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator anim;
    public float transitionTime = 1f;
    public static LevelLoader i { get; set; }
    private void Awake()
    {
        i = this;
    }
    public void LoadChooseScene()
    {
        if (PlayerPrefs.GetInt("DefaultLevelDone") == 0)
        {
            LoadLevel(2);
        }
        else
        {
            LoadLevel(1);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadMenu()
    {
        LoadLevel(0);
    }
    public void LoadLevel(int index)
    {
        StartCoroutine(LoadScene(index));
        PlayerPrefs.SetInt("DoText", 1);
    }
    public void LoadRestartLevel(float time)
    {
        PlayerPrefs.SetInt("DoText", 0);
        StartCoroutine(Wait(time, () =>
        {

            StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
        }));
    }
    IEnumerator Wait(float time, Action onEnd)
    {
        yield return new WaitForSeconds(time);
        onEnd?.Invoke();
    }
    public void LoadNextLevel()
    {
        PlayerPrefs.SetInt("DoText", 1);
        int levelReached = PlayerPrefs.GetInt("LevelReached") + 2;
        int activeIndex = SceneManager.GetActiveScene().buildIndex;
        if (activeIndex + 1 > levelReached)
        {
            PlayerPrefs.SetInt("LevelReached", activeIndex - 1);
        }


        if (activeIndex == 2 && PlayerPrefs.GetInt("DefaultLevelDone") == 0)
        {
            PlayerPrefs.SetInt("DefaultLevelDone", 1);
        }
        StartCoroutine(LoadScene(activeIndex + 1));
    }
    IEnumerator LoadScene(int levelIndex)
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
