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
                    Debug.Log("��Ҳ���");
                    FindObjectOfType<ChouJiangSceneManager>().createEndPiaoChuang("��Ҳ���", null);
                }
                else
                {

                    Debug.Log("isClicked");
                    isClicked = true;
                    Debug.Log("�۳����");
                    PlayerData.Instance.Expend(goldCost);
                    AndroidAdsDialog.Instance.UploadDataEvent("unlock_first_dangban");
                    //�۳����
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
                    FindObjectOfType<ChouJiangSceneManager>().createEndPiaoChuang("��Ҳ���", null);
                    Debug.Log("��Ҳ���");
                }
                else
                {
                    Debug.Log("isClicked");
                    isClicked = true;
                    Debug.Log("�۳����");
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
