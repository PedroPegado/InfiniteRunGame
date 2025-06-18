using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class EnemyController : MonoBehaviour
{
    public float m_moveSpeedEnemy = 5f;
    private Vector2 m_enemyDirection;
    private Rigidbody2D m_enemyRb;
    private CircleCollider2D m_enemyCollider;
    private SpriteRenderer m_enemySR;
    public string m_tagTargetDetection = "Player";
    private bool m_isDead = false;
    public event Action OnDeath;
    public DetectionArea _detectionArea;
    [SerializeField] private VisualEffect m_enemyDie;
    [SerializeField] private VisualEffect m_enemyTrail;

    void Start()
    {
        m_enemyRb = GetComponent<Rigidbody2D>();
        m_enemySR = GetComponent<SpriteRenderer>();
        m_enemyCollider = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        if (_detectionArea.dectectedObjs.Count > 0)
        {
            m_enemyDirection = (_detectionArea.dectectedObjs[0].transform.position - transform.position).normalized;

            m_enemyRb.linearVelocity = m_enemyDirection * m_moveSpeedEnemy;
        }
        else
        {
            m_enemyRb.linearVelocity = Vector2.zero;
        }
    }

    public void Die()
    {
        if (m_isDead) return;
        m_isDead = true;

        m_enemyTrail.Stop();
        m_enemyDirection = Vector2.zero;
        m_enemyRb.linearVelocity = Vector2.zero;
        m_moveSpeedEnemy = 0;
        OnDeath?.Invoke();
        m_enemyDie.Play();

        m_enemyCollider.enabled = false;
        m_enemySR.DOFade(0f, 0.1f);
        StartCoroutine(RemoveAfterAnimation());
    }

    IEnumerator RemoveAfterAnimation()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
