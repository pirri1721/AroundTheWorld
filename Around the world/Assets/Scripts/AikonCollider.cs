using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AikonCollider : MonoBehaviour {

    public CameraController cc;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("From Aikon Collider");
        Debug.Log("ForceSum " + collision.impactForceSum);
        Debug.Log("Impulse " + collision.impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if(other.name == "MainChar")
        {
            Debug.Log("shooting stars event");
            cc.CameraFreeEvent();
        }
    }
}
