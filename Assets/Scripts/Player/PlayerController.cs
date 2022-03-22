using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private void Awake()
    {
        instance = this;    
    }


    #region ThrowObject
    public Vector3 targetPoint;
    public float height = 1.5f;
    public float gravity = -18f;

    public GameObject ThrowableObject;
    private Rigidbody objectRb;

    public void TakeObject(GameObject trwObject)
    {
        if (ThrowableObject != null)
        {
            ThrowableObject.transform.position = transform.position;
            ThrowableObject.SetActive(true);

        }

        ThrowableObject = trwObject;
        ThrowableObject.SetActive(false);

    }
    Vector3 CalculateVelocity()
    {
        float displacementY = targetPoint.y - transform.position.y;
        Vector3 displacementXZ = new(targetPoint.x - transform.position.x, 0, targetPoint.z - transform.position.z);
        height = displacementXZ.magnitude * 0.2f;
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));
        return velocityXZ + velocityY * -Mathf.Sign(gravity);
    }
    public void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        objectRb = ThrowableObject.GetComponent<Rigidbody>();
        objectRb.velocity = CalculateVelocity();

    }
    #endregion
}
