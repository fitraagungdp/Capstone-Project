using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MotorcycleControl : MonoBehaviour
{
    public static MotorcycleControl instance;

    [Header("Fuel")]
    public GameObject collectibleFuel;
    public GameObject[] collectibleFuelPermanent;
    public Slider fuelBar;
    public float addGas;
    public float currentGas;

    [Header("Movement")]
    public float moveSpeed = 10f;
    public float maxSpeed = 20f;
    public float acceleration = 2f;
    public float braking = 4f;
    public float turnSpeed = 5f;

    public float currentSpeed = 0f;
    public bool isTutorial;

    private Rigidbody rb;
    private FuelSystem fuelSystem;

    private void Start()
    {
        fuelSystem = FindObjectOfType<FuelSystem>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        // Acceleration (using "W" key)
        float accelerationInput = Input.GetKey(KeyCode.W) ? 1f : 0f;
        currentSpeed += accelerationInput * acceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        // Apply braking (using "S" key)
        if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, braking * Time.deltaTime);
        }
        // Gradually decrease speed when "W" key is released
        else if (currentSpeed > 0f && !Input.GetKey(KeyCode.W))
        {
            float releaseBrakeFactor = 0.1f; // Adjust this value for the desired effect
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, (braking * releaseBrakeFactor) * Time.deltaTime);
        }

        // Turning
        float horizontalInput = Input.GetAxis("Horizontal");
        float rotationAmount = horizontalInput * turnSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotationAmount);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // Move forward based on currentSpeed
        Vector3 forwardVelocity = transform.forward * currentSpeed;
        rb.velocity = new Vector3(forwardVelocity.x, rb.velocity.y, forwardVelocity.z);

        // Decrease Gas
        if (currentSpeed > 0)
        {
            currentGas -= Time.deltaTime * 0.5f;
        }

        fuelBar.value = currentGas;

        if (currentGas <= 0)
        {
            //condition if fuel is empty
            currentGas = 0;
            currentSpeed = 0;

            if (isTutorial)
            {
                GameManager.Instance.TutorialOver();
            }
            else 
            {
                GameManager.Instance.GameOver();
            }
            
        }

        else if (currentGas > 100)
        {
            currentGas = 100;
        }
    }

    //Pickup Fuel
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == collectibleFuel)
        {
            collectibleFuel.SetActive(false);
            StartCoroutine(GetPosition());
            currentGas += addGas;
            SoundManager.instance.FuelSfx();
        }

        if (other.gameObject.CompareTag("FuelPermanent"))
        {
            currentGas += addGas;
            SoundManager.instance.FuelSfx();
        }
    }
    //Update Position Of Fuel
    private IEnumerator GetPosition()
    {
        yield return new WaitForSeconds(3f);
        fuelSystem.GetPosition();
        collectibleFuel.SetActive(true);
    }

    //Reset currentspeed to 0
    public void ResetCurrentSpeed()
    {
        currentSpeed = 0f;
    }
}
