using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    public float MaxThrowRange = 7f;
    public float MinThrowRange = 0.7f;
    public float height = 1.5f;
    public float gravity = -18f;
    private Vector3 target;

     

    public void ItemThrowing(Ray ray)
    {
        if (PlayerController.instance.ThrowableObject == null)
            return;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null && Vector3.Distance(transform.position, hit.point) <= MaxThrowRange && Vector3.Distance(transform.position, hit.point) >= MinThrowRange)
            {
                //atýþ
                target = hit.point;
                Vector3 lookRotation = target - transform.position;
                lookRotation.y = 0;
                transform.rotation = Quaternion.LookRotation(lookRotation, Vector3.up);
                Launch();

            }

        }
    }

    private void Launch()
    {
        if (GetComponent<PlayerController>().ThrowableObject == null)
            return;

        Physics.gravity = Vector3.up * gravity;
        GameObject trwObject = GetComponent<PlayerController>().ThrowableObject;
        trwObject.transform.position = transform.position + transform.forward + transform.up;
        trwObject.SetActive(true);
        Rigidbody itemRb;
        itemRb = trwObject.GetComponent<Rigidbody>();
        itemRb.velocity = CalculateVelocity();
        trwObject.GetComponent<ThrowableNotification>().isLaunched = true;
        GetComponent<PlayerController>().ThrowableObject = null;
    }

    private Vector3 CalculateVelocity()
    {
        float displacementY = target.y - transform.position.y;
        Vector3 displacementXZ = target - transform.position;
        displacementXZ.y = 0;

        height = /*0.5f*/ + (displacementXZ.magnitude * 0.2f);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));
        return velocityXZ + velocityY * -Mathf.Sign(gravity);
    }
}
