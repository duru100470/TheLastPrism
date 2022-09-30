using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCrackEffect : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> crackSprites;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
    }

    public void UpdateSprite(int _index)
    {
        switch (_index)
        {
            case 0:
                spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
                break;
            default:
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                spriteRenderer.sprite = crackSprites[_index - 1];
                break;
        }
    }
}
