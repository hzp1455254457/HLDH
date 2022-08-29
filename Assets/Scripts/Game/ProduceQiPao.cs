using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProduceQiPao : MonoBehaviour
{
    public TextMeshPro  textMeshPro;
    public SpriteRenderer sprite;
    public SpriteRenderer spriteQiPao;
    public Transform produceBack;
    Produce currentProduce;
   // int count;
 public   Rigidbody2D _rigidbody2D;
    BoxCollider2D _collider2D;
    public QiPaoData produceDate;
    public Transform canvaTf;
    public Transform targer;
    public bool isInCar;
    public void SetProduce(Produce produce, int value)
    {
        currentProduce = produce;
        this.sprite.sprite = ResourceManager.Instance.GetSprite(produce.item_pic);
        textMeshPro.text =string.Format("{0}¸ö",value);
        produceDate.item_have = value;
        produceDate.item_id = produce.item_id;
        spriteQiPao.enabled = true;
        RecoverPos();
        _rigidbody2D.gravityScale = 1;

    }
    public void SetImage(Sprite sprite)
    {
        this.sprite.sprite = sprite;
        textMeshPro.gameObject.SetActive(false);
        spriteQiPao.enabled = false;
    }
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<BoxCollider2D>();
    }
    public void SetStatus()
    {
        gameObject.layer = 8;
        textMeshPro.transform.gameObject.SetActive(false);
        _rigidbody2D.gravityScale = 1;
        isInCar = true;
        // _collider2D.isTrigger = RigidbodyType2D.Kinematic;
    }
    public void SetStatus1()
    {
        gameObject.layer = 15;
    }
  public   void RecoverPos()
    {
        textMeshPro.transform.gameObject.SetActive(true);
        gameObject.layer = 7;
        //_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        produceBack.localPosition = Vector3.zero;
        produceBack.localEulerAngles = Vector3.zero;
        textMeshPro.transform.localPosition= new Vector3 (0,0.55F,0);
        isInCar = false;
    }
    private void Update()
    {
        Vector3 vector = new Vector3(targer.localPosition.x, targer.localPosition.y + 0.64f, targer.localPosition.z);
        canvaTf.localPosition = vector;
    }
    public int GetValue()
    {
        return (int)(currentProduce.item_profit * produceDate.item_have);
        
    }
    public int GetCount()
    {
        return produceDate.item_have;
    }

}
