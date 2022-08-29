using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class launchSpriteClick : MonoBehaviour
{
    private bool isClicked = false;
    public GameObject dangban;
    private GameObject ball;

    public void Start()
    {
        ball = FindObjectOfType<ChouJiangSceneManager>().ball;
        Invoke("Scale", 0.3f);
    }

    public void OnMouseDown()
    {
        Debug.Log("·¢Éä°´Å¥µã»÷");
        if (!isClicked)
        {
            FindObjectOfType<dangbanControl>().BackToNormal();
            dangban.SetActive(false);
            isClicked = true;
            ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            ball.GetComponent<Rigidbody2D>().gravityScale = 2.0f;
            Destroy(gameObject);
        }
    }

    void Scale()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
