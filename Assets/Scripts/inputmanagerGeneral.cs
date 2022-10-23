using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inputmanagerGeneral : MonoBehaviour
{
    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && FindObjectOfType<MouseLook>().MouseSens < 9000)
        {
            FindObjectOfType<MouseLook>().MouseSens+=10;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && FindObjectOfType<MouseLook>().MouseSens > 25)
        {
            FindObjectOfType<MouseLook>().MouseSens-=10;
        }
    }
}
