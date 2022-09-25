using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void GetDamage(int amount, float invTime, bool ignoreInvTime);
    void Dead();
}
