using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomUpgrade : MonoBehaviour
{
    public ListUpgrade m_listUpgrade;
    public List<UpgradeButtonSelection> m_buttonList = new List<UpgradeButtonSelection>();

    private void Start()
    {
        List<Upgrades> availableUpgrades = new List<Upgrades>(m_listUpgrade.UpgradeList);

        foreach (var button in m_buttonList)
        {
            if (availableUpgrades.Count == 0) break;

            int randomIndex = Random.Range(0, availableUpgrades.Count);
            Upgrades selectedUpgrade = availableUpgrades[randomIndex];
            button.SetData(selectedUpgrade);

            availableUpgrades.RemoveAt(randomIndex);
        }
    }
}
