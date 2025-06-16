using UnityEngine;

public class ThornController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -45;
    void Update()
    {

        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("thorn deleted");
            Destroy(gameObject);
        }
    }
}
