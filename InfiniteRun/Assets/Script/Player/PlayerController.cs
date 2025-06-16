using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool m_isGrounded = false;
    private Rigidbody2D m_playerRb;
    private Transform m_playerTrans;
    public float m_jumpForce = 10f;
    public float m_rotationDuration = .5f;
    private float m_currentZRotation = 0f;
    public string lvlName;

    private void Start()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
        m_playerTrans = GetComponent<Transform>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && m_isGrounded)
        {
            m_playerRb.linearVelocity = new Vector2(m_playerRb.linearVelocity.x, m_jumpForce);
            m_currentZRotation -= 90f;
            StartCoroutine(RotateZSmooth(m_currentZRotation, m_rotationDuration));
        }
    }

    private IEnumerator RotateZSmooth(float targetZ, float duration)
    {
        Quaternion startRotation = m_playerTrans.rotation;
        Quaternion endRotation = Quaternion.Euler(0f, 0f, targetZ);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            m_playerTrans.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        m_playerTrans.rotation = endRotation;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = true;
        }
        if (collision.gameObject.CompareTag("DamageObject"))
            SceneManager.LoadScene(lvlName);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = false;
        }
    }
}
