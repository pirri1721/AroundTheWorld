using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public float min;
    public float max;
    public float angularV;

	// Use this for initialization
	void Start () {

        angularV = Random.Range(min, max);

        float distanceFactor = Vector3.Distance(transform.parent.position, transform.GetChild(0).transform.position)/ 100f;
        angularV = angularV / distanceFactor;
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(transform.up, angularV * Time.deltaTime);
	}
}
