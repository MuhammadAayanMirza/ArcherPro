using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Rigidbody rb;

    public bool hasHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        IgnoreOtherArrows();
    }

    public void Launch(float speed)
    {
        rb.linearVelocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit)
        {
            return;
        }

        if (!collision.gameObject.CompareTag("Target"))
        {
            return;
        }

        hasHit = true;

        ContactPoint contact = collision.GetContact(0);

        float impactAngle = Vector3.Angle(transform.forward, -contact.normal);

        Debug.Log("Impact angle: " + impactAngle);

        Vector3 impactPosition = transform.position;
        Quaternion impactRotation = transform.rotation;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        transform.SetParent(collision.transform, true);

        transform.SetPositionAndRotation(impactPosition, impactRotation);

        Debug.Log("Arrow hit:" + collision.gameObject.name);
    }

    public void IgnoreCollisionWith(Collider otherCollider)
    {
        Collider arrowCollider = GetComponent<Collider>();

        Physics.IgnoreCollision(arrowCollider, otherCollider);
    }

    public void IgnoreOtherArrows()
    {
        Collider[] arrowColliders = GetComponentsInChildren<Collider>();

        ArrowController[] existingArrows = FindObjectsByType<ArrowController>(FindObjectsSortMode.None);
        
        foreach (ArrowController otherArrow in existingArrows)
        {
            if (otherArrow == this)
                continue;

            Collider[] otherColliders = otherArrow.GetComponentsInChildren<Collider>();

            foreach (Collider arrowCollider in arrowColliders)
            {
                foreach (Collider otherCollider in otherColliders)
                {
                    Physics.IgnoreCollision(arrowCollider, otherCollider);
                }
            }
        }

}

}
