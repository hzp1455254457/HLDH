using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;
using Spine;

public class ChouJiangSceneManager : MonoBehaviour
{
    public Canvas _uiCanvas;
    public Button choujiangbiButton,freechoujiangButton;

    public GameObject handAnimationObject;

    public GameObject ball;
    public CinemachineVirtualCamera camera;

    public Camera sceneCamera;

    public Text coinNumberText;

    public GameObject fingerControlGameObject;

    //计时器
    public int timer = 0;

    public SpriteRenderer headSpriteRender;
    public Sprite girl, boy;

    /// <summary>
    /// 金币以及水晶奖励数量
    /// </summary>
    public int rewardFactor;

    public int choujiangbiNumber
    {
        get {
            return _choujiangbiNumber;
        }
        set
        {
            _choujiangbiNumber = value;
            coinNumberText.text = "已拥有：" + _choujiangbiNumber + "枚";
            PlayerData.Instance.ChouJiangCount = _choujiangbiNumber;
        }
    }

    private int _choujiangbiNumber;

    private bool isInMiddle = true;


    //游戏中钻石和金币数额
    public int diamondNumber=0, goldNumber=0;

    //游戏中红包数额
    public float hongbaoNumber=0.0f;

    //最后是否获得加倍
    public bool isDouble = false;

    public GameObject songbiGameObject;

    private void Start()
    {
        _uiCanvas.worldCamera = CamareManager.Instance.uiCamera;
        Physics2D.IgnoreLayerCollision(0, 10);
        choujiangbiButton.onClick.AddListener(() => {

            if (isInMiddle)
            {
                
            }
            else
            {
                if (choujiangbiNumber > 0)
                {
                    choujiangbiButton.interactable = false;
                    fingerControlGameObject.SetActive(false);
                    AndroidAdsDialog.Instance.UploadDataEvent("click_use_choujiangbi");
                    choujiangbiNumber--;

                    ButtonFade();
                }
                else
                {
                    AndroidAdsDialog.Instance.UploadDataEvent("choujiangbibuzu");
                    createPiaoChuang(false, "抽奖币不足,<color=red>次日可领取</color>", new Vector2(0, 0), new Vector2(0, _uiCanvas.GetComponent<RectTransform>().sizeDelta.y / 2));
                    //创建飘窗，然后再出手
                    handAnimationObject.SetActive(true);
                }
            }
        });

        //看完广告后抽奖
        freechoujiangButton.onClick.AddListener(() => {
            AndroidAdsDialog.Instance.UploadDataEvent("show_choujiang_ad");
            AndroidAdsDialog.Instance.ShowRewardVideo("免费抽奖", () =>
            {
                fingerControlGameObject.SetActive(false);
                AndroidAdsDialog.Instance.UploadDataEvent("finish_choujiang_ad");

                ButtonFade();
                //Invoke("resetTheClock", 2.0f);
               // StartCoroutine(caculateTimer());
            });
        });

    }

    void resetTheClock()
    {
        PlayerData.Instance.chouJiangTime = 300;
        PlayerData.Instance.startChouJiangTiming();
    }

    public GameObject waitingTips;

    private bool justOnce = false;

