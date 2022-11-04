using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [Header("Player Punch var")]
    public Camera cam;
    public float givedamageof = 10f;
    public float punchingRange = 5f;

    [Header("Punch Effects")]
    public GameObject Woodedeffect;
    public GameObject Goreeffect;



    public void Punch()
    {
        RaycastHit hitinfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitinfo, punchingRange))
        {

            Debug.Log(hitinfo.transform.name);

            ObjectToHit objectToHit = hitinfo.transform.GetComponent<ObjectToHit>();
            Zombie1 zombie1 = hitinfo.transform.GetComponent<Zombie1>();

            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(givedamageof);
                GameObject impactGo = Instantiate(Woodedeffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(impactGo, 1f);

            }

            else if (zombie1 != null)
            {
                zombie1.Zombiehitdamage(givedamageof);
                GameObject Goreeffect_ = Instantiate(Goreeffect, hitinfo.point, Quaternion.LookRotation(hitinfo.normal));
                Destroy(Goreeffect_, 1f);
            }



        }
    }
}