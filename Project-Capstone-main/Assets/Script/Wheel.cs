using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public MotorcycleControl motorcycleControl;
    public Transform wheelTransform;
    public float rotationMultiplier = 500f;

    private void Update()
    {
        // Calculate rotation based on motorcycle speed
        float speed = Mathf.Abs(motorcycleControl.currentSpeed);
        float rotationAmount = speed * rotationMultiplier * Time.deltaTime;

        // Apply rotation to the wheel
        wheelTransform.Rotate(Vector3.right, rotationAmount);
    }
}
