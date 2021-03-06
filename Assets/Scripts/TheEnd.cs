﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TheEnd : MonoBehaviour
{

    public VideoPlayer theEnd;
    public GameObject postp;


    // Start is called before the first frame update
    void Start()
    {
        postp = GameObject.Find("PostProcessingManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("Cutscene");
            PlayerPrefs.DeleteAll();
        }

    }

    public IEnumerator Cutscene()
    {
        playerMovement.isInputEnabled = false;
        yield return new WaitForSeconds(2f);
        Fade.fadeIn();
        yield return new WaitForSeconds(2f);
        FindObjectOfType<AudioManager>().mute();
        Fade.fadeOut();
        theEnd.enabled = true;
        postp.SetActive(false);
        yield return new WaitForSeconds(35f);
        theEnd.enabled = false;
        postp.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);

    }

}
