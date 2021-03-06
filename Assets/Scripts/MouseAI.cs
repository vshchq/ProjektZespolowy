﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MouseAI : MonoBehaviour
{
    private int i = 0;
    private Vector3[] positions = new Vector3[5];
    private Vector3 newPos;
    private Vector3 startPos;

    public Camera camera;
    private bool inMovement = false;
    public GameObject ksiazka;
    private Rigidbody bookRb;

    float t = 0f;
    private float timeToDoIt;

    public VideoPlayer ratIsDead;
    public GameObject player;

    public GameObject cat;

    private Vector3 rot;

    private Transform mouseModel;
    private Animator anim;

    public GameObject postp;

    void Start()
    {
        positions[0] = new Vector3(-54f, 0, 0);
        positions[1] = new Vector3(0, 0, 20f);
        positions[2] = new Vector3(-7f, 0, 0);
        positions[3] = new Vector3(-82f, 0, 0);
        positions[4] = new Vector3(-3f, 0, 20f);


        // GetComponent<Rigidbody>().detectCollisions = false;
        GetComponent<Rigidbody>().isKinematic = true;
        bookRb = ksiazka.GetComponent<Rigidbody>();
        mouseModel = transform.Find("mouse_idle");
        anim = mouseModel.GetComponent<Animator>();
        postp = GameObject.Find("PostProcessingManager");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine("Movement");
        }
    }

    public IEnumerator Movement()
    {   
        if(!inMovement)
        {
            inMovement = true;



            switch (i)
            {
                case 0:
                    newPos = transform.position + positions[i];
                    startPos = transform.position;

                    timeToDoIt = 2f;
                    t = 0f;
                    while (t < timeToDoIt)
                    {
                        anim.SetBool("walking", true);
                        t += Time.deltaTime;
                        transform.position = Vector3.Lerp(startPos, newPos, (t / timeToDoIt));

                        yield return null;
                    }
                    anim.SetBool("walking", false);

                    i = 1;
                    break;


                case 1:
                    newPos = transform.position + positions[i];
                    startPos = transform.position;
                    timeToDoIt = 0.5f;
                    //do dziury
                    rot.y = 90;
                    transform.eulerAngles = rot;
                    t = 0f;
                    while (t < timeToDoIt)
                    {
                        anim.SetBool("walking", true);
                        t += Time.deltaTime;
                        transform.position = Vector3.Lerp(startPos, newPos, (t / timeToDoIt));

                        yield return null;
                    }
                    anim.SetBool("walking", false);

                    transform.position = new Vector3(-93f, 24f, 7f);//teleportacja

                    //manipulacja kamerom
                    timeToDoIt = 3;
                    camera.GetComponent<CameraController>().enabled = false;

                    Vector3 oldCameraPos = camera.transform.position;
                    Vector3 newCameraPos = camera.transform.position + new Vector3(0, 0, -50f);
                    t = 0;
                    while (t < timeToDoIt)
                    {
                        anim.SetBool("walking", true);
                        t += Time.deltaTime;
                        camera.transform.position = Vector3.Lerp(oldCameraPos, newCameraPos, (t / timeToDoIt) * 0.2f);
                    }
                    anim.SetBool("walking", false);

                    timeToDoIt = 0.5f;
                    newPos = transform.position + new Vector3(0, 0, -10f);
                    startPos = transform.position;
                    //wyjscie z dziury
                    rot.y = -90;
                    transform.eulerAngles = rot;
                    t = 0f;
                    while (t < timeToDoIt)
                    {
                        anim.SetBool("walking", true);
                        t += Time.deltaTime;
                        transform.position = Vector3.Lerp(startPos, newPos, (t / timeToDoIt));

                        yield return null;
                    }
                    anim.SetBool("walking", false);

                    switchRigid();

                    timeToDoIt = 3f;
                    newPos = transform.position += new Vector3(4f, 0, 0);
                    //pchniecie ksiazki
                    rot.y = 180;
                    transform.eulerAngles = rot;
                    t = 0f;
                    while (t < timeToDoIt)
                    {
                        anim.SetBool("walking", true);
                        t += Time.deltaTime;
                        transform.position = Vector3.Lerp(transform.position, newPos, (t / timeToDoIt));

                        yield return null;
                    }
                    anim.SetBool("walking", false);

                    yield return new WaitForSeconds(2);

                    bookRb.mass = 100f;

                    switchRigid();

                    timeToDoIt = 0.5f;
                    newPos = new Vector3(-93f, 24f, 7f);
                    startPos = transform.position;
                    //powrot do dziury
                    t = 0f;
                    while (t < timeToDoIt)
                    {
                        anim.SetBool("walking", true);
                        t += Time.deltaTime;
                        transform.position = Vector3.Lerp(startPos, newPos, (t / timeToDoIt));

                        yield return null;
                    }
                    anim.SetBool("walking", false);


                    camera.GetComponent<CameraController>().enabled = true;

                    transform.position += new Vector3(0, -22f, 0);//teleportacja

                    newPos = transform.position + new Vector3(0, 0, -16f);
                    startPos = transform.position;
                    t = 0f;
                    //wyjscie z dziury 2
                    rot.y = -90;
                    transform.eulerAngles = rot;
                    while (t < timeToDoIt)
                    {
                        anim.SetBool("walking", true);
                        t += Time.deltaTime;
                        transform.position = Vector3.Lerp(startPos, newPos, (t / timeToDoIt));

                        yield return null;
                    }
                    anim.SetBool("walking", false);

                    i = 2;

                    break;

                case 2:
                    cat.SetActive(true);
                    timeToDoIt = 0.5f;
                    //spaście z pólki
                    rot.y = 0;
                    transform.eulerAngles = rot;
                    newPos = transform.position + positions[i];
                    startPos = transform.position;
                    t = 0f;
                    while (t < timeToDoIt)
                    {
                        anim.SetBool("walking", true);
                        t += Time.deltaTime;
                        transform.position = Vector3.Lerp(startPos, newPos, (t / timeToDoIt));

                        yield return null;
                    }
                    anim.SetBool("walking", false);
                    switchRigid();

                    i = 3;
                    break;

                case 3:
                    //wziuum dalejko na poczatek
                    yield return new WaitForSeconds(1f);
                    switchRigid();
                    newPos = transform.position + positions[i];
                    startPos = transform.position;
                    timeToDoIt = 5f;
                    t = 0f;
                    while (t < timeToDoIt)
                    {
                        anim.SetBool("walking", true);
                        t += Time.deltaTime;
                        transform.position = Vector3.Lerp(startPos, newPos, (t / timeToDoIt));

                        yield return null;
                    }
                    anim.SetBool("walking", false);
                    i = 4;
                    break;

                case 4:

                    newPos = transform.position + positions[i];
                    startPos = transform.position;
                    timeToDoIt = 1f;
                    //do dziury
                    rot.y = 90;
                    transform.eulerAngles = rot;
                    t = 0f;
                    while (t < timeToDoIt)
                    {
                        anim.SetBool("walking", true);
                        t += Time.deltaTime;
                        transform.position = Vector3.Lerp(startPos, newPos, (t / timeToDoIt));

                        yield return null;
                    }
                    anim.SetBool("walking", false);

                    transform.position += new Vector3(-5f, 10f, 0);//teleportacja

                    newPos = transform.position + new Vector3(0, 0, -14f);
                    startPos = transform.position;
                    t = 0f;
                    //wyjscie z dziury 2
                    rot.y = -90;
                    transform.eulerAngles = rot;
                    while (t < timeToDoIt)
                    {
                        anim.SetBool("walking", true);
                        t += Time.deltaTime;
                        transform.position = Vector3.Lerp(startPos, newPos, (t / timeToDoIt));

                        yield return null;
                    }
                    anim.SetBool("walking", false);

                    yield return new WaitForSeconds(2f);
                    newPos = player.transform.position;
                    postp.SetActive(false);
                    ratIsDead.enabled = true;
                    cat.SetActive(true);
                    yield return new WaitForSeconds(22f);
                    //mozna by tu wtawić black screen żeby nie pokazywało sceny
                    PlayerPrefs.DeleteAll();
                    SceneManager.LoadScene(2);

                    postp.SetActive(true);
                    ratIsDead.enabled = false;
                    player.transform.position = newPos;

                    break;

            }


            inMovement = false;

        }      

        
        yield return null;
    }

    private void switchRigid()
    {
        GetComponent<Rigidbody>().isKinematic = !GetComponent<Rigidbody>().isKinematic;
    }

}