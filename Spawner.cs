using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour {

    public GameObject[] spawnLocations;
    public GameObject enemy;
    public GameObject enemyLocal;

    private Vector3 respawnLocation;

    //Upon startup
    void Awake()
    {
        //Place all Spawner Locations into Array
        spawnLocations = GameObject.FindGameObjectsWithTag("Spawner");
    }

    // Use this for initialization
    void Start()
    {
        enemy = (GameObject)Resources.Load("Allosaurus", typeof(GameObject));

        enemyLocal = GameObject.Find("Allosaurus");

        //Places the enemy into it's respective spawn point.
        respawnLocation = enemy.transform.position;
    }

    // Update is called once per frame.
    void Update()
    {
        if (!enemyLocal)
        {
            SpawnEnemy();
            
            //Quick fix, but could result in Spagetthi Code
            enemyLocal = GameObject.Find("Allosaurus(Clone)");
        }
    }

    private void SpawnEnemy()
    {
        int spawn = Random.Range(0, spawnLocations.Length);
        GameObject.Instantiate(enemy, spawnLocations[spawn].transform.position, Quaternion.identity);
    }
}