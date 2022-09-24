using UnityEngine;

[System.Serializable]
public abstract class Item
{
    [SerializeField]
    protected ItemInfo itemInfo;
    [SerializeField]
    protected int amount;
    
    public virtual ItemInfo ItemInfo => itemInfo;
    public virtual int Amount
    {
        get { return amount; }
        set { amount = Mathf.Clamp(value, 0, itemInfo.maxStack); }
    }

    public abstract Item DeepCopy();
    public abstract void OnLeftClick();
    public abstract void OnRightClick();
}