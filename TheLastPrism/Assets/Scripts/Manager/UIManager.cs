using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour, IListener
{
    [SerializeField]
    private TextMeshProUGUI playerHealthTMP;

    private void Update()
    {
        playerHealthTMP.text = GameManager.Instance.CurPlayer.Health.ToString();
    }

    public void OnEvent(EVENT_TYPE eType, Component sender, object param = null)
    {

    }
}
