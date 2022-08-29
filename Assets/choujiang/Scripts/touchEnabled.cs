using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchEnabled : MonoBehaviour
{
    private GameObject ball = null;
    public bool isDouble = true;
    public static bool isEnd = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "ball"&& !isEnd)
        {
            isEnd = true;
            Debug.Log(transform.name);
            ball = collision.gameObject;
            FindObjectOfType<ChouJiangSceneManager>().createEndPiaoChuang("�齱����!",showEnd);
           // Invoke("showEnd", 2.0f);
        }

    }

    public void showEnd()
    {
        ball.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        FindObjectOfType<ChouJiangSceneManager>().isDouble = isDouble;
        //�������
        FindObjectOfType<ChouJiangSceneManager>().showHongbao();
    }

    private void OnDestroy()
    {
        isEnd = false;
    }
}
