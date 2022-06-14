using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;
    
    [SerializeField] RivalController rivalController1;
    [SerializeField] RivalController rivalController2;
    [SerializeField] RivalController rivalController3;

    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] GameObject gameOverScreen;
    
    private float spawnRange = 10.0f; // max distance of candy spawn
    private float spawnWait = 8.0f; // length of time before spawning new candy
    private int spawnNumber = 3; // number of candy objects to activate
    private float floorLevel = 0.25f;
    private int maxCandy; // max candy that can be activated at once
    private int candyNumberCheck; // check if candy won't go over maxCandy before spawning


    void Start()
    {
        maxCandy = GameObject.Find("Object Pooler").GetComponent<ObjectPooler>().amountToPool;
        SpawnCandy();
        StartCoroutine(SpawnMoreCandy());
    }

    private void SpawnCandy()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            Vector3 spawnPos = new Vector3
                (Random.Range(-spawnRange, spawnRange), floorLevel, Random.Range(-spawnRange, spawnRange));
            
            GameObject newCandy = ObjectPooler.SharedInstance.ActivateCandy();
            newCandy.SetActive(true);
            newCandy.transform.position = spawnPos;
        }
    }

    // Delay before spawning candy
    IEnumerator SpawnMoreCandy()
    {
        yield return new WaitForSeconds(spawnWait);
        if (isGameActive)
        {
            if (GameObject.FindGameObjectsWithTag("Pickup").Length <= candyNumberCheck)
            {
                SpawnCandy();
                rivalController1.GetTargetDelay();
                rivalController2.GetTargetDelay();
                rivalController3.GetTargetDelay();
                SetNewValues();
            }
            StartCoroutine(SpawnMoreCandy());
        }
    }

    // Determine random values
    private void SetNewValues()
    {
        spawnWait = Random.Range(1, 4);
        spawnNumber = Random.Range(1, 3);
        candyNumberCheck = maxCandy - spawnNumber; 
        // e.g. 14 if spawnNum = 1, which means spawn is okay because it will not go over 15
        // chose to cancel spawn and try again in this case, instead of reducing spawns to minimum acceptable
        // partially because it's easier but mostly because I want to preserve randomness
    }

    public void InitialiseGameOver(string victor)
    {
        isGameActive = false;
        gameOverText.text = "Game Over \n Winner: " + victor;
        // TODO build and show game over screen
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

// Do all rivals at once