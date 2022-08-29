using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class FahuoDanBan : MonoBehaviour
{
    public SkeletonGraphic skeletonGraphic;
   public void SetShowOrHide(bool value)
    {
       gameObject.SetActive(value);
        //if (value)
        //{
        //    isAchive = false;
        //}
    }
    int i = 0;
    bool isAchive = false;
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag=="Produce"){

    //        Debug.LogError("碰撞体："+collision.name);
    //        //fahuoAnim.AnimationState.SetAnimation(0, "pengzhuangshi", false).Complete += (s) => fahuoAnim.AnimationState.SetAnimation(0, "jingzhi", false);
    //        //var go = GameObjectPool.Instance.CreateObject("tipsAnim", ResourceManager.Instance.GetProGo("tipsAnim"), parent, Quaternion.identity);
    //        ////  Debug.LogError(tipsGo);
    //        //go.transform.position = tipsGo.transform.position;
    //        //go.transform.GetComponent<RectTransform>().localScale = Vector3.one * 0.5f;
    //        //var tipsAnim = go.GetComponent<FahuoSpine>();
    //        //tipsAnim.Animation();
    //        var qipao = collision.GetComponentInChildren<ProduceQiPao>();
          
    //        ProduceQiPaoManager.Instance.RemoveInCar(qipao);

    //        //ProduceQiPaoManager.Instance.listHuoWu.Remove(qipao);
    //        //往大世界写入待发货数据
    //        //BigWorldData.Instance.AddDaiFaHuo(qipao.GetValue());
    //      //  Destroy(collision.transform.parent.gameObject);
    //       GameObjectPool.Instance.CollectObject(collision.transform.parent.gameObject);
    //        qipao.RecoverPos();
    //        skeletonGraphic.AnimationState.SetAnimation(0, "pengzhuang", false);
    //        AudioManager.Instance.PlaySound("item_out");
    //        Debug.LogError(ProduceQiPaoManager.Instance.produceQiPaoDropedsInCars.Count+"___"+Time.time);
    //        if (ProduceQiPaoManager.Instance.produceQiPaoDropedsInCars.Count <= 0)
    //        {
    //         //ProduceQiPaoManager.Instance.RemoveAllInCar();
    //            Debug.LogError("FAHUOS");
    //            i++;
    //            //Debug.LogError(i);
    //            skeletonGraphic.AnimationState.SetAnimation(0, "showhb", false).Complete+=s=> {
    //                skeletonGraphic.AnimationState.SetAnimation(0, "daiji", false);
                 
    //                    StartCoroutine(Global.Delay(0.5f, Fun));


    //            };
              
              
               
               
                
                

    //        }
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.collider.tag == "Produce"){

            //Debug.LogError("碰撞体：" + collision.collider.GetComponent<Transform>().parent.name);
            //fahuoAnim.AnimationState.SetAnimation(0, "pengzhuangshi", false).Complete += (s) => fahuoAnim.AnimationState.SetAnimation(0, "jingzhi", false);
            //var go = GameObjectPool.Instance.CreateObject("tipsAnim", ResourceManager.Instance.GetProGo("tipsAnim"), parent, Quaternion.identity);
            ////  Debug.LogError(tipsGo);
            //go.transform.position = tipsGo.transform.position;
            //go.transform.GetComponent<RectTransform>().localScale = Vector3.one * 0.5f;
            //var tipsAnim = go.GetComponent<FahuoSpine>();
            //tipsAnim.Animation();
            var qipao = collision.collider.GetComponentInChildren<ProduceQiPao>();

            ProduceQiPaoManager.Instance.RemoveInCar(qipao);

            //ProduceQiPaoManager.Instance.listHuoWu.Remove(qipao);
            //往大世界写入待发货数据
            //BigWorldData.Instance.AddDaiFaHuo(qipao.GetValue());
            //  Destroy(collision.transform.parent.gameObject);
            GameObjectPool.Instance.CollectObject(collision.transform.parent.gameObject);
            //qipao.RecoverPos();
            skeletonGraphic.AnimationState.SetAnimation(0, "pengzhuang", false);
            AudioManager.Instance.PlaySound("item_out");
            //Debug.LogError(ProduceQiPaoManager.Instance.produceQiPaoDropedsInCars.Count + "___" + Time.time);
            if (ProduceQiPaoManager.Instance.produceQiPaoDropedsInCars.Count <= 0 )
            {
                //ProduceQiPaoManager.Instance.RemoveAllInCar();
                //isAchive = true;
                //Debug.LogError("FAHUOS");
               // i++;
                //Debug.LogError(i);
                ProduceQiPaoManager.Instance.RecorveStatus();
                skeletonGraphic.AnimationState.SetAnimation(0, "showhb", false).Complete += s => {
                    skeletonGraphic.AnimationState.SetAnimation(0, "daiji", false);

                    StartCoroutine(Global.Delay(0.5f, Fun));


                };







            }
        }
    }
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.LogError("11111");
    //}
        private static void Fun()
    {
        FaHuoPanel.Instance.carManager.GetGoldBtnEvent();
        FaHuoPanel.Instance.faHuoToggle.SetShow(true);
        ToggleManager.Instance.ShowUI();
        ProduceQiPaoManager.Instance.chiLunManager.StartAnim(false);
    }
}
