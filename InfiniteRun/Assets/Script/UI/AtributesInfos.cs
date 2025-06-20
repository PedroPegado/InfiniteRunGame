using UnityEngine;
using TMPro;

public class AtributesInfos : MonoBehaviour
{
    public TextMeshProUGUI m_dps;
    public TextMeshProUGUI m_jump;
    public TextMeshProUGUI m_bullet;
    public ListUpgrade m_listUpgrade;

    private void Update()
    {
        foreach (var upgrade in m_listUpgrade.UpgradeList)
        {
            print(upgrade);
        }
    }
}
