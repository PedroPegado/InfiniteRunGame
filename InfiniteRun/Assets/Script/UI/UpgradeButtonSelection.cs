using TMPro;
using UnityEngine;
using DG.Tweening;

public class UpgradeButtonSelection : MonoBehaviour
{
    public TextMeshProUGUI m_title;
    public TextMeshProUGUI m_description;
    private Upgrades m_actUpgrade;
    public DistanceCounter m_distanceCounter;
    private RectTransform m_canvasTransform;
    private Animator m_animator;

    private bool m_isIdleActive = false;
    private bool m_wasHighlighted = false;

    private void Start()
    {
        m_canvasTransform = GetComponent<RectTransform>();
        m_animator = GetComponent<Animator>();

        m_canvasTransform.DOAnchorPosY(0f, 0.5f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true)
            .OnComplete(() => {
                m_isIdleActive = true;
                StartIdleEffect();
            });
    }

    private void Update()
    {
        if (m_animator == null) return;

        bool isHighlighted = m_animator.GetCurrentAnimatorStateInfo(0).IsName("Highlighted");

        if (isHighlighted && !m_wasHighlighted)
        {
            DOTween.Kill(m_canvasTransform); 
            m_isIdleActive = false;
            m_wasHighlighted = true;
        }
        else if (!isHighlighted && m_wasHighlighted)
        {
            if (!m_isIdleActive)
            {
                StartIdleEffect();
                m_isIdleActive = true;
            }
            m_wasHighlighted = false;
        }
    }

    private void StartIdleEffect()
    {
        m_canvasTransform.DOLocalRotate(
            new Vector3(0f, 30f, 0f),
            2f
        ).SetEase(Ease.InOutSine)
         .SetLoops(-1, LoopType.Yoyo)
         .SetUpdate(true);

        m_canvasTransform.DOScale(
            new Vector3(1.01f, 1.01f, 1f),
            2.2f
        ).SetEase(Ease.InOutSine)
         .SetLoops(-1, LoopType.Yoyo)
         .SetUpdate(true);
    }

    public void SetData(Upgrades upgrade)
    {
        m_title.text = upgrade.upgradeName;
        m_description.text = upgrade.description;
        m_actUpgrade = upgrade;
    }

    public void SelectUpgrade()
    {
        m_actUpgrade.m_lvlAct++;

        DOTween.Kill(m_canvasTransform);

        if (m_distanceCounter != null)
        {
            m_distanceCounter.ResumeGame();
            Time.timeScale = 1f;
        }
    }

}
