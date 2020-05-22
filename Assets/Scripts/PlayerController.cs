using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    [SerializeField] float speed = 15.5f;
    [SerializeField] float turnSpeed = 40.0f;
    
    private float horizontalInput;
    private float forwardInput;

// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Axis setup
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        // Move the car forward based on vertical
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        
        // Rotates the car on horizontal input
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
    }
}
