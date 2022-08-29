using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaHuo : MonoBehaviour
{
    public static bool isLeftShow = false;
    public static bool isRightShow = false;
    private void Start()
    {
        //  eventQueueSystem = new EventQueueSystem();
        UnityActionManager.Instance.AddAction<string,int,Produce>("fahuo", ShowFaHuo);
    }
    public static int Maxcount = 120;
    public void ShowFaHuo(string name,int count, Produce produce)
    {
     
        if (i % 2 == 0)
        {

            //print("空闲++");
            if (!isLeftShow)
            {
                if (ProduceQiPaoManager.Instance.produceQiPaolist.Count <= Maxcount)
                {
                    //print("空闲++左展示");
                    ShowLeft(name, count, produce);
                }
                else
                {
                    EventQueueSystem.Instance.AddEvent<string, int, Produce>(ShowFaHuo, new ParameterData(name, count, produce));
                }
            }
            else
            {
                if (!isRightShow)
                {
                    if (ProduceQiPaoManager.Instance.produceQiPaolist.Count <= Maxcount)
                    {
                        ShowRight(name, count, produce);
                        //print("空闲++右展示");
                    }
                    else
                    {
                        EventQueueSystem.Instance.AddEvent<string, int, Produce>(ShowFaHuo, new ParameterData(name, count, produce));
                    }
                }
                else
                {
                    EventQueueSystem.Instance.AddEvent<string, int, Produce>(ShowFaHuo,new ParameterData(name,count,produce));
                }
            }
        }


        else
        {
            //print("空闲++");
            if (!isRightShow)
            {
                if (ProduceQiPaoManager.Instance.produceQiPaolist.Count <= Maxcount)
                {
                    //print("空闲++右展示");
                    ShowRight(name, count, produce);
                }
                else
                {
                    EventQueueSystem.Instance.AddEvent<string, int, Produce>(ShowFaHuo, new ParameterData(name, count, produce));
                }
            }
            else
            {
                if (!isLeftShow)
                {
                    if (ProduceQiPaoManager.Instance.produceQiPaolist.Count <= Maxcount)
                    {
                        ShowLeft(name, count, produce);
                        //print("空闲++左展示");
                    }
                    else
                    {
                        EventQueueSystem.Instance.AddEvent<string, int, Produce>(ShowFaHuo, new ParameterData(name, count, produce));
                    }
                }
                else
                {
                    EventQueueSystem.Instance.AddEvent<string, int, Produce>(ShowFaHuo, new ParameterData(name, count, produce));
                }
            }
        }


    }
    public Transform bornTf1, bornTf2, targetTf1, targetTf2;
    public int i = 0;
    public void ShowLeft(string name, int count, Produce produce)
    {
        i++;
        ShowToasts1(bornTf1, targetTf1, new string[] { count.ToString() + "个被",name, "卖出", }, new Sprite[] {ResourceManager.Instance.GetSprite("气泡icon"),
            ResourceManager.Instance.GetSprite(
                produce.item_pic)}, new Color[] { Color.black,Color.red }, () => {
                    CreactQiPao(count, produce,leftTipsEffectBase);
                }, 0.7f);
    }
    public void ShowRight(string name, int count, Produce produce)
    {
        i++;
        ShowToasts2(bornTf2, targetTf2, new string[] { name,"卖出了",count.ToString()+"个" },new Sprite[] {ResourceManager.Instance.GetSprite("气泡icon"), 
            ResourceManager.Instance.GetSprite(
                produce.item_pic)}, new Color[] { Color.red }, ()=> {
                    CreactQiPao(count, produce,rightTipsEffectBase);
                }, 0.7f);
    }
    private void CreactQiPao(int count,Produce produce, TipsEffectBase tipsEffectBase)
    {
       Transform  borntransform= EventFun(tipsEffectBase);
      var qipao=  GameObjectPool.Instance.CreateObject("Produce", ResourceManager.Instance.GetProGo("Produce"), borntransform, Quaternion.identity, false);
        qipao.transform.SetParent(CamareManager.Instance.mainCamere.transform);
        var pro = qipao.GetComponentInChildren<ProduceQiPao>();
        pro.SetProduce(produce, count);
        ProduceQiPaoManager.Instance.Add(pro);

    }
    public void CreactQiPao(int count, Produce produce,Transform parentTf)
    {
        
        var qipao = GameObjectPool.Instance.CreateObject("Produce", ResourceManager.Instance.GetProGo("Produce"), parentTf, Quaternion.identity, false);
        qipao.transform.SetParent(CamareManager.Instance.mainCamere.transform);
        var pro = qipao.GetComponentInChildren<ProduceQiPao>();
        pro.SetProduce(produce, count);
        ProduceQiPaoManager.Instance.Add(pro);

    }
    TipsEffectBase  leftTipsEffectBase;
    //Transform leftbornQiPaoTf;
    TipsEffectBase rightTipsEffectBase;
    //Transform rightbornQiPaoTf;
    private Transform EventFun( TipsEffectBase tipsEffectBase)
    {
        
            Global.FindChild<Image>(tipsEffectBase.transform, "img1").color = new Color32(255, 255, 255, 0);
              Global.FindChild<Image>(tipsEffectBase.transform, "img2").color = new Color32(255, 255, 255, 0);
       return Global.FindChild<Transform>(tipsEffectBase.transform, "img1");
    }
    private void ShowToasts1(Transform bornTf, Transform targetTf, string[] value, Sprite[] sprite, Color[] color, UnityEngine.Events.UnityAction unityAction = null, float scale = 1f)
    {

        var effect = GameObjectPool.Instance.CreateObject("FaHuoLeftTips", ResourceManager.Instance.GetProGo("FaHuoLeftTips"), bornTf, Quaternion.identity);
        leftTipsEffectBase = effect.GetComponent<TipsEffectBase>();
        leftTipsEffectBase.Show(targetTf, value, sprite, unityAction, color, scale);
    }
    private void ShowToasts2(Transform bornTf, Transform targetTf, string[] value, Sprite[] sprite, Color[] color, UnityEngine.Events.UnityAction unityAction = null, float scale = 1f)
    {

        var effect = GameObjectPool.Instance.CreateObject("FaHuoRightTips", ResourceManager.Instance.GetProGo("FaHuoRightTips"), bornTf, Quaternion.identity);
        rightTipsEffectBase = effect.GetComponent<TipsEffectBase>();
        rightTipsEffectBase.Show(targetTf, value, sprite, unityAction, color, scale);
    }
}
