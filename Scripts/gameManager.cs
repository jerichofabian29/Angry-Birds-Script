using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{

    int pigCount = 0;
    public TMP_Text text;
    public Canvas gameClear;
    public Canvas gameOverUI;
    public Canvas pauseUI;
    public Canvas tutorialUI;
   

    public bool isLevel1 = false;
    public bool isLevel3 = false;

    public GameObject blackOverlay;


    birdSpawner birdSpawner;
    void Start()
    {
        birdSpawner = GameObject.Find("Slingshot").GetComponent<birdSpawner>();
        countPigs();
        if(isLevel1 == true || isLevel3 == true)
        {
            StartCoroutine(showTutorialUI());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (birdSpawner.birds.Count == 0 && 
            (GameObject.FindGameObjectsWithTag("Red Bird").Length == 0 || GameObject.FindGameObjectsWithTag("Big Bird").Length == 0 || GameObject.FindGameObjectsWithTag("Green Bird").Length == 0 || GameObject.FindGameObjectsWithTag("Spinning Bird").Length == 0) &&
            (GameObject.FindGameObjectsWithTag("Pig King").Length > 0  && GameObject.FindGameObjectsWithTag("Normal Pig").Length > 0))
        {
            gameOverUI.gameObject.SetActive(true);
            blackOverlay.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            pauseUI.gameObject.SetActive(true);
            blackOverlay.SetActive(true);
        }

    }

    public void countPigs()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Pig King").Length; i++)
        {
            pigCount++;
            text.text = pigCount.ToString();
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Normal Pig").Length; i++)
        {
            pigCount++;
            text.text = pigCount.ToString();
        }
    }
    public void decreasePigCount(int deduction)
    {
        pigCount -= deduction;
        text.text = pigCount.ToString();
        if (pigCount == 0)
        {
            gameClear.gameObject.SetActive(true);
            blackOverlay.SetActive(true);
        }
    }

    private IEnumerator showTutorialUI()
    {
       yield return new WaitForSeconds(0.2f);
       blackOverlay.SetActive(true);
       tutorialUI.gameObject.SetActive(true);
    }
}
