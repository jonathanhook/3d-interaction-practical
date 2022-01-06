using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Grabber : MonoBehaviour
{
    public SteamVR_Input_Sources hand;
    public string tagToGrab = "Grabbable";

    private GameObject grabbed = null;
    private GameObject target = null;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (grabbed == null && target != null && SteamVR_Input.GetStateDown("GrabPinch", hand))
        {
            Debug.Log(target);

            grabbed = target;
            grabbed.GetComponent<Rigidbody>().isKinematic = true;
            grabbed.transform.parent = transform;
        }
        else if (grabbed != null && SteamVR_Input.GetStateUp("GrabPinch", hand))
        {
            grabbed.transform.parent = null;

            Rigidbody grb = grabbed.GetComponent<Rigidbody>();
            grb.isKinematic = false;

            Hand h = GetComponent<Hand>();

            grb.velocity = h.trackedObject.GetVelocity();
            grb.angularVelocity = h.trackedObject.GetAngularVelocity();

            grabbed = null;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == tagToGrab && target == null)
        {
            target = c.gameObject;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject == target)
        {
            target = null;
        }
    }
}