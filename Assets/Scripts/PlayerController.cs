using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    [SerializeField] TextMeshProUGUI speedometer;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float turnSpeed = 25;
    [SerializeField] float horsePowers;
    [SerializeField] float rpm;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] List<WheelCollider> wheels;
    float speed;
    float horizontalInput;
    float verticalInput;
    int wheelIsOnGround;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (IsOnGround())
        {
            playerRb.AddRelativeForce(Vector3.forward * verticalInput * horsePowers);
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 3.6f);
            speedometer.SetText($"Speed: {speed} km/h");
            rpm = Mathf.Round((speed % 30) * 40);
            rpmText.SetText($"RPM: {rpm}");
        }
    }

    bool IsOnGround()
    {
        wheelIsOnGround = 0;
        foreach (var wheel in wheels)
        {
            if (wheel.isGrounded) 
            {
                wheelIsOnGround++;
            }
        }
        if (wheelIsOnGround == 4) return true;
        else return false;
    }
}
