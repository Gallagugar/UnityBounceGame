using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    int toSpawn;
    int lastX;
    [SerializeField] GameObject platform;

    [SerializeField] Transform coins;
    [SerializeField] Coin coin;

	// Use this for initialization
	public void Start () {
        toSpawn = -10;
        lastX = 0;
        Instantiate(platform, new Vector3(0, -1.2f, toSpawn), Quaternion.identity, transform);
        toSpawn += 10;
        Instantiate(platform, new Vector3(0, -1.2f, toSpawn), Quaternion.identity, transform);
        toSpawn += 10;
        for (int i = 0; i < 8; i++)
        {
            Spawn();
        }
	}

    public void Spawn()
    {
        int xPos;
        do
        {
            xPos = Random.Range(-6, 7);
        } while (xPos > -3 && xPos < 3);
        //xPos = 0;
        lastX += xPos;
        Instantiate(platform, new Vector3(lastX, -1.2f, toSpawn), Quaternion.identity, transform);
        if (Random.Range(0, 6) == 5)
            Instantiate(coin, new Vector3(lastX, 0f, toSpawn), Quaternion.identity, coins);
        toSpawn += 10;
    }

    public void Despawn()
    {
        Destroy(transform.GetChild(0).gameObject);
    }

    public void DespawnAll()
    {
        foreach (Transform plat in transform)
            Destroy(plat.gameObject);
        foreach (Transform coin in coins)
            Destroy(coin.gameObject);
    }
    
}
