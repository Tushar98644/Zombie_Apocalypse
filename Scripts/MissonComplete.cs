using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissonComplete : MonoBehaviour
{
    [Header("Objectives To Complete")]
    public Text objective1;
    public Text objective2;
    public Text objective3;
    public Text objective4;


    public static MissonComplete occurrence;


    public void Awake()
    {
        occurrence = this;
    }


    public void GetObjectiveDone(bool obj1,bool obj2,bool obj3,bool obj4)
    {
        obj1 = false;

        if(obj1==true)
        {
            objective1.text = "Completed";
            objective1.color = Color.green;
        }

        else
        {
            objective1.text = "1.Find the Rifle";
            objective1.color = Color.white;
        }
    }
    
}
