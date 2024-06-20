using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner Instance;

    public GameObject platformPrefab;
    public List<GameObject> platformsList;
    public List <GameObject> platformsInGame;

    public static float currentHeight = -5f;
    private void Start()
    {
        Instance = this;

        platformsList = new List<GameObject>();
        platformsInGame = new List<GameObject>();

        for (int i = 0; i < 50; i++)
        {
            GameObject platform = Instantiate(platformPrefab);
            platform.SetActive(false);
            platformsList.Add(platform);
        }
    }
    private void Update()
    {
        if (platformsInGame.Count < 20 && platformsList.Count > 0)
        {
            GameObject nextPlatform = platformsList[platformsList.Count - 1];
            platformsList.RemoveAt(platformsList.Count - 1);
            platformsInGame.Add(nextPlatform);

            float height = UnityEngine.Random.Range(0.5f, 1.8f);
            float xPos = UnityEngine.Random.Range(-2.5f, 2.5f);
            currentHeight += height;
            nextPlatform.transform.position = new Vector2(xPos, currentHeight);

            nextPlatform.SetActive(true);
        }
    }

    public void ReloadPlatforms()
    {
        currentHeight = -5f;
        try
        {
            while (platformsInGame.Count > 0)
            {
                platformsInGame[0].SetActive(false);
                platformsList.Insert(0, platformsInGame[0]);
                platformsInGame.RemoveAt(0);
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Debug.LogError($"Error reloading platforms: {ex.Message}");
        }
    }
}
