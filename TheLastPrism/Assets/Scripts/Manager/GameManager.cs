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

    public GameObject ItemPrefab => itemPrefab;

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
    }
}
