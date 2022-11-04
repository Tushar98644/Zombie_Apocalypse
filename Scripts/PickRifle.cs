using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRifle : MonoBehaviour
{
    [Header("Rifle")]
    public GameObject playerrifle;
    public GameObject PickupRifle;
    public PlayerPunch playerpunch;
   

    [Header("Rifle Assign Things")]
    public PlayerScript Player;
    public float radius=2.5f;
    private float nextTimetopunch=0f;
    public float punchingcharge = 15f;
    public Animator animator;



    private void Awake()
    {
        playerrifle.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1")&& Time.time>=nextTimetopunch)
        {

            animator.SetBool("Punch", true);
            animator.SetBool("Idle",false);


            nextTimetopunch = Time.time + 1f /punchingcharge;

            playerpunch.Punch();
        }

        else {

            animator.SetBool("Punch", false);
            animator.SetBool("Idle", true);

        }


        if(Vector3.Distance(transform.position,Player.transform.position)<radius)
        {


            if(Input.GetKeyDown("f"))
            {
                playerrifle.SetActive(true);
                PickupRifle.SetActive(false);


            }

           
        }
    }
}
