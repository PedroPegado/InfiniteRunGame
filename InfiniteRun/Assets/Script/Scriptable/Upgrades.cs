using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "ListUpgrade", menuName = "Scriptable Objects/ListUpgrade/ListUpgrade")]
public class ListUpgrade : ScriptableObject
{
    public List<Upgrades> UpgradeList = new List<Upgrades>();
}

[CreateAssetMenu(fileName = "Upgrades", menuName = "Scriptable Objects/ListUpgrade/Upgrades")]
public class Upgrades : ScriptableObject
{
    public string upgradeName;
    public string description;
    public int m_lvlAct;
    public int m_lvlMax;
    public float m_valueByValue;

    public float getValue()
    {
        return m_valueByValue * m_lvlAct;
    }
}