using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListUpgrade", menuName = "Scriptable Objects/ListUpgrade/ListUpgrade")]
public class ListUpgrade : ScriptableObject
{
    public List<Upgrades> UpgradeList = new List<Upgrades>();
}