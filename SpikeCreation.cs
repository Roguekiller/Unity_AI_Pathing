using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpikeCreation : MonoBehaviour {
    public GameObject spike;
    public Transform player;
    public GameObject spikeSpawningPlatform001;
    public GameObject spikeSpawningPlatform002;
    public NavMeshSurface[] surfaces;

    GameObject enemyRepath;

    public GameObject spikeLocal;
    //int spikeCap = 1;

    private Vector3 spikeSpawnLocations;

    public LayerMask whatisTrap, whatisTrap2;
    public float spikeRange001, spikeRange002;

    public bool detectedTrap;
    bool inSpikeRange001, inSpikeRange002;
	// Use this for initialization
	void Awake () {
        spikeSpawningPlatform001 = GameObject.Find("TrapArea001");
        spikeSpawningPlatform002 = GameObject.Find("TrapArea002");

	}

    void Start(){
        spike = (GameObject)Resources.Load("Spike", typeof(GameObject));
        enemyRepath = (GameObject)Resources.Load("EnemyRepath", typeof(GameObject));
        player = GameObject.Find("LoneSurv").transform;
        //spikeLocal = GameObject.Find("Spike");

        spikeSpawnLocations = spike.transform.position;

    }

    // Update is called once per frame
    void Update(){
        inSpikeRange001 = Physics.CheckSphere(transform.position, spikeRange001, whatisTrap);
        inSpikeRange002 = Physics.CheckSphere(transform.position, spikeRange002, whatisTrap2);

        //if (inSpikeRange && spikeLocal && spikeCap == 1)
        if (inSpikeRange001 && !spikeLocal){
            PlaceSpikeLeft();
            spikeLocal = GameObject.Find("Spike(Clone)");
        }

        if (inSpikeRange002 && !spikeLocal){
            PlaceSpikeRight();
            spikeLocal = GameObject.Find("Spike(Clone)");
        }
    }

    private void PlaceSpikeLeft() {
        GameObject.Instantiate(spike, spikeSpawningPlatform001.transform.position, Quaternion.identity);
        if (FindObjectOfType<EnemyPathing>().alphaListen && !detectedTrap)
        {
            GameObject.Instantiate(enemyRepath, spikeSpawningPlatform001.transform.position, Quaternion.identity);
            for(int i = 0; i < surfaces.Length; i++)
            {
                surfaces[i].BuildNavMesh();
            }
            detectedTrap = true;
        }
    }

    private void PlaceSpikeRight(){
        GameObject.Instantiate(spike, spikeSpawningPlatform002.transform.position, Quaternion.identity);
        if (FindObjectOfType<EnemyPathing>().alphaListen && !detectedTrap)
        {
            GameObject.Instantiate(enemyRepath, spikeSpawningPlatform002.transform.position, Quaternion.identity);
            detectedTrap = true;
            for (int i = 0; i < surfaces.Length; i++)
            {
                surfaces[i].BuildNavMesh();
            }
        }
    }
}


