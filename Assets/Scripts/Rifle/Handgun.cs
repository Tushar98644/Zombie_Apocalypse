using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : MonoBehaviour
{

    [Header("Rifle Attributes")]
    public Camera cam;
    public float giveDamageof = 10f;
    public float ShootingRange = 100f;
    public float firecharge = 10f;
    private float nextTimetoshoot = 0f;


    [Header("Rifle Ammunition and shooting")]
    private int maximumAmmunition = 25;
    public int mag = 10;
    public float reloadingTime = 4.3f;
    private int presentAmmunition;
    private bool setReloading = false;


    [Header("Rifle Effects")]
    public ParticleSystem muzzlespark;
    public GameObject Woodeffect1;

    public void Shoot()
    {

        muzzlespark.Play();

        RaycastHit hitinfo;
        //returns the info of the structure hit by raycast

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitinfo, ShootingRange))
        {

            Debug.Log(hitinfo.transform.name);

            Object objectTohit = hitinfo.transform.GetComponent<Object>();

            if (objectTohit != null)
            {
                objectTohit.ObjectHitdamage(giveDamageof);
                GameObject impactgo = Instantiate(Woodeffect1, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(impactgo, 1f);

            }


        }
    }


    private void Update()

    {

            if (Input.GetButton("Fire1") && Time.time>=nextTimetoshoot)
            {
            nextTimetoshoot = Time.time + 1f / firecharge;
                Shoot();

            }

    }
}

