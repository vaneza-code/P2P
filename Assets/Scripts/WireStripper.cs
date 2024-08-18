using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireStripper : MonoBehaviour
{
    public Transform wireInsertPoint; // The point where the wire should be inserted
    public float rotationSpeed = 50f;

    private bool isStripping = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Toggle stripping
        {
            isStripping = !isStripping;
        }

        if (isStripping)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    public void InsertWire(GameObject wire)
    {
        if (wireInsertPoint != null)
        {
            // Make the wire a child of the wireInsertPoint
            wire.transform.SetParent(wireInsertPoint);
            wire.transform.localPosition = Vector3.zero; // Adjust position to fit the insert point
            wire.transform.localRotation = Quaternion.identity; // Reset rotation

            // Optional: Set a fixed scale or other properties if needed
            wire.transform.localScale = Vector3.one;

            Debug.Log("Wire inserted into the wire stripper");
        }
        else
        {
            Debug.LogError("WireInsertPoint is not assigned in WireStripper.");
        }
    }
}
