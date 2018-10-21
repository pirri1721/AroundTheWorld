using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngine : MonoBehaviour {

    public float fuelMass;
    public float maxThrust; // N [kg m s^-2]

    [Range (0,1f)]
    public float thrustPercent; // [none]
    public Vector3 thrustUnitVector; // [none]

    public float effectiveExhaustVelocity;

    private PhysicsEngine physicsEngine;
    private float currentThrust; //N


    private MainController mainController;

    public void GetMainController(MainController mainController)
    {
        this.mainController = mainController;
    }

    // Use this for initialization
    void Start () {
        physicsEngine = GetComponent<PhysicsEngine>();
        physicsEngine.mass += fuelMass;
	}
	
	// Update is called once per frame
	void Update () {
        if(fuelMass > FuelThisUpdate())
        {
            fuelMass -= FuelThisUpdate();
            physicsEngine.mass -= FuelThisUpdate();
            UpdateFuelUI();
            ExtertForce();
        }
        else
        {
            Debug.Log("Out of fuel");
        }


//        physicsEngine.AddForce(thrustUnitVector);	
	}

    private void UpdateFuelUI()
    {
        mainController.UpdateFuel();
    }

    private void ExtertForce()
    {
        currentThrust = thrustPercent * maxThrust;
        //Vector3 thrustVector = Vector3.Normalize(thrustUnitVector) * currentThrust;
        Vector3 thrustVector = Vector3.Normalize(this.transform.forward) * currentThrust;
        physicsEngine.AddForce(thrustVector);
    }

    private float FuelThisUpdate()
    {
        float exhaustMassFlow = 0f;          //
        //float effectiveExhaustVelocity; //

        //effectiveExhaustVelocity = 4462f;


        exhaustMassFlow = currentThrust / effectiveExhaustVelocity;

        return exhaustMassFlow * Time.deltaTime; // [kg]
    }
}
