using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStructure : Structure
{
    [Header("Tree")]
    [SerializeField]
    private float hammerMultiplier;
    [SerializeField]
    private List<Sprite> treeSprite;
    [SerializeField]
    private int treeGrow = 0;
    [SerializeField]
    private int treeLevel = 0;
    [SerializeField]
    private int maxTreeLevel;

    protected override void Awake() {
        base.Awake();
        EventManager.Instance.AddListener(EVENT_TYPE.TickRisingEdge, this);
    }

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
        EventManager.Instance.RemoveListener(EVENT_TYPE.TickRisingEdge, this);
        base.Dead();
    }

    public override void OnEvent(EVENT_TYPE eType, Component sender, object param = null)
    {
        base.OnEvent(eType, sender, param);
        if (eType != EVENT_TYPE.TickRisingEdge || treeGrow == 2) return;

        if (Random.Range(0f, 100f) < 2f)
        {
            Debug.Log("Tree Grown");
            Grow();
        }
    }

    private void Grow()
    {
        treeLevel++;

        if (treeLevel > maxTreeLevel)
        {
            treeGrow++;
            treeLevel = 0;
            GetComponent<SpriteRenderer>().sprite = treeSprite[treeGrow];
        }
    }
}
