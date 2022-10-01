using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IDamage
{
    [Header("Tile")]
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private TILE_ID tileId;
    [SerializeReference]
    private List<Item> dropItemList;

    [Header("Damage Multiplier")]
    [SerializeField]
    private float handMultiplier = 1;
    [SerializeField]
    private float pickaxeMultiplier = 5;

    public Coordinate Pos { get; set; }
    public TILE_ID TileId => tileId;
    public List<Item> DropItemList => dropItemList;
    public int Health => health;
    public TileCrackEffect crackEffect { get; set; }

    protected virtual void Awake()
    {
        maxHealth = health;
    }

    public virtual void GetDamage(int amount, DAMAGE_TYPE dmgType, float invTime, bool ignoreInvTime)
    {
        switch (dmgType)
        {
            case DAMAGE_TYPE.Hand:
                health = Mathf.Max(0, health - (int)(amount * handMultiplier));
                break;
            case DAMAGE_TYPE.Pickaxe:
                health = Mathf.Max(0, health - (int)(amount * pickaxeMultiplier));
                break;
        }

        if (((float)health / (float)maxHealth) > 0.8f)
            crackEffect.UpdateSprite(0);
        else if (((float)health / (float)maxHealth) > 0.6f)
            crackEffect.UpdateSprite(1);
        else if (((float)health / (float)maxHealth) > 0.4f)
            crackEffect.UpdateSprite(2);
        else if (((float)health / (float)maxHealth) > 0.2f)
            crackEffect.UpdateSprite(3);
        else if (((float)health / (float)maxHealth) > 0f)
            crackEffect.UpdateSprite(4);

        if (health == 0)
            Dead();
    }

    public virtual void Dead()
    {
        TileManager.Instance.DestroyTile(Pos);
    }
}