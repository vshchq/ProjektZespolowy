﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonLampCollider : MonoBehaviour
{
    public GameObject button;
    private bool buttonPressed = false;

    public MeshRenderer klatka;
    public Material klatka1;
    public Material klatka2;

    public Light lampLight;
    public GameObject[] miceEyes; 

    void Start()
    {
       miceEyes = GameObject.FindGameObjectsWithTag("MouseEyes");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "LampButton")
        {
            if (buttonPressed == false)
            {
                button.transform.position += new Vector3(0, -0.5f, 0);
                klatka.material = klatka2;
                lampLight.enabled = !lampLight.enabled;

                foreach(GameObject i in miceEyes)
                {
                    i.GetComponent<Light>().enabled = true;
                }

                buttonPressed = true;
            }
            else if (buttonPressed == true)
            {
                button.transform.position += new Vector3(0, +0.5f, 0);
                klatka.material = klatka1;
                lampLight.enabled = !lampLight.enabled;

                foreach (GameObject i in miceEyes)
                {
                    i.GetComponent<Light>().enabled = false;
                }

                buttonPressed = false;
            }
        }
    }

}
