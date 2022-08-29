using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using UnityEngine.U2D;

public class ResourceManager : MonoSingleton<ResourceManager>
{
 public   List<GameObject> textList=new List<GameObject>();
    Dictionary<string ,Sprite> produceDic=new Dictionary<string, Sprite>();
    Transform effectPos;
    public List<SkeletonDataAsset> animatorList;
    Dictionary<string, GameObject> proGoDic = new Dictionary<string, GameObject>();

  
    public void LoadGame(Game game)
    {
        //textList = game.textList;
        //effectPos = game.effectPos;
        animatorList = game.animatorList;
    }
    SpriteAtlas atlas01;
   // SpriteAtlas atlas02;
    public override void Init()
    {
        base.Init();
        atlas01 = Resources.Load<SpriteAtlas>("ProduceAtlas");
       // atlas02 = Resources.Load<SpriteAtlas>("Other");
    }
    public GameObject GetDimondEffect(Transform parentTf)
    {
      var effect= textList.Find(s => s.activeSelf==false);
        if (effect == null)
        {
      effect=   Instantiate<GameObject>( Resources.Load<GameObject>("Prefab/Effect/Daimomd"), parentTf);
            
            effect.transform.localPosition = Vector3.zero;
            effect.transform.localScale = Vector3.one;
            effect.transform.localRotation =Quaternion.Euler( Vector3.zero);
            textList.Add(effect);
        }
        effect.SetActive(true);
        return effect;
    }
    public void RecoveryDimondEffect(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(this.transform);
    }


    public Sprite GetSprite(string spriteName)
    {
        if (!produceDic.ContainsKey(spriteName))
        {
            //var sprite = Resources.Load<Sprite>("Sprite/" + spriteName);
           // produceDic.Add(spriteName, sprite);
            var sprite = atlas01.GetSprite(spriteName);
            //if (sprite == null)
            //{
            //    sprite = atlas02.GetSprite(spriteName);
            //}
           produceDic.Add(spriteName, sprite);
        }
        else
        {
        
        }
        return produceDic[spriteName];
    }
    public GameObject GetProGo(string goName,string topName= "Prefab/")
    {
        if (!proGoDic.ContainsKey(goName))
        {
            var sprite = Resources.Load<GameObject>(topName + goName);
            proGoDic.Add(goName, sprite);
        }
        else
        {

        }
        return proGoDic[goName];
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index">1ÊÇÄÐ£¬0ÊÇÅ®</param>
    /// <returns></returns>
    public SkeletonDataAsset GetSkeleton(int index)
    {

        return animatorList[index];


    }
    public void LoadRed(int value)
    {
      Instantiate( GetProGo("hongbao7")).GetComponent<CommonRed>();
    }
    public void ShowGuideRed1(UnityEngine.Events.UnityAction unityAction)
    {
        Instantiate(GetProGo("GuideRed1")).GetComponent<GuideRed1>().Show(unityAction);
    }
    public void ShowGuideRed2(int count,UnityEngine.Events.UnityAction unityAction)
    {
        Instantiate(GetProGo("GuideRed2")).GetComponent<GuideRed2>().Show(count,unityAction);
    }
}
