using UnityEngine;

public enum EVENT_TYPE
{
    TileChange,
    PlayerHPChanged,
    PlayerDead,
    TickRisingEdge
}

public interface IListener
{
    void OnEvent(EVENT_TYPE eType, Component sender, object param = null);
}
