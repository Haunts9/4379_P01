using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] GameObject Target;
    void Update()
    {
        if (Target != null)
        {
            transform.LookAt(Target.transform.position, Vector3.up);
            transform.Rotate(0, 180, 0);
        }

    }
}
