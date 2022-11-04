using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Attributes")]
    public Camera cam;
    public float giveDamageof = 10f;
    public float ShootingRange = 100f;
    public float firecharge = 15f;
    private float nextTimetoshoot = 0f;
    public PlayerScript Player;
    public Transform Hand;
    public Animator anim;


    [Header("Rifle Ammunition and shooting")]
    private int maximumAmmunition = 32;
    public int mag=10;
    public float reloadingTime=1f;
    private int presentAmmunition;
    private bool setReloading = false;

    [Header("Ammo Sound and UI")]
    public GameObject AmmoOut;




    [Header("Rifle Effects")]
    public ParticleSystem muzzlespark;
    public GameObject Woodeffect;
    public GameObject goreeffect;


    private void Awake()
        //awake is called once and is used for initialization--before the game starts
    {

        transform.SetParent(Hand);
        presentAmmunition = maximumAmmunition;
    }

    public void Update()
    {
        if (setReloading)
            return;

        if (presentAmmunition<=0)
        {

            StartCoroutine(Reload());
            return;
        }



        if(Input.GetButton("Fire1") && Time.time>=nextTimetoshoot)
        {

            anim.SetBool("Fire", true);
            anim.SetBool("Idle", false);

            nextTimetoshoot = Time.time + 1f/ firecharge;
            Shoot();
        }

        else if(Input.GetButton("Fire1") && Input.GetKey(KeyCode.W )|| Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("Idle", false);

            anim.SetBool("FireWalk", true);
            
        }

        else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {

            anim.SetBool("FireWalk", true);
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            anim.SetBool("Relaoding", false);
            anim.SetBool("Walk",true);


        }

        else
        {
            anim.SetBool("FireWalk",false);
            anim.SetBool("Idle",true);
            anim.SetBool("Fire", false);


        }




    }


    public void Shoot()
    {

        if(mag==0)
        {
            return;
        }

        presentAmmunition--;

        if(presentAmmunition==0)
        {
            mag--;
        }

        muzzlespark.Play();

        RaycastHit hitinfo;

        if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hitinfo,ShootingRange))
        {

            Debug.Log(hitinfo.transform.name);

            ObjectToHit objectToHit = hitinfo.transform.GetComponent<ObjectToHit>();
           Zombie1 zombie1= hitinfo.transform.GetComponent<Zombie1>();

            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(giveDamageof);
                GameObject impactGo = Instantiate(Woodeffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(impactGo, 1f);

            }

            else if (zombie1 != null)
            {
                zombie1.Zombiehitdamage(giveDamageof);
                GameObject impactandGo = Instantiate(goreeffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(impactandGo, 1f);
            }


        }




    }
    IEnumerator Reload()
    {
        Player.PlayerSpeed = 0f;
        Player.Playersprint = 0f;
        setReloading = true;
        Debug.Log("Reloading...");
        anim.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadingTime);
        anim.SetBool("Reloading", false);

        presentAmmunition = maximumAmmunition;
        Player.PlayerSpeed = 1.9f;
        Player.Playersprint = 3f;
        setReloading = false;



    }

    IEnumerator ammoout()
    {
        AmmoOut.SetActive(true);
        yield return new WaitForSeconds(5f);
        AmmoOut.SetActive(false);
    }



}
