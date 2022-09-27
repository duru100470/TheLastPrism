using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void GetDamage(int amount, DAMAGE_TYPE dmgType, float invTime, bool ignoreInvTime);
    void Dead();
}

public enum DAMAGE_TYPE
{
    // Entity damage type
    Melee,
    Projectile,
    Falling,
    Lava,
    Drowned,
    Suffocated,
    Infected,
    // Tile, structure damage type
    Hand,
    Pickaxe,
    Hammer
}