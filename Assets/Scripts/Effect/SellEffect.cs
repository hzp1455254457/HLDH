using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SellEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;
    private void OnEnable()
    {
        Anim();
    }
    public void SetImage(Sprite sprite)
    {

        image.sprite = sprite;
    }
    private void Anim()
    {
        image.color = Color.white;
          //image.sprite = sprite;
          image.DOFade(0, 0.5F).SetUpdate(true);
        transform.DOMove(ToggleManager.Instance.toggles[2].transform.position, 0.5F).SetUpdate(true).onComplete+=()=>GameObjectPool.Instance.CollectObject(this.gameObject);//ศฅต๔ักฦท
    }
}
