using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class spriteRenderpiaochuang : MonoBehaviour
{
    public TextMeshPro _textMesh;
    public SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    public void InitSpriteRendererPiaoChuang(Sprite g, string str, Vector2 start, Vector2 end)
    {
        _spriteRenderer.sprite = g;
        _textMesh.text = str;
        transform.localPosition = start;
        transform.DOLocalMove(end, 1.0f).onComplete = () => {
            Destroy(gameObject);
        };
    }
}