    private void LateUpdate()
    {
        /*
        timer = PlayerDate.Instance.chouJiangTime;

        if (timer > 0)
        {
            int minute = timer / 60;
            int second = timer % 60;
            //timerImage.SetActive(true);
            //timerImage.GetComponentInChildren<Text>().text = minute + ":" + second.ToString("D2");\
            choujiangbiButton.gameObject.SetActive(false);
            freechoujiangButton.gameObject.SetActive(false);

            waitingTips.SetActive(true);
            waitingTips.GetComponentInChildren<Text>().text = "再次开启时间：" + minute + "分" + second.ToString("D2") + "秒";
            justOnce = true;
        }
        else if (timer <= 0 && justOnce == true)
        {
            justOnce = false;
            choujiangbiButton.gameObject.SetActive(true);
            freechoujiangButton.gameObject.SetActive(true);
            waitingTips.SetActive(false);
            //freechoujiangButton.interactable = true;
        }*/
    }
    /// <summary>
    /// 两个按钮淡出
    /// </summary>
    void ButtonFade()
    {
        Graphic[] graphicsList = choujiangbiButton.GetComponentsInChildren<Graphic>();

        for (int i = 0; i < graphicsList.Length; i++)
        {
            if (i == graphicsList.Length - 1)
            {
                graphicsList[i].DOFade(0, 1).onComplete = () => choujiangbiButton.gameObject.SetActive(false);
            }
            else
            {
                graphicsList[i].DOFade(0, 1);
            }
        }

        Graphic[] freeChouJiangGraphicsList = freechoujiangButton.GetComponentsInChildren<Graphic>();

        for (int i = 0; i < freeChouJiangGraphicsList.Length; i++)
        {
            if (i == freeChouJiangGraphicsList.Length - 1)
            {
                freeChouJiangGraphicsList[i].DOFade(0, 1).onComplete = () =>
                {
                    freechoujiangButton.gameObject.SetActive(false);
                    AudioManager.Instance.PlayMusic("choujiang");
                    camera.transform.DOLocalMoveY(-4.9f, 20.0f).SetUpdate(true).SetSpeedBased(true).onComplete = () =>
                    {
                        ball.transform.localPosition = new Vector3(Random.Range(-10, 10) > 0 ? 5.0f : -5.0f, 0.42f, -5f);
                        headSpriteRender.sprite = Random.Range(-1.0f, 1.0f) > 0 ? girl : boy;
                        camera.Follow = ball.transform;
                        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        //StartCoroutine(checkBallStay());
                    };

                    RectTransform t = ToggleManager.Instance.GetComponent<RectTransform>();
                    t.DOAnchorPosY(-t.sizeDelta.y - 100.0f, 1.0f);
                };
            }
            else
            {
                freeChouJiangGraphicsList[i].DOFade(0, 1);
            }
        }
    }

    public void OnEnable()
    {
        //第二日多给2枚
        if (PlayerData.Instance.day == 2 && !DataSaver.Instance.HasKey("isDayTwoGet"))
        {
            DataSaver.Instance.SetString("isDayTwoGet", "yes");
            PlayerData.Instance.ChouJiangCount += 2;
        }

        if (PlayerData.Instance.day == 7 && !DataSaver.Instance.HasKey("isDaySevenGet"))
        {
            DataSaver.Instance.SetString("isDaySevenGet", "yes");
            PlayerData.Instance.ChouJiangCount += 2;
        }

        //关闭UI
        DaimondTaskUI.Instance.Show(false);

        choujiangbiNumber = PlayerData.Instance.ChouJiangCount;

        rewardFactor = PlayerData.Instance.actorDateList.Count * (1 + Mathf.Abs(10 - choujiangbiNumber));

        timer = PlayerData.Instance.chouJiangTime;

        _uiCanvas.enabled = true;
        isInMiddle = true;

        /*
        camera.transform.DOLocalMoveY(-46.0f, 2.0f).onComplete = ()=> {
            fingerControlGameObject.gameObject.SetActive(true);
           
        };*/
        ButtonFade();
        //初始化抽奖币数量，从志鹏那边拿
        /*
        choujiangbiButton.interactable = false;
        choujiangbiButton.GetComponent<RectTransform>().DOAnchorPosX(-176, 0.8f).onComplete = () =>
        {
            isInMiddle = false;
            freechoujiangButton.gameObject.SetActive(true);
            freechoujiangButton.transform.DOScale(Vector3.one, 0.4f).onComplete = () => choujiangbiButton.interactable = true;
            songbiGameObject.gameObject.SetActive(true);
            coinNumberText.gameObject.SetActive(true);
        };
        */
    }

    public GameObject timerImage;
    IEnumerator caculateTimer()
    {
        while(timer>0)
        {
            yield return new WaitForSeconds(1.0f);
            timer--;
            int minute = timer / 60;
            int second = timer % 60;
            timerImage.SetActive(true);
            timerImage.GetComponentInChildren<Text>().text = minute + ":" + second.ToString("D2");
            PlayerData.Instance.chouJiangTime = timer;
        }

        timerImage.SetActive(false);
        freechoujiangButton.interactable = true;
    }


