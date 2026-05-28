using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour
{
    public GameObject blackOverlay;
    public void GoToTutorial()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToLevels()
    {
        SceneManager.LoadScene("Levels");
    }
    public void GoToLevel1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1");
    }
    public void GoToLevel2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 2");
    }
    public void GoToLevel3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 3");
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        if(GameObject.Find("Pause UI") != null)
        {
            GameObject.Find("Pause UI").GetComponent<Canvas>().gameObject.SetActive(false);
        }
        blackOverlay = GameObject.Find("Black Overlay");
        blackOverlay.SetActive(false);
        if(GameObject.Find("Tutorial UI") != null)
        {
            GameObject.Find("Tutorial UI").GetComponent<Canvas>().gameObject.SetActive(false);
        }

    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
