using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    public GameObject mainCamera;
    public GameObject rearCamera;

    bool activeCamera = true;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera.SetActive(true);
        rearCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxisRaw("Camera"));
        if (Input.GetAxisRaw("Camera") != 0)
        {
            activeCamera = !activeCamera;
        }

        mainCamera.SetActive(activeCamera);
        rearCamera.SetActive(!activeCamera);
    }
}