    public void OnDisable()
    {
        //DaimondTaskUI.Instance.Show(true);
    }

    float threshold = 0.01f;

    public Button onceAgainButton;

    public bool isDown = false;
    IEnumerator checkBallStay()
    {
        yield return new WaitForSeconds(1.0f);
        while (ball.GetComponent<Rigidbody2D>().velocity.magnitude > threshold * threshold)
        {
            yield return new WaitForSeconds(1.0f);
            if (isDown)
            {
                Debug.Log("break-----");
                break;
            }
        }

        if (!isDown)
        {
            Debug.Log("break-----2222222222");
            yield return new WaitForSeconds(3.0f);
            Debug.Log("break-----33333");
            onceAgainButton.onClick.AddListener(() => onceAgainInit());
            onceAgainButton.gameObject.SetActive(true);
        }
    }

    //再次出发按钮
    public void onceAgainInit()
    {
        /*
        onceAgainButton.gameObject.SetActive(false);
        ball.transform.localPosition = new Vector3(Random.Range(-10, 10) > 0 ? -2.85f : -2.85f, 0.01f, -5f);
        camera.Follow = ball.transform;
        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(checkBallStay());
        */
        choujiangbiNumber++;
        RectTransform t = ToggleManager.Instance.GetComponent<RectTransform>();
        t.DOAnchorPosY(0, 0.8f);
        ToggleManager.Instance.Fun4(false);
        ToggleManager.Instance.Fun4(true);
    }

    public GameObject piaochuangObject;
    /// <summary>
    /// 1-钻石，2-金币，3-红包，4-银币
    /// </summary>
    public List<Sprite> spriteList = new List<Sprite>();

    /// <summary>
    /// 创建飘窗，给水晶和金币用
    /// </summary>
    /// <param name="crystalOrGold"></param>
    /// <param name="content"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public void createPiaoChuang(bool crystalOrGold,string content,Vector2 start,Vector2 end)
    {
        GameObject obj = Instantiate(piaochuangObject,_uiCanvas.transform);
        obj.GetComponent<piaochuang>().InitPiaoChuang(spriteList[3], content, start, end);
    }

    public GameObject spriteRendererPiaoChuangObject;

    /// <summary>
    /// 创建SpriteRenderer飘窗
    /// </summary>
    /// <param name="type"></param>
    /// <param name="content"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public void createSpriteRendererPiaoChuang(int type, string content, Vector2 start, Vector2 end)
    {
        GameObject obj = Instantiate(spriteRendererPiaoChuangObject,transform);
        if (type == 1)
        {
            AudioManager.Instance.PlaySound("diamond");
            diamondNumber += int.Parse(content);
        }
        else if (type == 2)
        {
            AudioManager.Instance.PlaySound("gold");
            goldNumber += int.Parse(content);
        }
        else if (type == 3)
        {
            //红包
            AudioManager.Instance.PlaySound("gold");
            hongbaoNumber += float.Parse(content);
        }

        obj.GetComponent<spriteRenderpiaochuang>().InitSpriteRendererPiaoChuang(spriteList[type-1], "+"+content, start, end);
    }

