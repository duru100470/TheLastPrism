using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void GetDamage(int amount, float stunDuration, float invTime, bool ignoreInvTime);
    void Dead();
}
