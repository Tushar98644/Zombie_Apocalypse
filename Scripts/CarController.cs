using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("wheels colliders")]
    public WheelCollider FLC;
    public WheelCollider FRC;
    public WheelCollider BLC;
    public WheelCollider BRC;

    [Header("wheels Transform")]
    public Transform FLTransform;
    public Transform FRTransform;
    public Transform BLTransform;
    public Transform BRTransform;
    public Transform Vehicledoor;

    [Header("Vehicle Engine")]
    public float accelerationforce = 100f;
    public float presentacceleration = 0f;
    public float breakingforce = 400f;
    public float Presentbreakforce = 0f;

    [Header("Vehicle steering")]
    public float wheelstorque = 20f;
    public float presentTurnAngle = 0f;


    [Header("Vehicle security")]
    public PlayerScript player;
    private float radius = 5f;
    private bool isopened = false;



    [Header("Disable Things")]
    public GameObject Aimcam;
    public GameObject Aimcanvas;
    public GameObject ThirdPersonCam;
    public GameObject Thirdpersoncanvas;
    public GameObject PlayerCharacter;


    void MoveVehicle()
    {
        FLC.motorTorque = presentacceleration;
        FRC.motorTorque = presentacceleration;
        BLC.motorTorque = presentacceleration;
        BRC.motorTorque = presentacceleration;

        presentacceleration = accelerationforce * -Input.GetAxis("Vertical");

        Steeringwheels(FRC, FRTransform);
        Steeringwheels(FLC, FLTransform);
        Steeringwheels(BRC, BRTransform);
        Steeringwheels(BLC, BLTransform);

    }

    void Steeringwheels(WheelCollider WC, Transform WT)
    {
        Vector3 position;
        Quaternion rotation;


        WC.GetWorldPose(out position, out rotation);

        WT.position = position;
        WT.rotation = rotation;

    }





    void VehicleSteering()
    {
        presentTurnAngle = wheelstorque * Input.GetAxis("Horizontal");
        FRC.steerAngle = presentTurnAngle;
        FLC.steerAngle = presentTurnAngle;
        //BRC.steerAngle = presentTurnAngle;
        //BLC.steerAngle = presentTurnAngle;


    }




    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {

            if (Input.GetKeyDown(KeyCode.F))
            {
                isopened = true;
                radius = 5000f;

                


            }

            else if (Input.GetKeyDown(KeyCode.G))
            {
                player.transform.position = Vehicledoor.transform.position;
                isopened = false;
                radius = 5f;

            }


        }


        if (isopened == true)
        {
            ThirdPersonCam.SetActive(false);
            Thirdpersoncanvas.SetActive(false);
            Aimcam.SetActive(false);
            Aimcanvas.SetActive(false);
            PlayerCharacter.SetActive(false);

            MoveVehicle();
            VehicleSteering();
            Applybrakes();
            

        }


        else if (isopened == false)
        {

            ThirdPersonCam.SetActive(true);
            Thirdpersoncanvas.SetActive(true);
            Aimcam.SetActive(true);
            Aimcanvas.SetActive(true);
            PlayerCharacter.SetActive(true);
        }






    }



   
    void Applybrakes()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Presentbreakforce = breakingforce;
        }

        else
        {

            Presentbreakforce = 0f;
        }

        FLC.brakeTorque = Presentbreakforce;
        FRC.brakeTorque = Presentbreakforce;
        BLC.brakeTorque = Presentbreakforce;
        BRC.brakeTorque = Presentbreakforce;


    }


}
