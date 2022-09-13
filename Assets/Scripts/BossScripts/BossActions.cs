using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActions : MonoBehaviour
{
    #region Move Vars
    [SerializeField] GameObject LerpABox;
    [SerializeField] GameObject LerpBBox;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float beamMoveSpeed = 5f;
    [SerializeField] float beamMoveSpeedWhileFiring = .3f;
    GameObject player;
    Vector3 LerpA;
    Vector3 LerpB;
    float playerX;
    bool GoToA = true;
    bool SpecializedMovement = false;
    #endregion

    #region Gun Vars
    [SerializeField] public GameObject[] Guns;
    GameObject CurrentGun;
    GameObject LastGun;
    [SerializeField] float GunRotationSpeed = 2f;
    bool CurrentlyGunRotating = false;
    #endregion

    #region Beam Vars
    bool BeamCheck = false;
    bool BeamReady = false;
    bool BeamFiring = false;
    bool PhaseOneStart = false;
    [SerializeField] float BeamCoolDownTime = 10f;
    [SerializeField] float BeamFireTime = 3f;
    [SerializeField] GameObject BeamChargeGraphic;
    [SerializeField] GameObject BeamBox;
    [SerializeField] GameObject[] WhiteArt;
    [SerializeField] GameObject[] BlackArt;
    [SerializeField] GameObject[] OrangeArt;
    [SerializeField] AudioClip chargeSound;
    [SerializeField] AudioClip beamFireSound;
    private Color[] WsavedArt;
    private Color[] BsavedArt;
    private Color[] OsavedArt;
    #endregion

    void Awake()
    {
        //Art Init
        WsavedArt = new Color[WhiteArt.Length];
        int count = 0;
        foreach (GameObject f in WhiteArt)
        {
            WsavedArt[count] = f.GetComponent<Renderer>().material.color;
            count++;
        }
        BsavedArt = new Color[BlackArt.Length];
        count = 0;
        foreach (GameObject f in BlackArt)
        {
            BsavedArt[count] = f.GetComponent<Renderer>().material.color;
            count++;
        }
        OsavedArt = new Color[OrangeArt.Length];
        count = 0;
        foreach (GameObject f in OrangeArt)
        {
            OsavedArt[count] = f.GetComponent<Renderer>().material.color;
            count++;
        }
        //End Art Init
        player = GameObject.FindGameObjectWithTag("Player");
        LastGun = Guns[0];
        if (LerpABox != null && LerpBBox != null)
        {
            LerpA = LerpABox.transform.position;
            LerpB = LerpBBox.transform.position;
        }
    }

    void FixedUpdate()
    {
        if (BeamReady == false && BeamFiring == false)
        {
            StartCoroutine(BeamCooldown());
        }
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
        playerX = player.transform.position.x;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerX, transform.position.y, transform.position.z), Time.deltaTime * beamMoveSpeed);
    }
    void Attack()
    {
        if (BeamReady == false && CurrentlyGunRotating == false)
        {
            Debug.Log("Initialize Guns");
            StartCoroutine(GunRotation());
        }
        else if (BeamReady == true && CurrentlyGunRotating == false)
        {
            //BeamReady
            if (BeamCheck == false)
            {
                BeamCheck = true;
                prepareBeam();
            }

        }
    }
    #region Gun Scripts
    IEnumerator GunRotation()
    {
        Debug.Log("Rotating Gun");
        CurrentlyGunRotating = true;
        yield return new WaitForSeconds(GunRotationSpeed / 2);
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
    #endregion
    IEnumerator BeamCooldown()
    {
        BeamFiring = true;
        yield return new WaitForSeconds(BeamCoolDownTime);
        Debug.Log("Beam Ready");
        BeamReady = true;
    }
    void prepareBeam()
    {
        Debug.Log("Beam Beginning Fire");
        //Begin Tracking Player
        beamMoveSpeed = 10f;
        SpecializedMovement = true;
        //Activate Charge Graphics
        BeamChargeGraphic.SetActive(true);
        if(PhaseOneStart == false)
        {
            PhaseOneStart = true;
            StartCoroutine(BeamPhaseOne());
        }

    }
    //Charge Beam then
    //Stop Briefly Before Firing Beam
    IEnumerator BeamPhaseOne()
    {
        AudioHelper.PlayClip2D(chargeSound, 1f);
        yield return new WaitForSeconds(2f);
        beamMoveSpeed = 0f;
        yield return new WaitForSeconds(.5f);
        StartCoroutine(BeamPhaseTwo());

    }
    //Fire Beam
    IEnumerator BeamPhaseTwo()
    {
        AudioHelper.PlayClip2D(beamFireSound, 1f);
        BeamBox.SetActive(true);
        beamMoveSpeed = beamMoveSpeedWhileFiring;
        ApplyChangesToColor();
        yield return new WaitForSeconds(BeamFireTime);
        ceaseBeam();

    }
    //Reset Everything Beam Related
    void ceaseBeam()
    {
        BeamChargeGraphic.SetActive(false);
        BeamBox.SetActive(false);
        BeamCheck = false;
        SpecializedMovement = false;
        BeamReady = false;
        BeamFiring = false;
        PhaseOneStart = false;
        RevertChangesToColor();
    }
    public void ApplyChangesToColor()
    {

        Debug.Log("Color");
        int count = 0;
        foreach (GameObject f in WhiteArt)
        {
            f.GetComponent<Renderer>().material.color = Color.white;
            count++;
        }
        count = 0;
        foreach (GameObject f in BlackArt)
        {
            f.GetComponent<Renderer>().material.color = Color.black;
            count++;
        }
        count = 0;
        foreach (GameObject f in OrangeArt)
        {
            f.GetComponent<Renderer>().material.color = Color.HSVToRGB(.05f,1f,1f);
            count++;
        }
    }
    public void RevertChangesToColor()
    {

        Debug.Log("Reset");
        int count = 0;
        foreach (GameObject f in WhiteArt)
        {
            f.GetComponent<Renderer>().material.color = WsavedArt[count];
            count++;
        }
         count = 0;
        foreach (GameObject f in BlackArt)
        {
            f.GetComponent<Renderer>().material.color = BsavedArt[count];
            count++;
        }
         count = 0;
        foreach (GameObject f in OrangeArt)
        {
            f.GetComponent<Renderer>().material.color = OsavedArt[count];
            count++;
        }
    }
}
