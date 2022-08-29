using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dangbanControl : MonoBehaviour
{
    private bool isFirstTime = true;
    private PhysicsMaterial2D ballMaterial;
    private float bounce, friction = 0.0f;
    public GameObject lauchSpriteButtonGameObject;
    private GameObject ballGameObject;

    public void Start()
    {
        ballGameObject = FindObjectOfType<ChouJiangSceneManager>().ball;
        ballMaterial = ballGameObject.GetComponent<CircleCollider2D>().sharedMaterial;
        bounce = ballMaterial.bounciness;
        friction = ballMaterial.friction;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFirstTime && collision.name == "ball")
        {
            isFirstTime = false;
            FindObjectOfType<ChouJiangSceneManager>().isDown = true;
            ballGameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            ballGameObject.transform.DOLocalMove(new Vector3(Random.Range(-0.1f,0.1f), -56.6f, -5.0f), 1.0f).onComplete = () =>
            {
                lauchSpriteButtonGameObject.SetActive(true);
                lauchSpriteButtonGameObject.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.1f);
            };
        }
    }
    public void BackToNormal()
    {
        ballMaterial.bounciness = bounce;
        ballMaterial.friction = friction;
    }

    private void OnDisable()
    {
        ballMaterial.bounciness = bounce;
        ballMaterial.friction = friction;
    }
}
