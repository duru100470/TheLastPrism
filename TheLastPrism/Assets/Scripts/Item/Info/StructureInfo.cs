using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new StructureInfo", menuName = "Item/Structure Info")]
public class StructureInfo : ItemInfo
{
    public STRUCTURE_ID structureId;
    public Sprite structureSprite;
    public int width;
}
