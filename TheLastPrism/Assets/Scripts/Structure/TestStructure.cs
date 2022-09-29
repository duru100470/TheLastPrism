using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStructure : Structure
{
    [SerializeField]
    private float hammerMultiplier;

    public override void GetDamage(int amount, DAMAGE_TYPE dmgType, float invTime, bool ignoreInvTime)
    {
        switch (dmgType)
        {
            case DAMAGE_TYPE.Hammer:
                health = Mathf.Max(0, health - (int)(amount * hammerMultiplier));
                break;
        }

        if (health == 0)
            Dead();
    }

    public override void Dead()
    {
        base.Dead();
    }
}
