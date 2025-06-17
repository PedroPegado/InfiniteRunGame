using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private bool m_isGrounded = false;
    private Rigidbody2D m_playerRb;
    private Transform m_playerTrans;
    public float m_jumpForce = 10f;
    public float m_rotationDuration = .5f;
    private float m_currentZRotation = 0f;
    public string lvlName;
    public Transform m_visual;
    public GameObject m_bullet;
    public float m_bulletSpeed = 10f;
    public GameObject m_canvasDie;
    public GameObject m_canvasMainMenu;

    private void Start()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
        m_playerTrans = GetComponent<Transform>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && m_isGrounded)
        {
            m_playerRb.linearVelocity = new Vector2(m_playerRb.linearVelocity.x, m_jumpForce);
            m_currentZRotation -= 90f;
            StartCoroutine(RotateZSmooth(m_currentZRotation, m_rotationDuration));
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorldPos.z = 0f;

            Vector2 direction = (mouseWorldPos - transform.position).normalized;

            GameObject bullet = Instantiate(m_bullet, transform.position, Quaternion.identity);

            Collider2D playerCollider = GetComponent<Collider2D>();
            Collider2D bulletCollider = bullet.GetComponent<Collider2D>();

            if (playerCollider != null && bulletCollider != null)
            {
                Physics2D.IgnoreCollision(bulletCollider, playerCollider);
            }

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = direction * m_bulletSpeed;
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

            //m_visual.localScale = Vector3.one;

            //Vector3 originalPos = m_visual.localPosition;
            //float squashY = 0.1f;

            //float zRot = m_playerTrans.eulerAngles.z;

            //bool isUpsideDown = Mathf.Abs(Mathf.DeltaAngle(zRot, 180f)) < 10f;

            //if (isUpsideDown)
            //{
            //    squashY = -squashY;
            //}

            //Sequence bounce = DOTween.Sequence();
            //bounce.Append(m_visual.DOScale(new Vector3(1.2f, 0.8f, 1f), 0.1f));
            //bounce.Join(m_visual.DOLocalMoveY(originalPos.y - squashY, 0.1f));
            //bounce.Append(m_visual.DOScale(Vector3.one, 0.1f));
            //bounce.Join(m_visual.DOLocalMoveY(originalPos.y, 0.1f));
        }

        if (collision.gameObject.CompareTag("DamageObject") || (collision.gameObject.CompareTag("Enemy"))){
            m_canvasDie.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = false;
        }
    }

    public void ResetLvl(string lvl)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(lvl);
        m_canvasDie.SetActive(false);
    }
}
