using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class AlphaEnemy : MonoBehaviour {

    public GameObject enemy;
    //public NavMeshAgent agent;
    public LayerMask whatisEnemy, whatisSpikes, whatisGround;
    public NavMeshAgent agent;
    int alphaRoar = 0;

    public bool alphaCall;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    public float spikeRange, enemyRange;
    bool inSpikeRange, inEnemyRange;
    bool sawEnemyDie;

    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


	// Use this for initialization
	void Update () {
        //Check if Alpha is in range of the spike and if the character died.
        inSpikeRange = Physics.CheckSphere(transform.position, spikeRange, whatisSpikes);

        //Use if Enemies are going to be far from the Alpha
        //inEnemyRange = Physics.CheckSphere(transform.position, enemyRange, whatisEnemy);

        //inSpikeRange = Physics.CheckSphere(transform.position, spikeRange, whatisSpikes);
        if (inSpikeRange && !sawEnemyDie) AlphaPatrol();
        if (inSpikeRange && sawEnemyDie && alphaRoar < 1) AlphaCall();
    }

    private void AlphaPatrol()
    {
        //if (!walkPointSet) DistanceMeasure();

        /*if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint Reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = true;*/

        if (FindObjectOfType<EnemyPathing>().death == true)
        {
            sawEnemyDie = true;
        }
    }

    /* Later use if I want to work on it
    private void DistanceMeasure()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatisGround)) walkPointSet = true;
    }*/

    private void AlphaCall()
    { 
        FindObjectOfType<AudioHandler>().Play("AlphaRoar");

        //Only call
        alphaRoar++;
        alphaCall = true;
    }
}
