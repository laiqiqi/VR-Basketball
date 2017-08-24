﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    public Transform ballPortal;
    public GameObject endCanvas;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void BallReset()
    {
        transform.position = ballPortal.position;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Null Space"))
        {
            BallReset();
        }

        if (other.gameObject.name.Equals("Board"))
        {
            other.gameObject.GetComponent<AudioSource>().Play();
        }
        else if (other.gameObject.name.Equals("Net"))
        {
            other.gameObject.GetComponent<AudioSource>().Play();
        }
        else if (other.gameObject.name.Equals("Rim"))
        {
            other.gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Restart"))
        {
            endCanvas.SetActive(false);
            Timer.timeLeft = 90f;
        }
        else if (other.gameObject.name.Equals("Exit"))
        {
            SteamVR_LoadLevel.Begin("Intro");
            ScoreHandler.scoreBlue = 0;
            ScoreHandler.scoreRed = 0;
        }
        else if (other.gameObject.name.Equals("Practice-Half"))
        {
            SteamVR_LoadLevel.Begin("Practice-Half");
            ScoreHandler.scoreBlue = 0;
            ScoreHandler.scoreRed = 0;
        }
        else if (other.gameObject.name.Equals("Practice-Full"))
        {
            SteamVR_LoadLevel.Begin("Practice-Full");
            ScoreHandler.scoreBlue = 0;
            ScoreHandler.scoreRed = 0;
        }
        else if (other.gameObject.name.Equals("RapidFire"))
        {
            SteamVR_LoadLevel.Begin("RapidFire");
            ScoreHandler.scoreBlue = 0;
            ScoreHandler.scoreRed = 0;
        }
    }
}
