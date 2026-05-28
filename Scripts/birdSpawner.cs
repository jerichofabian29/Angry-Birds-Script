using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class birdSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]public List<GameObject> birds;
    public GameObject birdPrefab;
    public Transform spawnPoint;
    int attempts = 4;

    public TMP_Text attempt;

    void Start()
    {
        for (int i = 0; i <= attempts; i++)
        {
            GameObject tmp = Instantiate(birdPrefab);
            tmp.SetActive(false);
            birds.Add(tmp);
        }
       spawnBird();
    }

    // Update is called once per frame
    void Update()
    {
        attempt.text = birds.Count.ToString();
    }

    public void spawnBird()
    {
        if (GameObject.FindGameObjectsWithTag("Red Bird").Length <= 0 &&
            GameObject.FindGameObjectsWithTag("Big Bird").Length <= 0 &&
            GameObject.FindGameObjectsWithTag("Green Bird").Length <= 0 &&
            GameObject.FindGameObjectsWithTag("Spinning Bird").Length <= 0 &&
            birds.Count != 0)
        {
            birds[0].transform.position = spawnPoint.position;
            birds[0].SetActive(true);
            birds.Remove(birds[0]);
        }
    }
}
