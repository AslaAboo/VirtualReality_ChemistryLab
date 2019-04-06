using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{

    public ParticleSystem system;
    public bool isOn;


    void FixedUpdate()
    {


        Debug.Log("isBurner");
        if (isOn == true)
        {
            system.Play();
        }
        else if (isOn == false)
        {
            system.Stop();
        }
    }
}