using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour, IListener
{
    private static UIManager _instance = null;
    public static UIManager Instance => _instance;
    [SerializeField]
    private TextMeshProUGUI playerHealthTMP;
    [SerializeField]
    private Inventory inventory;

    public Inventory Inventory => inventory;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.PlayerHPChanged, this);
        
        playerHealthTMP.text = GameManager.Instance.CurPlayer.Health.ToString();
    }

    public void OnEvent(EVENT_TYPE eType, Component sender, object param = null)
    {
        switch (eType)
        {
            case EVENT_TYPE.PlayerHPChanged:
                playerHealthTMP.text = GameManager.Instance.CurPlayer.Health.ToString();
                break;
        }
    }
}
