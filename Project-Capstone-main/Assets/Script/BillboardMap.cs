using UnityEngine;

public class BillboardMap : MonoBehaviour
{
    private Transform fuelTransform;
    private Quaternion initialRotation;
    public bool billboardEnabled = true;

    void Start()
    {
        fuelTransform = transform.parent; // Assuming Fuel is the parent
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (billboardEnabled && fuelTransform != null)
        {
            // Apply the initial rotation to maintain orientation
            transform.rotation = initialRotation;

            // Optionally, you can adjust the position to follow Fuel's position
            transform.position = fuelTransform.position;
        }
    }
}
