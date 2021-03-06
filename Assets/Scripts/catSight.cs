﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class catSight : MonoBehaviour
{
    public SphereCollider col;
    public GameObject player;

    public VideoPlayer catDeath;
    public Light light;
    private Color defLightColor;

    public GameObject postp;

    public Checkpoint CH;

    void Start()
    {
        postp = GameObject.Find("PostProcessingManager");
        CH = FindObjectOfType<Checkpoint>();
        defLightColor = light.color;
    }


    void OnTriggerStay(Collider other)
    {

        if (other.gameObject == player)
        {
            Vector3 direction = other.transform.position - transform.position;

            RaycastHit hit;


            if (Physics.Raycast(transform.position, direction, out hit, col.radius))
            {
                if (hit.collider.gameObject == player && !this.GetComponent<CatBlink>().catBlinked)
                {
                    Debug.Log("Caught!");
                    StartCoroutine("RespawnCo", catDeath);

                }
                else
                {
              //      Debug.Log("Safe.");
                }
            }
        }
    }

    public IEnumerator RespawnCo(VideoPlayer vp)
    {
        light.color = Color.cyan;
        playerMovement.isInputEnabled = false;
        Fade.fadeIn();
        yield return new WaitForSeconds(1);
        if (PlayerPrefs.GetString("isSaved") == "true")
            CH.ReloadScene();
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Fade.fadeOut();

        catDeath.enabled = true;
        yield return new WaitForSeconds(6);

        

        playerMovement.isInputEnabled = true;
        light.color = defLightColor;
        catDeath.enabled = false;

    }

}
