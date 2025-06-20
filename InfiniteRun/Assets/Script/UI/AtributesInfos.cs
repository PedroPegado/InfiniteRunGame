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
        m_dps.text = "lvl " + m_listUpgrade.UpgradeList[0].m_lvlAct.ToString() + "/3";
        m_bullet.text = "lvl " + m_listUpgrade.UpgradeList[1].m_lvlAct.ToString() + "/3";
        m_jump.text = "lvl " + m_listUpgrade.UpgradeList[2].m_lvlAct.ToString() + "/3";
    }
}
