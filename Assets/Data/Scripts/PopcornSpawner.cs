using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornSpawner : MonoBehaviour
{
    public float timer1;
    public float timer2;
    private Vector3 spawnPos;
    private GameObject clonedPopcorn;
    public int popcornSpawned;
    public GameObject popcornObj;
    public int quantitySpawned;
    public float spawnInterval;
    public float timeBetween;
    public float zSpawnLimit;
    public float xSpawnLimit;

    // Start is called before the first frame update
    void Start()
    {
        timer1 = 0;
        timer2 = 0;
        popcornSpawned = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance.isSpawnEnd == false)
        {
            StartTimer();
            // StartCoroutine(SpawnNewBatchOfPopcorns(spawnInterval));
        }
    }

    IEnumerator SpawnNewBatchOfPopcorns(float time)
    {
        yield return new WaitForSeconds(time);

        timer2 += Time.deltaTime;
        if (popcornSpawned < quantitySpawned)
        {
            if (timer2 >= timeBetween)
            {
                SpawnPopcorn(1);
                popcornSpawned++;
                timer2 = 0;
            }
        }
        else if (popcornSpawned >= quantitySpawned)
        {
			popcornSpawned = 0;
			timer2 = 0;
        }
    }
	
	void StartTimer2()
    {
        timer2 += Time.deltaTime;
        if (popcornSpawned < quantitySpawned)
        {
            if (timer2 >= timeBetween)
            {
                SpawnPopcorn(1);
                popcornSpawned++;
                timer2 = 0;
            }
        }
        else if (popcornSpawned >= quantitySpawned)
        {
			popcornSpawned = 0;
			timer2 = 0;
        }
    }

    void StartTimer()
    {
        timer1 += Time.deltaTime;
        if (timer1 >= spawnInterval)
        {
			SpawnPopcorn(5);
			timer1 = 0;
        }
    }

    void SpawnPopcorn(int popcornQuantity)
    {
        for (int i = 0 ; i <popcornQuantity ; i++)
        {
            RandomSpawnPos();
            clonedPopcorn = Instantiate(popcornObj, spawnPos, Quaternion.identity,this.transform);
        }
    }
    void RandomSpawnPos()
    {
        float randX = Random.Range(-xSpawnLimit, xSpawnLimit);
        float randZ = Random.Range(-zSpawnLimit, zSpawnLimit);

        spawnPos = new Vector3(randX, this.transform.position.y, randZ);
    }
}
