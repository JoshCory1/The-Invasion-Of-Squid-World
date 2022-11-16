using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("General particle settings")]
    [Tooltip("Destroy VFX")] [SerializeField] GameObject deathVFX;
    [Tooltip("Damage VFX")] [SerializeField] GameObject hitVFX;
    [Header("Hitpoints and scorepoints")]
    [Tooltip("scorepoints")] [SerializeField] int scorePerHit = 15;
    [Tooltip("Hitpoints")] [SerializeField] int hitPoints = 4;
    ScoreBoard scoreBoard;
    GameObject parentGameObject;
    
    void Start()
    {
        AddRigidbody();
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");

    }

    private void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        processHit();
        
    }
    private void processHit()
    {
        takeDamage();
    }

    private void takeDamage()
    {
        hitPoints--;
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        if (hitPoints < 1)
        {
            scoreBoard.increaseScore(scorePerHit);
            processDestroy();
        }
    }

    private void processDestroy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
       vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }

    
}
