using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformObj : MonoBehaviour
{
    GameObject player;
    BoxCollider2D boxCollider;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
    }
    void Update()
    {
        if (transform.position.y > player.transform.position.y - 0.28f)
        {
            boxCollider.enabled = false;
        }
        else if (transform.position.y < player.transform.position.y - 0.42f)
        {
            boxCollider.enabled = true;
        }

        if (transform.position.y < player.transform.position.y - 10f)
        {
            gameObject.SetActive(false);
            PlatformSpawner.Instance.platformsInGame.Remove(gameObject);
            PlatformSpawner.Instance.platformsList.Insert(0, gameObject);
        }
    }
}