    public GameObject hongbao;
    /// <summary>
    /// 显示红包
    /// </summary>
    public void showHongbao()
    {
        GameObject obj = Instantiate(hongbao,_uiCanvas.transform);

        if (isDouble)
        {
            diamondNumber *= 2;
            hongbaoNumber *= 2;
            goldNumber *= 2;
        }

        obj.GetComponent<choujiangHongBao>().InitHongBao(diamondNumber, hongbaoNumber, goldNumber,isDouble,_uiCanvas, ()=>
        {
            AndroidAdsDialog.Instance.UploadDataEvent("click_kaixinshouxia_inchoujiang");

            AndroidAdsDialog.Instance.ShowRewardVideo("抽奖领取红包", () =>
            {
                AndroidAdsDialog.Instance.UploadDataEvent("finish_kaixinshouxia_inchoujiang");
                AndroidAdsDialog.Instance.AddSignDataCount(2);
                //AndroidAdsDialog.Instance.CloseFeedAd();
                Destroy(obj);

                createJieSuanPiaoChuang(diamondNumber, hongbaoNumber, goldNumber,()=> {

                    RectTransform t = ToggleManager.Instance.GetComponent<RectTransform>();
                    t.DOAnchorPosY(0, 0.8f);

                    if (diamondNumber == 0 && hongbaoNumber == 0.0f && goldNumber == 0)
                    {
                        PlayerData.Instance.GetDiamond(100);
                    }
                    else
                    {
                        PlayerData.Instance.GetDiamond(diamondNumber);
                        PlayerData.Instance.GetGold(goldNumber);
                        PlayerData.Instance.GetRed((int)(hongbaoNumber * 10000));
                    }

                    if (isDouble)
                        AndroidAdsDialog.Instance.UploadDataEvent("finishwith_double_inchoujiang");
                    else
                        AndroidAdsDialog.Instance.UploadDataEvent("finishwith_normal_inchoujiang");
                    resetTheClock();
                    //ToggleManager.Instance.Fun4(false);
                    //ToggleManager.Instance.Fun4(true);
                    Destroy(gameObject);
                    tipsManager.Instance.uiTransform.GetComponent<Canvas>().enabled = true;

                    AudioManager.Instance.PlayMusic("bgm");

                });

            });
            
        }, ()=> {
            AndroidAdsDialog.Instance.ShowTableVideo("点击谢谢，播放插屏广告");
            AndroidAdsDialog.Instance.UploadDataEvent("click_noneed_inchoujiang");
            RectTransform t = ToggleManager.Instance.GetComponent<RectTransform>();
            t.DOAnchorPosY(0, 0.8f);
            resetTheClock();

            Destroy(gameObject);
            tipsManager.Instance.uiTransform.GetComponent<Canvas>().enabled = true;
            AudioManager.Instance.PlayMusic("bgm");
            //ToggleManager.Instance.Fun4(false);
            //ToggleManager.Instance.Fun4(true);
        });
    }

    public GameObject endPiaoChuang;
    /// <summary>
    /// 游戏结束飘窗，显示游戏结束
    /// </summary>
    /// <param name="content"></param>
    /// <param name="endAction"></param>
    public void createEndPiaoChuang(string content,System.Action endAction)
    {
        GameObject obj = Instantiate(endPiaoChuang, _uiCanvas.transform);
        obj.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        obj.GetComponentInChildren<Text>().text = content;
        obj.GetComponent<RectTransform>().DOAnchorPosY(_uiCanvas.GetComponent<RectTransform>().sizeDelta.y / 2, 0.8f).SetEase(Ease.OutQuint).onComplete = () =>
        {
            endAction?.Invoke();
            Destroy(obj);
        };
    }

    public GameObject diamondHongbaoGoldpiaochuang;

    /// <summary>
    /// 玩球结束飘窗
    /// </summary>
    /// <param name="number1"></param>
    /// <param name="number2"></param>
    /// <param name="number3"></param>
    /// <param name="jiesuanAction"></param>
    public void createJieSuanPiaoChuang(int number1,float number2,int number3,System.Action jiesuanAction)
    {
        GameObject obj = Instantiate(diamondHongbaoGoldpiaochuang, _uiCanvas.transform);
        obj.GetComponent<rewardPiaoChuangConfig>().InitRewardPiaoChuang(number1, number2, number3);
        obj.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        Debug.Log("结算pixelRect" + _uiCanvas.pixelRect);
        obj.GetComponent<RectTransform>().DOAnchorPosY(_uiCanvas.GetComponent<RectTransform>().sizeDelta.y/ 2, 0.8f).SetEase(Ease.OutQuint).onComplete = () =>
        {
            jiesuanAction?.Invoke();
            Destroy(obj);
        };
    }

}