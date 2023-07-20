using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundRayCast : MonoBehaviour
{


    public bool isThereGround()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, 20f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
