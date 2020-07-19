using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    public float speed;
    [SerializeField] float turnSpeed = 40.0f;

    private float horizontalInput;
    private float forwardInput;

    [SerializeField] Rigidbody playerRb;
    [SerializeField] float horsePower;
    [SerializeField] float rpm;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI gearText;
    private int gear;
    public int gearInterval = 8;
    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;
    [SerializeField] TextMeshProUGUI rpmText;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If at least tow wheel on ground do below.
        if (IsOnGround())
        {
            //ACCELERATION
            // Axis setup
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");
            // Move the car forward based on vertical
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            playerRb.AddRelativeForce(Vector3.forward * forwardInput * horsePower);
            
            //SPEED
            // display speed in kph
            speed = playerRb.velocity.magnitude;
            int kph = (int)(speed * 3.6f); // 2.237 for mph
            speedometerText.text = "Speed: " + kph + " kph";

            //RPM
            rpm = Mathf.Round((speed % 30) * 40);
            rpmText.SetText("RPM: " + rpm);

            //display gear
            if (kph % gearInterval == 0)
            {
                gear = kph / gearInterval;
                gearText.SetText("Gear: " + gear);
            }

            //ROTATION
            // Rotates the car on horizontal input
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        }
    }

    private bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }
        if (wheelsOnGround >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
