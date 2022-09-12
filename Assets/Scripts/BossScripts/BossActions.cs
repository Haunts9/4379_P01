using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActions : MonoBehaviour
{
    [SerializeField] GameObject LerpABox;
    [SerializeField] GameObject LerpBBox;
    Vector3 LerpA;
    Vector3 LerpB;
    bool SpecializedMovement = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (LerpABox != null && LerpBBox != null)
        {
            LerpA = LerpABox.transform.position;
            LerpB = LerpBBox.transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(SpecializedMovement == false)
        {

        }
    }
    void NormalMovement()
    {

    }
    void FollowPlayerMovement()
    {

    }
}
