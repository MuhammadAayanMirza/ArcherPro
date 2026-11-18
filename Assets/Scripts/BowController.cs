using UnityEngine;

public class BowController : MonoBehaviour
{
    [Header("Bow Stuff")]
    public float maxDrawTime = 2f;
    public float drawTime = 0f;

    public bool isDrawing = false;

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
        }

        if (isDrawing && Input.GetMouseButtonUp(0))
        {
            isDrawing = false;

            Debug.Log("Bow Out! Draw Time:" + drawTime);
        }
    }
}
