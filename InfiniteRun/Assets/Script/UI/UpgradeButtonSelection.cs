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

    private void Start()
    {
        m_canvasTransform = GetComponent<RectTransform>();

        m_canvasTransform.DOAnchorPosY(0f, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
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

        if (m_distanceCounter != null) {
            m_distanceCounter.ResumeGame();
            Time.timeScale = 1f;
        }
    }
}
