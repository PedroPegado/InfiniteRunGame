using UnityEngine;

public class ThornController : MonoBehaviour
{
    private float moveSpeed = 8f;
    public float deadZone = -30;
    void Update()
    {

        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}
