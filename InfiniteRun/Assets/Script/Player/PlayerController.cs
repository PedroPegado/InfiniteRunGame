using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    private bool        m_isGrounded = false;
    private Rigidbody2D m_playerRb;
    private Transform   m_playerTrans;
    public float        m_jumpForce = 10f;
    public float        m_rotationDuration = .5f;
    private float       m_currentZRotation = 0f;
    public string       lvlName;
    public Transform    m_visual;
    public GameObject   m_bullet;
    public float        m_bulletSpeed = 10f;
    public GameObject   m_canvasDie;
    private Vector2     m_playerDirection;
    public float        m_playerSpeed = 5f;
    public Transform    m_gunTransform;
    public Transform    m_bulletSpawnPoint;
    public Transform                        m_gunPivot;
    [SerializeField] private VisualEffect   m_groundDust;
    [SerializeField] private Transform      m_dustAnchor;

    [Header("Upgrades Settings")]
    public Upgrades m_attackSpeed;
    public Upgrades m_jumpUpgrade;
    public Upgrades m_bulletSizeUpgrade;
    private bool m_canShoot = true;
    private float m_delayToShoot = 1f;


    [Header("Audio Settings")]
    [SerializeField] private AudioSource m_musicLoop;
    [SerializeField] private AudioSource m_sfxSource;
    [SerializeField] private AudioClip m_dieSound;
    [SerializeField] private AudioClip m_shootSound;

    [Header("Jump Settings")]
    public float m_jumpInitialForce = 4f;
    public float m_jumpHoldForce = 5f; 
    public float m_maxJumpTime = 0.3f;

    private bool m_isJumping = false;
    private float m_jumpTimeCounter = 0f;
    private bool m_jumpPressed = false;
    private bool m_jumpHeld = false;
    private bool m_jumpReleased = false;

    [Header("Jump Buffer")]
    public float m_jumpBufferTime = 0.1f;
    private float m_jumpBufferTimer = 0f;


    private void Start()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
        m_playerTrans = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (m_playerDirection.sqrMagnitude > 0.1)
            MovePlayer();

        if (m_jumpBufferTimer > 0)
            m_jumpBufferTimer -= Time.fixedDeltaTime;

        HandleJump();

        if (m_isGrounded)
            m_groundDust.Play();
        else if (!m_isGrounded || m_playerDirection.sqrMagnitude < 0.1)
            m_groundDust.Stop();

        m_dustAnchor.position = new Vector3(
            m_playerTrans.position.x - 0.45f,
            m_playerTrans.position.y - 0.45f,
            m_playerTrans.position.z
        );

        RotateGunAroundPlayer();
    }

    void MovePlayer()
    {
        Vector2 velocity = m_playerRb.linearVelocity;
        velocity.x = m_playerDirection.normalized.x * m_playerSpeed;
        m_playerRb.linearVelocity = velocity;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_playerDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            m_jumpBufferTimer = m_jumpBufferTime;
            m_jumpHeld = true;
        }

        if (context.canceled)
        {
            m_jumpHeld = false;
            m_jumpReleased = true;
        }
    }


    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started && m_canShoot)
        {
            m_sfxSource.PlayOneShot(m_shootSound, 0.1f);
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorldPos.z = 0f;

            Vector2 direction = (mouseWorldPos - transform.position).normalized;

            GameObject bullet = Instantiate(m_bullet, m_bulletSpawnPoint.position, m_gunPivot.rotation);
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x + m_bulletSizeUpgrade.getValue(), bullet.transform.localScale.y + m_bulletSizeUpgrade.getValue(), bullet.transform.localScale.z + m_bulletSizeUpgrade.getValue());
            bullet.GetComponent<Rigidbody2D>().linearVelocity = m_gunPivot.right * m_bulletSpeed;

            Collider2D playerCollider = GetComponent<Collider2D>();
            Collider2D bulletCollider = bullet.GetComponent<Collider2D>();

            if (playerCollider != null && bulletCollider != null)
            {
                Physics2D.IgnoreCollision(bulletCollider, playerCollider);
            }

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.linearVelocity = direction * m_bulletSpeed;
            m_canShoot = false;
            StartCoroutine(DelayToShoot(m_delayToShoot - m_attackSpeed.getValue()));
        }
    }

    IEnumerator DelayToShoot(float delay)
    {
        yield return new WaitForSeconds(delay);
        m_canShoot = true;
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
            
            if (m_musicLoop != null)
            {
                m_musicLoop.Stop();
            }

            if (m_sfxSource != null && m_dieSound != null) {
                m_sfxSource.PlayOneShot(m_dieSound, 0.1f);
            }

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

    void RotateGunAroundPlayer()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0f;

        Vector3 direction = mousePos - m_gunPivot.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        m_gunPivot.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void HandleJump()
    {
        if (m_jumpBufferTimer > 0 && m_isGrounded)
        {
            m_isJumping = true;
            m_jumpTimeCounter = m_maxJumpTime;
            m_playerRb.linearVelocity = new Vector2(m_playerRb.linearVelocity.x, m_jumpInitialForce + m_jumpUpgrade.getValue());
            m_currentZRotation -= 90f;
            StartCoroutine(RotateZSmooth(m_currentZRotation, m_rotationDuration));

            m_jumpBufferTimer = 0f;
        }

        if (m_isJumping && m_jumpHeld)
        {
            if (m_jumpTimeCounter > 0)
            {
                m_playerRb.linearVelocity = new Vector2(m_playerRb.linearVelocity.x, m_jumpHoldForce + m_jumpUpgrade.getValue());
                m_jumpTimeCounter -= Time.fixedDeltaTime;
            }
            else
            {
                m_isJumping = false;
            }
        }

        if (m_jumpReleased)
        {
            m_isJumping = false;
        }

        m_jumpPressed = false;
        m_jumpReleased = false;
    }
}
