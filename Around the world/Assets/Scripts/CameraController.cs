using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour {

    public enum CameraState
    {
        followState,
        orbitState,
        freeState
    }

    public CameraState currentCameraState;
    public bool free;

    public Transform target;
    public float distanceDamp;
    public float rotationalDamp;

    private MainController mainController;
    // Use this for initialization
    void Start () {
        mainController = target.gameObject.GetComponent<MainController>();
	}

    private void Update()
    {
        if (free)
        {
            currentCameraState = CameraState.freeState;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                currentCameraState = CameraState.orbitState;
            }
            else
            {
                currentCameraState = CameraState.followState;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
		
        if(currentCameraState == CameraState.followState)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, distanceDamp * Time.deltaTime);
            //transform.Rotate(Vector3.Slerp(transform.forward, target.forward, 0.5f));
            //transform.LookAt(target.parent.transform);

            Quaternion toRot = Quaternion.LookRotation(target.parent.transform.position - transform.position, target.up);
            Quaternion curRot = Quaternion.Slerp(transform.rotation, toRot, rotationalDamp * Time.deltaTime);
            transform.rotation = toRot;
        }
        if (currentCameraState == CameraState.orbitState)
        {
            //gameObject.GetComponent<MouseOrbit2>();
        }
        if (currentCameraState == CameraState.freeState)
        {
            Quaternion toRot = Quaternion.LookRotation(target.parent.transform.position - transform.position, target.up);
            Quaternion curRot = Quaternion.Slerp(transform.rotation, toRot, rotationalDamp * Time.deltaTime);
            transform.rotation = toRot;
        }

    }

    public void CameraFreeEvent()
    {
        StartCoroutine(FreeCameraCoroutine());
        //free = true;
    }

    public IEnumerator FreeCameraCoroutine()
    {
        DOTween.To(() => distanceDamp, x => distanceDamp = x, 0.2f, 2f);
        yield return new WaitForSeconds(2f);
        free = true;
        mainController.ui.EnableResetPanel();
    }
}
