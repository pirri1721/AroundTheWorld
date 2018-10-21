using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour {

    public float mass = 1f; // kg

    /// <summary>
    /// Velocity vecotr [m s^-1]
    /// </summary>
    public Vector3 velocityVector; //average velocity
    private Vector3 deltaS; //displacement

    public Vector3 netForce; // N [kg m s^-2]
    public List<Vector3> forceVectorList = new List<Vector3>();

    public bool Gravity;


    private PhysicsEngine[] physicsEnginesArray;

    private const float bigG = 6.673e-11f;



	// Use this for initialization
	void Start () {
        physicsEnginesArray = GameObject.FindObjectsOfType<PhysicsEngine>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        
        if (Gravity)
        {
            SumForces();
            CalculateGravity();
            UpdatePosition();
        }
        

        //transform.position += velocityVector * Time.deltaTime;

        /*if (netForce == Vector3.zero)
        {
            transform.position += v * Time.deltaTime;
        }
        else
        {
            Debug.Log("Unbalanced force detected");
        }
        */
        
    }

    public void CalculateGravity()
    {
        foreach( PhysicsEngine peA in physicsEnginesArray)
        {
            foreach (PhysicsEngine peB in physicsEnginesArray)
            {
                if(peA != peB && peA != this)
                {
                    //Debug.Log("Calculating gravitational force exerted on " + peA.name + " due to the gravity of " + peB.name);

                    Vector3 offset = peA.transform.position - peB.transform.position;
                    float squaredD = Mathf.Pow(offset.magnitude, 2f);

                    //FORCE
                    float gravityMagnitude = bigG * peA.mass * peB.mass / squaredD;
                    Vector3 gravityFeltVector = gravityMagnitude * offset.normalized;

                    peA.AddForce(-gravityFeltVector);
                }
               
            }
        }
    }

    public void SumForces()
    {
        netForce = Vector3.zero;

        foreach(Vector3 forceVector in forceVectorList)
        {
            netForce = netForce + forceVector;
            Debug.DrawLine(transform.position,netForce, Color.black);
        }

        forceVectorList.Clear();
    }

    public void UpdatePosition()
    {
        Vector3 accelerationvector = netForce / mass;
        velocityVector += accelerationvector * Time.deltaTime;
        transform.position += velocityVector * Time.deltaTime;
    }

    public void AddForce(Vector3 forceVector)
    {
        forceVectorList.Add(forceVector);
    }
}
