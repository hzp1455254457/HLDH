using System;
using UnityEngine;


public class BigWorld : MonoBehaviour
{


    public static BigWorld Instance;

    public Action DaFaHuoChange, TransportationUnlockStateChange, LockAreaChange;
    public Action<TransportationType> TransportationBack;

    //�̳������ֻ�е�һ����ʾ
    public GameObject ToturialCamera;
    public GameObject MainCamera;




    #region ��Ҫ�л��Ķ���
    public GameObject[] HideObjects;
    #endregion
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        
    }
    //private void Start()
    //{
    //    //SceneManager.SetActiveScene(SceneManager.GetSceneByName("BigWorld"));
    //    //Debug.Log("BigWorld����Start"+IsActive);
    //    //ToogleObjects();
    //}

    /// <summary>
    /// ǰ��������
    /// </summary>
   public void GoBigWorld()
    {
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("BigWorld"));
        //Debug.Log("�����糡��Start");
        ToogleObjects(true);
        //��������ֽ̳̣����������
        if (GuideManager.Instance.isFirstGame)
        {
            ToturialCamera.SetActive(true);
            MainCamera.SetActive(false);
        }
    }
    /// <summary>
    /// ǰ��������
    /// </summary>
    public void GoGame()
    {
        //���������������
        ToturialCamera.SetActive(false);
        //Debug.Log("�л���������");
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
        //��������
        ToogleObjects(false);
        ToggleManager.Instance.ShowUI();
        UIManager.Instance.SetUIStates(true);

    }
    void ToogleObjects(bool tf)
    {
        foreach (var item in HideObjects)
        {
            //item.SetActive(IsActive);
            item.SetActive(tf);
        }
    }

    /// <summary>
    /// �жϵ�ǰ�Ƿ��Ǵ����糡��
    /// </summary>
    //public bool IsActive
    //{
    //    get
    //    {
    //        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("BigWorld"))
    //        {
    //            return false;
    //        }
    //        return true;
    //    }
    //}
}
