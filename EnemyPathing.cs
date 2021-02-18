using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyPathing : MonoBehaviour {

    public NavMeshAgent agent;
    public Transform alpha;
    public Transform player;
    public LayerMask whatisGround, whatisPlayer, whatisSpikes, whatisAlpha;

    [HideInInspector]
    public bool roar, alphaListen;

    [HideInInspector]
    public bool death;

    //Alpha Patrol
    public Vector3 walkPoint;
    //bool walkPointSet;
    public float walkPointRange;

    //States
    public float sightRange, attackRange, spikeRange, alphaRange;
    bool inSightRange, inAttackRange, isOnSpikes, inAlphaRange, onPlayer;

    private void Awake()
    {
        player = GameObject.Find("LoneSurv").transform;
        alpha = GameObject.Find("AlphaAllosaurus").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        //Check if player is in sight range and attack
        inSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        //Check if player is on spikes
        isOnSpikes = Physics.CheckSphere(transform.position, spikeRange, whatisSpikes);

        //Check if character is in Alpha Range
        inAlphaRange = Physics.CheckSphere(transform.position, alphaRange, whatisAlpha);

        onPlayer = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        //Bool check if the alpha has roared. Will do something before chasing the player.
        if (inAlphaRange && FindObjectOfType<AlphaEnemy>().alphaCall == true && !roar) Persuasion();

        //Chasing, attacking and dyinhg if statements.
        if (inSightRange && !inAttackRange) ChasePlayer();
        if (inSightRange && inAttackRange) AttackPlayer();
        if (isOnSpikes) DestroyEnemy();

        /*if (inAlphaRange)
        {
            AlphaCall();
        }*/
    }

    
    //Enemy AI % Chance to listen to Alpha Call
    private void Persuasion(){
        float listenNumber = Random.Range(0f, 6f);
        if (listenNumber < 5f)
        {
            transform.LookAt(player);
            FindObjectOfType<AudioHandler>().Play("Allosub");

            //Can speed up Agent. Possible change spike to be part of the navmesh. 1 step closer to finishing up this project
            GetComponent<NavMeshAgent>().speed = 15;
            GetComponent<NavMeshAgent>().acceleration = 4;
            alphaListen = true;
        }
        roar = true;
    }

    //Allo's Chasing Player
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    //Allo's Attacking Player
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (onPlayer)
        {
            SceneManager.LoadScene("Level001");
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject, 0f);
        Destroy(GameObject.FindGameObjectWithTag("Spike"), 0f);
        death = true;
        roar = false;
    }
}
