using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SpringGrabber : MonoBehaviour
{
    public SteamVR_Input_Sources hand;
    public string tagToGrab = "Pullable";

    private GameObject target = null;
    private SpringJoint joint;

    void Start()
    {
        joint = GetComponent<SpringJoint>();
    }

    void Update()
    {

        if (joint.connectedBody == null && target != null && SteamVR_Input.GetStateDown("GrabPinch", hand))
        {
            joint.connectedBody = target.GetComponent<Rigidbody>();
        }
        else if (joint.connectedBody != null && SteamVR_Input.GetStateUp("GrabPinch", hand))
        {
            joint.connectedBody = null;
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