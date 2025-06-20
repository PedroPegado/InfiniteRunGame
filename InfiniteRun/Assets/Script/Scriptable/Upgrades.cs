using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

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