using UnityEngine;

public class BowController : MonoBehaviour
{
    [Header("Bow Stuff")]
    public float maxDrawTime = 2f;
    public float drawTime = 0f;

    public float drawStrength = 0f;

    public bool isDrawing = false;

    [Header("Arrow Thingy")]
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float minArrowSpeed = 10f;
    public float maxArrowSpeed = 50f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            drawTime = 0f;
        }

        if ( isDrawing && Input.GetMouseButton(0))
        {
            drawTime += Time.deltaTime;
            drawTime = Mathf.Clamp(drawTime, 0f, maxDrawTime);
            drawStrength = drawTime / maxDrawTime;
        }

        if (isDrawing && Input.GetMouseButtonUp(0))
        {
            isDrawing = false;

            ShootArrow();

            Debug.Log("Bow Out! Draw Time:" + drawTime);
            Debug.Log("DrawStrength:" + drawStrength);
        }
    }

    void ShootArrow()
    {
        float arrowSpeed = Mathf.Lerp(minArrowSpeed, maxArrowSpeed, drawStrength);
        
        GameObject arrow = Instantiate(
            arrowPrefab,
            arrowSpawnPoint.position,
            arrowSpawnPoint.rotation
        );

        

        ArrowController arrowController = arrow.GetComponent<ArrowController>();

        arrowController.IgnoreCollisionWith(
            GetComponentInParent<CharacterController>()
        );

        Collider[] bowColliders = GetComponentsInChildren<Collider>();
        Collider[] arrowColliders = arrow.GetComponentsInChildren<Collider>();

        foreach (Collider bowCollider in bowColliders)
        {
            foreach (Collider arrowCollider in arrowColliders)
            {
                Physics.IgnoreCollision(arrowCollider, bowCollider);
            }
        }

        arrowController.Launch(arrowSpeed);
    }
}
