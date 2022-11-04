using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie1 : MonoBehaviour
{
    [Header("zombie Health")]
    public float giveDamage = 5f;
    private float presenthealth;
    private float zombiehealth = 100f;


    [Header("zombie things")]
    public LayerMask PlayerLayer;
    public Transform LookPoint;
    public NavMeshAgent ZombieAgent;
    public Transform playerBody;
    public Camera Attackingraycastarea;



    [Header("zombie things")]
    public Animator anim;
    


    [Header("zombie Gaurding var")]
    public GameObject[] Walkpoints;
    public float Zombiespeed;
    float Walkingpointradius = 2;
    int currentzombieposition = 0;


    [Header("zombie attacking var")]
    public float timebtwattack;
    bool previouslyattack;

    [Header("zombie mood/states")]
    public bool playerinvisionradius;
    public float visionradius;
    public float attackingradius;
    public bool playerinattackingradius;

    private void Awake()
    {
        ZombieAgent = GetComponent<NavMeshAgent>();
        presenthealth = zombiehealth;
    }

    private void Update()
    {
        playerinvisionradius = Physics.CheckSphere(transform.position, visionradius, PlayerLayer);
        playerinattackingradius = Physics.CheckSphere(transform.position, attackingradius, PlayerLayer);

        if(!playerinattackingradius && !playerinvisionradius) Gaurd();

        if (!playerinattackingradius && playerinvisionradius) PursuePlayer();
        if (playerinattackingradius && playerinvisionradius) AttackPlayer();




    }

    private void Gaurd()
    {

        if(Vector3.Distance(Walkpoints[currentzombieposition].transform.position,transform.position)<Walkingpointradius)
        {
            currentzombieposition = Random.Range(0, Walkpoints.Length);
            if (currentzombieposition >= Walkpoints.Length)
            {
                currentzombieposition = 0;
            }

           
        }
        transform.position = Vector3.MoveTowards(transform.position, Walkpoints[currentzombieposition].transform.position, Time.deltaTime * Zombiespeed);

        transform.LookAt(Walkpoints[currentzombieposition].transform.position);
    }

    private void PursuePlayer()
    {
        if (ZombieAgent.SetDestination(playerBody.position))
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Died", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("Running", true);
        }

        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Died", true);
            anim.SetBool("Attacking", false);
            anim.SetBool("Running", false);
        }


    }


    private void AttackPlayer()
    {

        ZombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);

        if(!previouslyattack)
        {
            RaycastHit hitinfo;


            if(Physics.Raycast(Attackingraycastarea.transform.position,Attackingraycastarea.transform.forward,out hitinfo,attackingradius))
            {
                Debug.Log("Attacking" + hitinfo.transform.name);
                PlayerScript playerbody = hitinfo.transform.GetComponent<PlayerScript>();

                if(playerbody!= null)
                {
                    playerbody.playerhitdamage(giveDamage);
                }


                anim.SetBool("Walking", false);
                anim.SetBool("Died", false);
                anim.SetBool("Attacking", true);
                anim.SetBool("Running", false);
            }

            previouslyattack = true;
            Invoke(nameof(ActiveAttacking), timebtwattack);
        }

    }

    private void ActiveAttacking()
    {
        previouslyattack = false;


    }

    public void Zombiehitdamage(float takendamage)
    {
        presenthealth -= takendamage;

        if(presenthealth<=0)
        {

            anim.SetBool("Walking", false);
            anim.SetBool("Died", true);
            anim.SetBool("Attacking", false);
            anim.SetBool("Running", false);
            zombiedie();
        }
    }

    private void zombiedie()
    {
        ZombieAgent.SetDestination(transform.position);
        Zombiespeed = 0f;
        attackingradius = 0f;
        visionradius = 0f;
        playerinattackingradius = false;
        playerinvisionradius = false;
        Object.Destroy(gameObject, 5f);
    }


}
