using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimcamera : MonoBehaviour
{
    [Header("Camera Assign")]
    public GameObject Aimcam;
    public GameObject Aimcanvas;
    public GameObject Thirdpersoncam;
    public GameObject Thirdpersoncanvas;


    [Header("Camera Animator")]
    public Animator anim;

    void Update()
    {
        if(Input.GetKey("l") && Input.GetKey(KeyCode.UpArrow)|| Input.GetKey(KeyCode.W))
        {

            anim.SetBool("RifleWalk", true);
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim",true);
            anim.SetBool("Walk",true);


            Aimcam.SetActive(true);
            Aimcanvas.SetActive(true);
            Thirdpersoncam.SetActive(false);
            Thirdpersoncanvas.SetActive(false);
        }


        else if (Input.GetKey("l"))
        {
            anim.SetBool("RifleWalk",false);
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            anim.SetBool("Walk", false);

            Aimcam.SetActive(true);
            Aimcanvas.SetActive(true);
            Thirdpersoncam.SetActive(false);
            Thirdpersoncanvas.SetActive(false);
        }

        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("IdleAim", false);
            anim.SetBool("RifleWalk", false);

            Aimcam.SetActive(false);
            Aimcanvas.SetActive(false);
            Thirdpersoncam.SetActive(true);
            Thirdpersoncanvas.SetActive(true);

        }
    }
}
