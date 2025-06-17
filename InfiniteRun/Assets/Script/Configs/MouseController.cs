using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Camera _cam;

    private void Update()
    {
        if (Mouse.current == null) return; 

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = _cam.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;
        transform.position = worldPos;
    }
}
