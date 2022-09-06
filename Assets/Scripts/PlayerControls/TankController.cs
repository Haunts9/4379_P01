using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] float _maxSpeed = 1000f;
    public float MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = value;
    }
    [SerializeField] float _turnSpeed = 2f;

    Rigidbody _rb = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveTank();
        //TurnTank();
    }

    public void MoveTank()
    {
        // calculate the move amount
        float YmoveAmountThisFrame = Input.GetAxis("Vertical") * _maxSpeed;
        float XmoveAmountThisFrame = Input.GetAxis("Horizontal") * _maxSpeed;
        // create a vector from amount and direction
        Vector3 YmoveOffset = transform.forward * YmoveAmountThisFrame;
        Vector3 XmoveOffset = transform.right * XmoveAmountThisFrame;
        // apply vector to the rigidbody
        _rb.AddForce(YmoveOffset + XmoveOffset);
        // technically adjusting vector is more accurate! (but more complex)
    }

    public void TurnTank()
    {
        // calculate the turn amount
        float turnAmountThisFrame = Input.GetAxis("Horizontal") * _turnSpeed;
        // create a Quaternion from amount and direction (x,y,z)
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // apply quaternion to the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }
}
