
using System.Collections.Generic;
using UnityEngine;

public class ProduceQiPaoManager : MonoBehaviour
{
  //  public List<ProduceQiPao> produceQiPaos;
    public List<ProduceQiPao> produceQiPaoNoDropeds;
    public List<ProduceQiPao> produceQiPaoDropedsInCars;
    public List<ProduceQiPao> produceQiPaolist;
    public static ProduceQiPaoManager Instance;
    public Transform parentTf;
    public static int jianceCount=100;
    public Transform bornTf;
    public List<DaimondFaHuo> daimondFaHuos;
    public GetDaimondManager daimondManager;

    public Sprite qipaoSprite;
    private void Awake()
    {
        Instance = this;
        bornTf = CamareManager.Instance.mainCamere.transform;
        isZiDong = true;
    }
    public void AddDaimond(DaimondFaHuo daimond)
    {
        daimondFaHuos.Add(daimond);
        //daimondManager.AddCount(daimond.count);
        if (daimondFaHuos.Count >= 20)
        {
            daimondManager.Show(true);
        }
    }
    public void RevomeDaimond(DaimondFaHuo daimond)
    {
        daimondFaHuos.Remove(daimond);
        daimondManager.RemoveCount(daimond.count);
        if (daimondFaHuos.Count <= 0)
        {
            daimondManager.Show(false);
        }
    }
    public void CleanDaimond()
    {
        int count = 0;
        foreach (var item in daimondFaHuos)
        {
            count += item.count;

         item. transform.SetParent(UIManager.Instance.canvas.transform);
            item.GetDaimond(true);
        }
        PlayerData.Instance.GetDiamond(count);
        TipsShowBase.Instance.Show("TipsShow4", ToggleManager.Instance.effectBorn, ToggleManager.Instance.effectTarget, new string[]
      {
            string.Format("+{0}",count)
          }, new Sprite[]
          {
                ResourceManager.Instance.GetSprite("钻石")
          }, null, null);
        daimondFaHuos.Clear();
        daimondManager.Show(false);
    }
    private void Start()
    {
        produceQiPaoNoDropeds = new List<ProduceQiPao>();
        produceQiPaoDropedsInCars = new List<ProduceQiPao>();
        daimondFaHuos = new List<DaimondFaHuo>();
        if (PlayerData.Instance.ProduceQiPaoList.Count > 0)
        {
            for (int i = 0; i < PlayerData.Instance.ProduceQiPaoList.Count; i++)
            {
                CreactQiPao(PlayerData.Instance.ProduceQiPaoList[i].item_have,
                  ConfigManager.Instance.GetProduce(PlayerData.Instance.ProduceQiPaoList[i].item_id),new Vector3((float)PlayerData.Instance.ProduceQiPaoList[i].x, (float)PlayerData.Instance.ProduceQiPaoList[i].y));
            }
        }
        UnityActionManager.Instance.AddAction("FaHuoEvent", ChangeDaimond);
    }
    private void CreactQiPao(int count, Produce produce,  Vector3 vector3)
    {
     //   Transform borntransform = EventFun(tipsEffectBase);
        var qipao = GameObjectPool.Instance.CreateObject("Produce", ResourceManager.Instance.GetProGo("Produce"), bornTf, Quaternion.identity,false);
        qipao.transform.position = vector3;
        qipao.transform.SetParent(bornTf);
        var pro = qipao.GetComponentInChildren<ProduceQiPao>();
        pro.SetProduce(produce, count);
        produceQiPaolist.Add(pro);
    }
    public ChiLunManager chiLunManager;
    public List<ProduceQiPao> listHuoWu;
    bool isStart = false;
    public void RecorveStatus()
    {
        isStart = false;
    }
    public void ChangeDaimond()
    //{if (isStart) return;
    //    isStart = true;
    { 
        print("执行FaHuoEvent");
        if (produceQiPaoDropedsInCars.Count > 0)
        { if (isStart) return;
            isStart = true;
          //   Physics2D.gravity = Vector2.zero;
            int count = 0;
            for (int i = 0; i < produceQiPaoNoDropeds.Count; i++)
            {
                count += CreactDimond(Random.Range(30, 51), 1, produceQiPaoNoDropeds[i].transform);

                GameObjectPool.Instance.CollectObject(produceQiPaoNoDropeds[i].transform.parent.gameObject, 0.1f);
                //RemoveProduceQiPao(produceQiPaoNoDropeds[i]);
                //CreactDimond(Random.Range(30,51), 1, produceQiPaoNoDropeds[i].transform);
                // RemoveProduceQiPao(produceQiPaoNoDropeds[i]);
            }

            daimondManager.AddCount(count);
            if (FaHuoPanel.Instance.isTips)
            {
                FaHuoPanel.Instance.HideTips();
            }

         
            ToggleManager.Instance.HideUI();
            FaHuoPanel.Instance.faHuoToggle.SetShow(false);
            FaHuoPanel.Instance.faHuoToggle.OnPointerUp1();
            // produceQiPaoDropedsInCars.Clear();
            produceQiPaoNoDropeds.Clear();
            int count1 = 0;
            for (int i = 0; i < produceQiPaoDropedsInCars.Count; i++)
            {
                produceQiPaoDropedsInCars[i].transform.parent.SetParent(FaHuoPanel.Instance.transform);
                produceQiPaoDropedsInCars[i].SetStatus();
                //  var qipao = collision.GetComponentInChildren<ProduceQiPao>();
                produceQiPaoDropedsInCars[i].SetImage(ResourceManager.Instance.GetSprite("箱子"));
                count1 += produceQiPaoDropedsInCars[i].GetValue();
            }
            //ProduceQiPao[] arry=new ProduceQiPao[produceQiPaoDropedsInCars.Count];
            //produceQiPaoDropedsInCars.CopyTo(arry);
            //listHuoWu = new List<ProduceQiPao>(arry);
            dropedInCarCollider.SetShow(false);
            chiLunManager.StartAnim(true);
            if (GuideManager.Instance.isFirstGame)
            {

                fahuoGuideCount++;

                if (fahuoGuideCount == 1)
                {
                    FaHuoPanel.Instance.faHuoGuide.GuideFuncEvent1();

                    AndroidAdsDialog.Instance.UploadDataEvent("new_guide_6");
                    FaHuoPanel.Instance.carManager.SetCount(2222);
                }
                else
                {
                    FaHuoPanel.Instance.carManager.SetCount((count1 / 2));
                }
            }
            else
            {
                //FaHuoPanel.Instance.carManager.SetZiDongFaHuoTips();//自动发货显示
                FaHuoPanel.Instance.carManager.SetCount((count1 / 2));
            }
            //  Physics2D.gravity = new Vector2(0, -10);

        }
        
        //else
        //{
           
        //}
       
    }
    int fahuoGuideCount = 0;
    public DropedInCarCollider dropedInCarCollider;
    public int CreactDimond(int count, int type,Transform posTf)
    {
           
            var go = GameObjectPool.Instance.CreateObject("DaimomdFaHuo", ResourceManager.Instance.GetProGo("DaimomdFaHuo", "Prefab/Effect/"), posTf, Quaternion.identity,false);
        //var go = Instantiate(ResourceManager.Instance.GetProGo("DaimomdFaHuo", "Prefab/Effect/"));
        go.transform.SetParent(parentTf);
        go.transform.position = posTf.position;
        go.transform.localScale = Vector3.one;
            var daimond = go.GetComponent<DaimondFaHuo>();
       
            daimond.SetCount(count);
        AddDaimond(daimond);
        //borns.SetDaimond(daimond);
        daimond.type = type;
            if (type == 1)
            {
                daimond.image.sprite = qipaoSprite;

                //daimond.type = type;

            }
            else
            {
                daimond.image.sprite = qipaoSprite;
                //daimond.type = type;
            }
            daimond.image.SetNativeSize();
        return count;

    }
    public List<QiPaoData> GetSaveQiPaos()
    {
        List<QiPaoData> list = new List<QiPaoData>();
        for (int i = 0; i < produceQiPaolist.Count; i++)
        {
            list.Add(produceQiPaolist[i].produceDate);
            list[i].x = produceQiPaolist[i].transform.position.x;
            list[i].y = produceQiPaolist[i].transform.position.y;
        }
        return list;
    }
    public void AddInCar(ProduceQiPao produceQiPao)
    {
        if (!produceQiPaoDropedsInCars.Contains(produceQiPao))
        produceQiPaoDropedsInCars.Add(produceQiPao);
    }
    public void RemoveInCar(ProduceQiPao produceQiPao)
    {if(produceQiPaoDropedsInCars.Contains(produceQiPao))
        produceQiPaoDropedsInCars.Remove(produceQiPao);
    }
    public void RemoveAllInCar()
    {
        for (int i = 0; i < produceQiPaoDropedsInCars.Count; i++)
        {
            GameObjectPool.Instance.CollectObject(produceQiPaoDropedsInCars[i].transform.parent.gameObject, 1.5f);
            CanKuPanel.AddSelledCount(produceQiPaoDropedsInCars[i].GetCount());
            // RemoveInCar(produceQiPaoDropedsInCars[i]);
        }
        for (int i = 0; i < produceQiPaoNoDropeds.Count; i++)
        {
            GameObjectPool.Instance.CollectObject(produceQiPaoNoDropeds[i].transform.parent.gameObject, 1.5f);
            //RemoveProduceQiPao(produceQiPaoNoDropeds[i]);
        }
      produceQiPaoDropedsInCars.Clear();
 produceQiPaoNoDropeds.Clear();
        isFahuo = false;
    }
    public void RemoveList()
    {
      
        
        produceQiPaoDropedsInCars.Clear();
        produceQiPaoNoDropeds.Clear();
      // produceQiPaoNoDropedsInCars.Clear();
    }
    public void Add(ProduceQiPao produceQiPao)
    {
        if (!produceQiPaolist.Contains(produceQiPao))
            produceQiPaolist.Add(produceQiPao);
        RefreshQiPaoCount();
    }
    public void Remove(ProduceQiPao produceQiPao)
    { if(produceQiPaolist.Contains(produceQiPao))
            produceQiPaolist.Remove(produceQiPao);
        RefreshQiPaoCount();
    }
  public  bool isZiDong ;
    public int zidongCount = 100;
 public   bool isFahuo = false;
    public void RefreshQiPaoCount()
    {
        //StartCoroutine(FaHuoPanel.Instance.faHuoToggle.ZiDongFaHuo());
        //if (FaHuoPanel.Instance.carManager.ziDongFaHuo.IsZiDong)
        //{

        //if (isZiDong)
        //{
        //    if (produceQiPaolist.Count >= zidongCount)
        //    {
        //        isZiDong = false;

        //        StartCoroutine(FaHuoPanel.Instance.faHuoToggle.ZiDongFaHuo());
        //    }
        //}
        //}
        //if(!isFahuo)
        //    FaHuoPanel.Instance.faHuoToggle.canvasGroup.alpha = produceQiPaolist.Count * 0.02f + 0.2f;

        FaHuoPanel.Instance.faHuoToggle.SetAlpha(produceQiPaolist.Count * 0.02f + 0.2f);
        ToggleManager.Instance.SetFaHuoValue(produceQiPaolist.Count);
        if (ToggleManager.Instance.toggles[2].isOn)
        {
            return;
        }
        if (produceQiPaolist.Count >= jianceCount)
        {
            ToggleManager.Instance.SetTips(2);
        }
        else
        {
            ToggleManager.Instance.HideTips(2);
        }

    }
    public void AddProduceQiPao(ProduceQiPao produceQiPao)
    {
        if (!produceQiPaoNoDropeds.Contains(produceQiPao))
            produceQiPaoNoDropeds.Add(produceQiPao);
        //produceQiPao.SetStatus1();
    }
    public void RemoveProduceQiPao(ProduceQiPao produceQiPao)
    {
        if (produceQiPaoNoDropeds.Contains(produceQiPao))
            produceQiPaoNoDropeds.Remove(produceQiPao);
    }
    private int GetValues(List<ProduceQiPao> produceQiPaos)
    { int count = 0;
        foreach (var item  in produceQiPaos)
        {
            count += item.GetValue();
        }
        return count;
    }
    public int GetValues()
    {
        return GetValues(produceQiPaoDropedsInCars);
    }
    
}
