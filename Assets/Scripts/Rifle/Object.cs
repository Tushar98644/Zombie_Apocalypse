using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public float ObjectHealth = 120f;

    public void ObjectHitdamage(float amt)
    {
        ObjectHealth -= amt;

        if (ObjectHealth <= 0f)
        {

            Die();
        }

    }

    public void Die()
    {

        Destroy(gameObject);
    }

}
