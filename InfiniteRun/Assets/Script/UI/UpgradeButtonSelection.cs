using TMPro;
using UnityEngine;

public class UpgradeButtonSelection : MonoBehaviour
{
    public TextMeshProUGUI m_title;
    public TextMeshProUGUI m_description;
    private Upgrades m_actUpgrade;
    private DistanceCounter m_distanceCounter;

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
        }
    }
}
