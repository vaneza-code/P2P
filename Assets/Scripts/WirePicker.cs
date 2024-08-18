using UnityEngine;

public class WirePicker : MonoBehaviour
{
    public Transform holdParent;
    public WireStripper wireStripper; // Reference to the WireStripper script

    private GameObject heldObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Pick up or drop object
        {
            if (heldObject != null)
            {
                DropObject();
            }
            else
            {
                TryPickUpObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) // Attempt to insert wire
        {
            if (heldObject != null && heldObject.CompareTag("Wire") && wireStripper != null)
            {
                wireStripper.InsertWire(heldObject);
                // Optionally, you might want to drop the wire after inserting
                DropObject();
            }
            else
            {
                Debug.LogWarning("Cannot insert wire: Either no wire is held or WireStripper is not assigned.");
            }
        }
    }

    private void TryPickUpObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f))
        {
            if (hit.transform.CompareTag("Wire") || hit.transform.CompareTag("WireStripper"))
            {
                heldObject = hit.transform.gameObject;
                Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                heldObject.transform.SetParent(holdParent);
                heldObject.transform.localPosition = Vector3.zero;
            }
        }
    }

    private void DropObject()
    {
        if (heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            heldObject.transform.SetParent(null);
            heldObject = null;
        }
    }
}
