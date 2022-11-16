using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Speed settings")]
    [Tooltip("Speed contoral")] [SerializeField] float controlSpeed = 30f;
    [Tooltip("Sets range of movement on x axis")] [SerializeField] float xRange = 10f;
    [Tooltip("Sets range of movement on y axis")] [SerializeField] float yRange = 7f;

    [Header("Screen position based tuning")]
    
    [Tooltip("Sets pich factor on y axis")] [SerializeField] float positionPitchFactor = -2f;
    [Tooltip("Sets yaw factor on x axis")] [SerializeField] float PositionYawFactor = 2f;

    [Header("Player input based tuning")]
    [Tooltip("Sets temporary pich factor on y axis")] [SerializeField] float controlPitchFactor = -15f;
    [Tooltip("Sets temporary roll factor on x axis")] [SerializeField] float corolRollFactor = -20f;
    
    [Header("General laser settings")]
    [Tooltip("Adds partical componet to lasers")] [SerializeField] GameObject[] lasers;
    [Tooltip("Adds sound file to laserSound audioclip")] [SerializeField] AudioClip laserSound;
    
    float xThrow;
    float yThrow;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float yawDueToPosition = transform.localPosition.x * PositionYawFactor;
        float rollDueToControlThrow = xThrow * corolRollFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToPosition;
        float roll = rollDueToControlThrow;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xMove = xThrow * controlSpeed * Time.deltaTime;
        float yMove = yThrow * controlSpeed * Time.deltaTime;
        float xRawPos = transform.localPosition.x + xMove;
        float yRawPos = transform.localPosition.y + yMove;
        float clampxMove = Mathf.Clamp(xRawPos, -xRange, xRange);
        float clampyMove = Mathf.Clamp(yRawPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampxMove, clampyMove, transform.localPosition.z);
    }
    void ProcessFiring()
    {
       if(Input.GetButton("Fire1"))
       {
        SetLasersActive(true);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(laserSound);
        }
       }
       else
       {
        SetLasersActive(false);
        audioSource.Stop();
       }
    }
    void SetLasersActive(bool isActive)
    {
         foreach (GameObject laser in lasers)
        {
            var emissionMod = laser.GetComponent<ParticleSystem>().emission;
            emissionMod.enabled = isActive;
        }
    }
}
