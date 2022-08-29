using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class crystalBallControl : MonoBehaviour
{
    public int spriteType = 1;
    private ChouJiangSceneManager _manager;
    public string content;

    private Vector3 localScale;
    // Start is called before the first frame update
    void Start()
    {
        _manager = FindObjectOfType<ChouJiangSceneManager>();
        if (content.Contains("."))
        {
            GetComponentInChildren<TextMeshPro>().text = "+" + content+ "元";
        }
        else
        {
            content = (_manager.rewardFactor * int.Parse(content)).ToString() ; 
            GetComponentInChildren<TextMeshPro>().text = "+" + content;
        }

        localScale = transform.localScale;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "ball")
        {
            AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

            Vector3 startVector3 = localScale * 0.9f;
            Vector3 endVector3 = localScale * 1.0f;

            transform.DOScale(startVector3, 0.2f).onComplete = () => transform.DOScale(endVector3, 0.2f);

            _manager.createSpriteRendererPiaoChuang(spriteType, content, transform.localPosition, transform.localPosition + new Vector3(0, 2.0f,0));

            if (content.Contains("."))
            {
                float x = float.Parse(content);
                float y = Random.Range(0.8f, 1.2f) * x;
                content = y.ToString("F2");
                GetComponentInChildren<TextMeshPro>().text = "+" + content+"元";
            }
            else
            {
                int x = int.Parse(content);
                float y = Random.Range(0.8f, 1.2f) * x;
                content = ((int)y).ToString();
                GetComponentInChildren<TextMeshPro>().text = "+" + content;
            }
            
        }
    }
}
