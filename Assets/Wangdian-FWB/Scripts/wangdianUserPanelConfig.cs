using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class wangdianUserPanelConfig : MonoBehaviour
{
    [Header("�û�ͷ��")]
    public Image userHeadImage;

    [Header("�û�����")]
    public Text userNameText;

    [Header("�̵�ȼ�image")]
    public Image storeLevelImage;

    [Header("�̵�ȼ���������")]
    public Text storeLevelText;

    [Header("�콱��ť")]
    public Button lingquButton;

    public List<Sprite> storeLevelImageList = new List<Sprite>();

    public List<GameObject> storeLeveldiamondList = new List<GameObject>();

    [Header("�̵���ʯ������Transform")]
    public Transform storeLevelDiamondPanelTransform;

    private string userName;
    private Coroutine getUserHeadImageCoroutine = null;

    public int levelGrade
    {
        get {
           return _levelGrade;
        }
        set {
            _levelGrade = value;
            levelGradeAction?.Invoke(_levelGrade);
        }
    }
    private int _levelGrade;

    public Action<int> levelGradeAction = null;
    public void OnEnable()
    {
        if (!userData.Instance.dataInitialed)
            userData.Instance.InitData();

        userData.Instance.xinyuAction = refreshXinyu;
        setUserImageAndLevel();
    }

    IEnumerator getUserHeadImage()
    {
        Sprite sprite = JavaCallUnity.Instance.GetWangDianSpriteAndName(out userName);
        yield return new WaitUntil(() =>(sprite!=null)&&!string.IsNullOrEmpty(userName));
        userHeadImage.sprite = sprite;
        userNameText.text = userName + "������";
    }

    public void OnDisable()
    {
        if (getUserHeadImageCoroutine != null)
        {
            StopCoroutine(getUserHeadImageCoroutine);
            getUserHeadImageCoroutine = null;
        }
    }

    public void Start()
    {
        
    }
    public GameObject fingerAnimation;
    /// <summary>
    /// ��ʼ���û��ȼ�UI
    /// </summary>
    void setUserImageAndLevel()
    {
        lingquButton.onClick.AddListener(() => {
            if(fingerAnimation.activeSelf)
            fingerAnimation.SetActive(false);
            Debug.Log("�����ȡ");
            tipsManager.Instance.openLingQuDialog(() => tipsManager.Instance.createTips(() =>
            {
                lingquDialogConfig t = FindObjectOfType<lingquDialogConfig>();
                if (t != null)
                {
                    Destroy(t.transform.gameObject);
                }
            })) ;
            });

        getUserHeadImageCoroutine = StartCoroutine(getUserHeadImage());
        
        refreshXinyu(0,userData.Instance.xinyu);
    }

    private List<GameObject> diamondList = new List<GameObject>();
    private Tweener scaleTweener = null;
    void refreshXinyu(int startNum,int num)
    {
        int currentValve = startNum;

        if (scaleTweener != null)
        {
            scaleTweener.Kill();
        }

        scaleTweener = storeLevelText.transform.DOScale(Vector3.one * 1.8f, 0.8f).SetAutoKill(false);

        scaleTweener.onComplete =
            ()=> {
                Tweener rollTweener = DOTween.To(() => currentValve, (x) => currentValve = x, num, 1.0f).SetUpdate(true);
                rollTweener.onUpdate = () =>
                {
                    for (int j = 0; j < diamondList.Count; j++)
                    {
                        Destroy(diamondList[j]);
                    }

                    diamondList.Clear();

                    Debug.Log("����ֵ:" + currentValve);
                    int xinyu = currentValve;
                    int grade = 0;
                    float progress;
                    int currentTarget;
                    xinyuCheck(xinyu, out grade, out progress, out currentTarget);
                    storeLevelImage.sprite = storeLevelImageList[grade / 2];

                    int lightNumber = (int)(progress / 0.2f);
                    Debug.Log("lightNumber:" + lightNumber);

                    for (int i = 0; i < 5; i++)
                    {
                        GameObject obj = Instantiate(storeLeveldiamondList[grade - 1], storeLevelDiamondPanelTransform);
                        if (i >= lightNumber)
                        {
                            obj.GetComponentInChildren<Shadow>().GetComponent<Image>().fillAmount = 0.0f;
                        }
                        diamondList.Add(obj);
                    }

                    levelGrade = grade;

                    storeLevelText.text = currentValve + "/" + currentTarget;
                };

                rollTweener.onComplete = () =>
                {
                    storeLevelText.transform.DOScale(Vector3.one, 0.8f).onComplete = () =>
                    {
                        scaleTweener.Kill();
                    };
                };
            } ;

        scaleTweener.onKill = () =>
        {
            currentValve = num;

            for (int j = 0; j < diamondList.Count; j++)
            {
                Destroy(diamondList[j]);
            }

            diamondList.Clear();

            Debug.Log("����ֵ:" + currentValve);
            int xinyu = currentValve;
            int grade = 0;
            float progress;
            int currentTarget;
            xinyuCheck(xinyu, out grade, out progress, out currentTarget);
            storeLevelImage.sprite = storeLevelImageList[grade / 2];

            int lightNumber = (int)(progress / 0.2f);
            Debug.Log("lightNumber:" + lightNumber);

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(storeLeveldiamondList[grade - 1], storeLevelDiamondPanelTransform);
                if (i >= lightNumber)
                {
                    obj.GetComponentInChildren<Shadow>().GetComponent<Image>().fillAmount = 0.0f;
                }
                diamondList.Add(obj);
            }

            levelGrade = grade;
            storeLevelText.text = currentValve + "/" + currentTarget;
        };

    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Debug.Log("��������ʱ");
        }
        else
        {
            Debug.Log("��ͣ����ʱ");
        }
    }

    public int level1, level2, level3, level4, level5;
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="x"></param>
    /// <param name="level"></param>
    /// <param name="progress"></param>
    /// <param name="target"></param>
    private void xinyuCheck(int x, out int level,out float progress,out int target)
    {
        if (x <= level1)
        {
            level = 1;
            progress = x / (float)level1;
            Debug.Log("progress:"+progress);
            target = level1;
        }
        else if (x <= level2)
        {
            level = 2;
            progress = x/ (float)level2;
            target = level2;
        }
        else if (x <= level3)
        {
            level = 3;
            progress = x/ (float)level3;
            target = level3;
        }
        else if (x <= level4)
        {
            level = 4;
            progress = x / (float)level4;
            target = level4;
        }
        else if (x <= level5)
        {
            level = 5;
            progress = x/ (float)level5;
            target = level5;
        }
        else
        {
            level = 6;
            progress = 1;
            target = level5;
            //�����ȼ��ﵽ��ߣ��Ͳ�������
            storeLevelText.gameObject.SetActive(false);
        }
    }
}
