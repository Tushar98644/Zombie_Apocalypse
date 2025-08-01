using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_script : MonoBehaviour
{
    [Header("Player Movement")]
    public float PlayerSpeed = 1.1f;
    public float Playersprint = 3f;

    [Header("Player Script")]
    public Transform PlayerCamera;




    [Header("Player controller,Animator and Gravity")]
    public CharacterController cC;
    // charactercontroller is used for movement consstrained by collisons without rigidbody physics  
    public float gravity = -9.8f;
    public Animator animator;

    [Header("Player Jumping and velocity")]
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    public float JumpRange = 1f;
    Vector3 velocity;
    public Transform SurfaceCheck;
    bool onsurface;
    public float SurfaceDistance = 0.4f;
    public LayerMask surfacemask;

    //private void Start()
    ////called once if a script is enabled --when the first frame is loaded
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //}





    public void PlayerMove()
    {

        float horizontal_axis = Input.GetAxisRaw("Horizontal");

        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {


            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            //animator.SetBool("Running", false);
            //animator.SetBool("RifleWalk", false);
            //animator.SetBool("IdleAim", false);




            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg+PlayerCamera.eulerAngles.y;
            //targetangle gives the angle by which to rotate,atan2 returns the run angle...


            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);


            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //quaternian.euler will basically return the rotation that would rotate x degrees around the 



            Vector3 movedierction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            cC.Move(movedierction.normalized * PlayerSpeed * Time.deltaTime);

            JumpRange = 2f;



        }
        else
        {

             animator.SetBool("Idle", true);
             animator.SetBool("Walk", false);
            animator.SetBool("Running", false);

            JumpRange = 1f;


        }





    }






    //  



    //


    private void Update()
        ////called every frame-depends on the time taken by the frame to process-used in moving objects
        ////fixedupdate is consistent and occurs after a fixed time  

    {

        onsurface = Physics.CheckSphere(SurfaceCheck.position, SurfaceDistance, surfacemask);
        if (onsurface && velocity.y < 0)
        {
            velocity.y = -2f;



        }

        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);


        //    //Time.deltaTime is the reciprocal of the framerate and ensures that the game runs at equal units/Sec on every device





        PlayerMove();
        Jump();
        Sprint();

    }



      public void Jump()
      {
         if (Input.GetButtonDown("Jump") && onsurface)
         {

            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");

            velocity.y = Mathf.Sqrt(JumpRange * -2 * gravity);
            

        }
        else

        {
            animator.SetBool("Idle", true);
            animator.ResetTrigger("Jump");
          

        }

    }

    public void Sprint()
    {

        if (Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onsurface)
        {
            float horizontal_axis = Input.GetAxisRaw("Horizontal");

            float vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Running", true);
                JumpRange = 0f;


                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + PlayerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);


                Vector3 moveDierction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                cC.Move(moveDierction.normalized * Playersprint * Time.deltaTime);




            }

            else
            {

                animator.SetBool("Walk", true);
                animator.SetBool("Running", false);
                JumpRange = 1f;
            }




        }
    }

}

