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
    [SerializeField]
    private ToolInfo startPickaxeInfo;
    [SerializeField]
    private ToolInfo startHammerInfo;

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

        CurPlayer = GameObject.Find("PlayerController").GetComponent<Player>();
        StartCoroutine(DoGameTick());
    }

    private void Start() {
        Item startPickaxe = new ItemTool(startPickaxeInfo, 1);
        Item startHammer = new ItemTool(startHammerInfo, 1);

        UIManager.Instance.Inventory.AcquireItem(ref startPickaxe);
        UIManager.Instance.Inventory.AcquireItem(ref startHammer);
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
