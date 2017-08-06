﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{

    #region SteamVR variables
		public SteamVR_TrackedObject trackedObject;
		public SteamVR_Controller.Device device;
    #endregion

    #region Teleportation variables	
		private LineRenderer laser;
		public GameObject teleportAimer;
		public Vector3 teleportLocation;
		public GameObject player;
		public LayerMask laserMask;
		public float yNudge = 1f;
		public bool isLeftHanded;
		public float throwForce = 1.5f;
	#endregion
    // Use this for initialization
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);
        if (isLeftHanded)
        {
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                laser.gameObject.SetActive(true);
                teleportAimer.SetActive(true);

                laser.SetPosition(0, gameObject.transform.position);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 10, laserMask))
                {
                    teleportLocation = hit.point;
                    laser.SetPosition(1, teleportLocation);
                    teleportAimer.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudge, teleportLocation.z);
                }
            }

			if(device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
			{
				laser.gameObject.SetActive(false);
				teleportAimer.SetActive(false);
				player.transform.position=teleportLocation;
			}
        }
        else
        {

        }
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(other);
            }
            else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                ThrowObject(other);
            }
        }
    }

    void ThrowObject(Collider col)
    {
        col.transform.SetParent(null);
        Rigidbody rb = col.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = device.velocity * throwForce;
        rb.angularVelocity = device.angularVelocity;
        Debug.Log("Released the object");
    }

    void GrabObject(Collider col)
    {
        col.transform.SetParent(gameObject.transform);
        col.GetComponent<Rigidbody>().isKinematic = true;
        device.TriggerHapticPulse(2000);
        Debug.Log("Grabbing Ball");
    }
}