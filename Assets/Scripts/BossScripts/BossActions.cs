using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActions : MonoBehaviour
{
    [SerializeField] GameObject LerpABox;
    [SerializeField] GameObject LerpBBox;
    [SerializeField] float moveSpeed = 1f;
    Vector3 LerpA;
    Vector3 LerpB;
    bool GoToA = true;
    bool SpecializedMovement = false;

    [SerializeField] public GameObject[] Guns;
    GameObject CurrentGun;
    GameObject LastGun;
    [SerializeField] float GunRotationSpeed = 2f;
    bool BeamReady = false;
    bool CurrentlyGunRotating = false;
    // Start is called before the first frame update
    void Awake()
    {
        LastGun = Guns[0];
        if (LerpABox != null && LerpBBox != null)
        {
            LerpA = LerpABox.transform.position;
            LerpB = LerpBBox.transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Attack();
    }
    void Movement()
    {
        if (SpecializedMovement == false)
        {
            NormalMovement();
        }
        else
        {
            FollowPlayerMovement();
        }
    }

    void NormalMovement()
    {
        if (gameObject.transform.position == LerpA)
        {
            //Debug.Log("Reached A");
            GoToA = false;
        }
        if (gameObject.transform.position == LerpB)
        {
            //Debug.Log("Reached B");
            GoToA = true;
        }

        if (GoToA == false)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, LerpB, Time.deltaTime * moveSpeed);
        }
        if (GoToA == true)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, LerpA, Time.deltaTime * moveSpeed);
        }

    }
    void FollowPlayerMovement()
    {

    }
    void Attack()
    {
        if (BeamReady == false && CurrentlyGunRotating == false)
        {
            Debug.Log("Initialize Guns");
            StartCoroutine(GunRotation());
        }
        else
        {
            //BeamReady
        }
    }
    IEnumerator GunRotation()
    {
        Debug.Log("Rotating Gun");
        CurrentlyGunRotating = true;
        yield return new WaitForSeconds(GunRotationSpeed/2);
        //code goes here
        GunPick();
        GunPick();
        //end
        yield return new WaitForSeconds(GunRotationSpeed / 2);
        //Is Beam Ready? If not continue
        if (BeamReady == false)
        {
            StartCoroutine(GunRotation());
        }
        else
        {
            CurrentlyGunRotating = false;
        }
    }
    void GunPick()
    {
        int randomGun = Random.Range(0, Guns.Length);
        while (Guns[randomGun].name == LastGun.name)
        {
          randomGun = Random.Range(0, Guns.Length);
        }
        Debug.Log("Current Gun Selected: " + Guns[randomGun]);
        CurrentGun = Guns[randomGun];
        LastGun = CurrentGun;
        BaseEnemyTurret Trigger = CurrentGun.GetComponent<BaseEnemyTurret>();
        if (Trigger != null)
        {
            Trigger.Shoot();
        }
    }

}
