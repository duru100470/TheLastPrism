using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void GetDamage(int amount, float stunDuration, float invTime, bool ignoreInvTime);
    void Dead();
}
