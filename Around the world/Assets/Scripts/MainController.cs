using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainController : MonoBehaviour {

    public float angularV;
    private Rigidbody rigidbody;
    private RocketEngine rE;
    public  UIController ui;
    public float deathDamage;

    // Use this for initialization
    void Start () {
        ui = GameObject.Find("Canvas").gameObject.GetComponent<UIController>();

        rigidbody = GetComponent<Rigidbody>();
        rE = GetComponent<RocketEngine>();
        rE.GetMainController(this);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddTorque(-transform.right * angularV * Time.deltaTime);
            //transform.Rotate(transform.right, -angularV * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddTorque(transform.right * angularV * Time.deltaTime);
            //transform.Rotate(transform.right, angularV * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddTorque(-transform.up * angularV * Time.deltaTime);
            //transform.Rotate(transform.up, -angularV * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddTorque(transform.up * angularV * Time.deltaTime);
            //transform.Rotate(transform.up, angularV * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(rE.thrustPercent < 1f)
            {
                rE.thrustPercent += 0.7f * Time.deltaTime;
            }else
            {
                rE.thrustPercent = 1f;
            }

            ui.ThrustSlider.value = rE.thrustPercent;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (rE.thrustPercent > 0f)
            {
                rE.thrustPercent -= 0.7f * Time.deltaTime;
            }
            else
            {
                rE.thrustPercent = 0f;
            }

            ui.ThrustSlider.value = rE.thrustPercent;
        }
    }

    public void UpdateFuel()
    {
        ui.FuelSlider.value = rE.fuelMass/10f;
        ui.fuelText.text = rE.fuelMass.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("From main controller collider");

        Debug.Log("Impulse "+collision.impulse);
        Debug.Log("Impulse normalized" + collision.impulse.normalized);
        Debug.Log("Impulse magnitude" + collision.impulse.magnitude);

        if(collision.impulse.magnitude > deathDamage)
        {
            //WASTED

            StartCoroutine(WastedCoroutine());
        }
    }

    public IEnumerator WastedCoroutine()
    {
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0.2f, 0.05f);
        ui.EnableWastedPanel();
        yield return new WaitForSeconds(2f);

        ui.EnableResetPanel();
    }

    ///////////////
    public void UpdateThrustPercentFromUI()
    {
        rE.thrustPercent = ui.ThrustSlider.value;
    }
}
