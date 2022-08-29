using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class spriteClick : MonoBehaviour
{
    public SpriteRenderer block;
    private bool isClicked = false;
    public int goldCost = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<TextMeshPro>().text = goldCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if (!isClicked)
        {
            Collider2D d = block.GetComponent<Collider2D>();
            if (!d.enabled)
            {

                if (goldCost > PlayerData.Instance.gold)
                {
                    Debug.Log("½ð±Ò²»×ã");
                    FindObjectOfType<ChouJiangSceneManager>().createEndPiaoChuang("½ð±Ò²»×ã", null);
                }
                else
                {

                    Debug.Log("isClicked");
                    isClicked = true;
                    Debug.Log("¿Û³ý½ð±Ò");
                    PlayerData.Instance.Expend(goldCost);
                    AndroidAdsDialog.Instance.UploadDataEvent("unlock_first_dangban");
                    //¿Û³ý½ð±Ò
                    d.enabled = true;
                    //d.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    d.transform.DOScale(new Vector3(2.0f,2.0f,2.0f), 0.1f);
                    transform.parent.DOScale(Vector3.zero, 1.0f).onComplete = () => Destroy(transform.parent.gameObject);
                }
            }
            else
            {
                if (goldCost > PlayerData.Instance.gold)
                {
                    FindObjectOfType<ChouJiangSceneManager>().createEndPiaoChuang("½ð±Ò²»×ã", null);
                    Debug.Log("½ð±Ò²»×ã");
                }
                else
                {
                    Debug.Log("isClicked");
                    isClicked = true;
                    Debug.Log("¿Û³ý½ð±Ò");
                    PlayerData.Instance.Expend(goldCost);
                    AndroidAdsDialog.Instance.UploadDataEvent("destory_second_dangban");
                    d.enabled = false;
                    block.transform.DOScale(Vector3.zero, 1.0f).onComplete = () => Destroy(block);
                    transform.parent.DOScale(Vector3.zero, 1.0f).onComplete = () => Destroy(transform.parent.gameObject);
                }
            }
        }
    }
}
