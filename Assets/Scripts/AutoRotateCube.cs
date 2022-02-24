using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotateCube : MonoBehaviour
{
    public float rotationSpeed = 0;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed);
    }
}