using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject finalScene;

    public int currentLevel;
    public LevelController[] levelArray;
    public int numberOfLevelFruits;
    public int currentCollectedFruits;

    public bool isPlayerDead = false;

    public void Start()
    {
        currentLevel = 0;
        mainCamera.backgroundColor = Color.black;

        StartCoroutine(WaitForNewLevel(50 * Time.deltaTime));
    }

    private void FixedUpdate()
    {
        if (isPlayerDead)
        {
            if (Input.anyKeyDown)
            {
                isPlayerDead = false;
                currentLevel = 0;
                mainCamera.backgroundColor = Color.black;

                StartCoroutine(WaitForNewLevel(50 * Time.deltaTime));
            }
        }
    }

    public void StartNewLevel()
    {
        mainCamera.backgroundColor = Color.white;
        levelArray[currentLevel].gameObject.SetActive(true);

        numberOfLevelFruits = levelArray[currentLevel].fruits.Length;
        for (int i = 0; i < numberOfLevelFruits; i++)
        {
            levelArray[currentLevel].fruits[i].SetActive(true);
        }
        currentCollectedFruits = 0;

        levelArray[currentLevel].playerGameObject.transform.position = levelArray[currentLevel].playerInitialPosition;
        levelArray[currentLevel].playerGameObject.SetActive(true);
        levelArray[currentLevel].playerHeart.SetActive(false);
    }

    public void FinishGame()
    {
        mainCamera.backgroundColor = Color.white;
        StartCoroutine(WaitForEnd(500 * Time.deltaTime));
    }

    public void FinishLevel()
    {
        StartCoroutine(ShowHeart());
    }

    public void PlayerGotFruit()
    {
        currentCollectedFruits++;
        if(currentCollectedFruits >= numberOfLevelFruits)
        {
            FinishLevel();
        }
    }

    public void PlayerDied()
    {
        //Hide all levels
        for (int i = 0; i < levelArray.Length; i++)
        {
            GameObject levelGameObject = levelArray[i].gameObject;
            levelGameObject.SetActive(false);
        }

        mainCamera.backgroundColor = Color.black;
        isPlayerDead = true;
    }

    IEnumerator WaitForNewLevel(float duration)
    {
        yield return new WaitForSeconds(duration);
        StartNewLevel();
    }

    IEnumerator WaitForEnd(float duration)
    {
        finalScene.SetActive(true);
        yield return new WaitForSeconds(duration);
        finalScene.SetActive(false);
        PlayerDied();
    }

    IEnumerator ShowHeart()
    {
        levelArray[currentLevel].playerHeart.SetActive(true);
        yield return new WaitForSeconds(50 * Time.deltaTime);

        mainCamera.backgroundColor = Color.black;
        for (int i = 0; i < levelArray.Length; i++)
        {
            GameObject levelGameObject = levelArray[i].gameObject;
            levelGameObject.SetActive(false);
        }

        levelArray[currentLevel].playerHeart.SetActive(false);
        currentLevel++;

        if (currentLevel >= levelArray.Length)
        {
            FinishGame();
        } else
        {
            StartNewLevel();
        }
    }
}