using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    public Player CurPlayer { get; private set; }

    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private GameObject testPrefab;

    public GameObject ItemPrefab => itemPrefab;
    public GameObject TestPrefab => testPrefab;

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

        CurPlayer = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine(DoGameTick());
    }

    private IEnumerator DoGameTick()
    {
        while(true)
        {
            EventManager.Instance.PostNotification(EVENT_TYPE.TickRisingEdge, null);
            yield return new WaitForSeconds(.05f);
        }
    }
}
