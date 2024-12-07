using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform cameraPos;

    void FixedUpdate()
    {
        if (cameraPos != null)
        {
            transform.position = cameraPos.position;
        }
        else
        {
            Debug.LogWarning("cameraPos reference is missing in CameraMove script.");
        }
    }
}
