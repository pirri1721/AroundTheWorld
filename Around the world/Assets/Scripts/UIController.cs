using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Image RestartPanel;
    public Image MainPanel;
    public Image WastedPanel;

    public Slider ThrustSlider;
    public Slider FuelSlider;
    public Text fuelText;

    public StreamVideo streamVideo;
    // Use this for initialization
    void Start () {
		
	}

    public void ResetButton()
    {
        SceneManager.LoadScene(1);
    }

    public void EnableResetPanel()
    {
        RestartPanel.gameObject.active = true;
    }

    public void EnableWastedPanel()
    {
        WastedPanel.gameObject.active = true;
        if(streamVideo != null)
        {
            streamVideo.StartWastedCoroutine();
        }
        
    }

}
