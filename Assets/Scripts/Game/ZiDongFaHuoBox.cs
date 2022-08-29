using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ZiDongFaHuoBox : MonoBehaviour
{
    // Start is called before the first frame update
    public SkeletonGraphic fahuoAnim;
    public Transform tipsGo;
    //public SkeletonGraphic tipsAnim;
   public Transform parent;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Produce")
        {
        var qipao=    collision.GetComponentInChildren<ProduceQiPao>();
            qipao.SetImage(ResourceManager.Instance.GetSprite("箱子"));
            fahuoAnim.AnimationState.SetAnimation(0, "pengzhuangshi", false).Complete +=(s)=> fahuoAnim.AnimationState.SetAnimation(0, "jingzhi", false);
        var go=    GameObjectPool.Instance.CreateObject("tipsAnim", ResourceManager.Instance.GetProGo("tipsAnim"), parent, Quaternion.identity);
          //  Debug.LogError(tipsGo);
            go.transform.position = tipsGo.transform.position;
            go.transform.GetComponent<RectTransform>().localScale= Vector3.one * 0.5f;
       var tipsAnim=   go.GetComponent<FahuoSpine>();
            tipsAnim.Animation();
          //  tipsAnim.AnimationState.SetAnimation(0, "jingzhi", false).Complete+=s => GameObjectPool.Instance.CollectObject(go);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Produce")
        {
            var qipao = collision.GetComponentInChildren<ProduceQiPao>();
            //往大世界写入待发货数据
            BigWorldData.Instance.AddDaiFaHuo(qipao.GetValue());
            GameObjectPool.Instance.CollectObject(collision.transform.parent.gameObject);
            //PlayerDate.Instance.AddDaiFaHuoCount(1);
            
        }
    }
}
