using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objectPrefab;
    private Vector3 spawnPosObstacle = new Vector3(25, 0, 0);
    private Vector3 spawnPosFood = new Vector3(25, 5, 0);
    private float startDelay = 2;
    private float repeatRate;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawning()
    {
        //InvokeRepeating("SpawnObstacle", startDelay, Random.Range(2, 5));
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        while (playerControllerScript.isGameActive)
        {
            repeatRate = Random.Range(1.5f, 5);
            yield return new WaitForSeconds(repeatRate);
            if (!playerControllerScript.gameOver && playerControllerScript.isGameActive)
            {
                Instantiate(objectPrefab[0], spawnPosObstacle, objectPrefab[0].transform.rotation);
                Instantiate(objectPrefab[1], spawnPosFood, objectPrefab[1].transform.rotation);
            }
        }
    }

    void SpawnFood()
    {

    }
}
