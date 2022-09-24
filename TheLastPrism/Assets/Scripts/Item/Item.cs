using UnityEngine;

[System.Serializable]
public abstract class Item
{
    [SerializeField]
    protected int amount;

    public abstract ItemInfo ItemInfo { get; }
    public abstract int Amount { get; set; }
    public abstract Item DeepCopy();
    public abstract void OnLeftClick();
    public abstract void OnRightClick();
}