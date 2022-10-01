using UnityEngine;

[CreateAssetMenu(fileName = "new ToolInfo", menuName = "Item/Tool Info")]
public class ToolInfo : ItemInfo
{
    public DAMAGE_TYPE dmgType;
    public int damage;
    public int maxDurability;
}
