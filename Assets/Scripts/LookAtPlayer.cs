using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    GameObject Target;

    void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Target != null)
        {
            transform.LookAt(Target.transform.position, Vector3.up);
            //transform.Rotate(0, 180, 0);
        }

    }
}
