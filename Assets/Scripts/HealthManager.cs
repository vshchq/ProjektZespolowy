﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public playerMovement thePlayer;

    public Renderer blob;

    public Material hurtMaterial;
    public Material neutralMaterial;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLenght;

    public static bool isHurt = false;

    private Checkpoint CH;

    // Start is called before the first frame update
    void Start()
    {
        CH = FindObjectOfType<Checkpoint>();
        if(PlayerPrefs.GetString("isSaved")=="true")
        CH.ReloadScene();
        currentHealth = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHurt)
        {
            blob.material = hurtMaterial;
        }
        else if (blob.material != neutralMaterial)
        {
            blob.material = neutralMaterial;
            isHurt = false;
        }

    }

    public void HurtPlayer(int damage, Vector3 knockbackDirection)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Respawn();
        }
        else
        {
            thePlayer.Knockback(knockbackDirection);

            isHurt = true;
        }
    }

    public void Respawn()
    {
        if (!isRespawning)
            StartCoroutine("RespawnCo");

    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        isHurt = true;
        playerMovement.isInputEnabled = false;
        Fade.fadeIn();
        
        yield return new WaitForSeconds(respawnLenght);

        isRespawning = false;

        if (PlayerPrefs.GetString("isSaved") == "true")
            CH.ReloadScene();
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        currentHealth = maxHealth;
        playerMovement.isInputEnabled = true;
        isHurt = false;
        Fade.fadeOut();

    }

    public void HealPlayer(int healAmmount)
    {
        currentHealth += healAmmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    public Vector3 GetSpawnPoint()
    {
        return respawnPoint;
    }

}
