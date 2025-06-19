using UnityEngine;

public class AtributesInfos : MonoBehaviour
{
    public TextMeshProUGui m_dps;
    public TextMeshProUGui m_jump;
    public TextMeshProUGui m_bullet;
    public ListUpgrade m_listUpgrade;

    private void Update()
    {
        foreach (var upgrade in m_listUpgrade.UpgradeList) {
            print(upgrade);
        }
    }
}
