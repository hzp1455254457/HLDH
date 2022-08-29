using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataTest : MonoBehaviour
{
    static bool isSeven = false;
    public static bool GetIsSevenRedCount()
    {
        return isSeven;
    }
    public static Queue<int> DaiFaHuo = new Queue<int>();
    public static bool IsJieSuoLunChuan, IsJieSuoFeiJi;
    public static int CarJieSuoLevel = 1;
    public static int FeiJIJieSuoLevel = 1;


    #region 测试代码

    //public void QiRiJieSuo()
    //{
    //    isSeven = true;
    //    BigWorld.Instance.LockAreaChange?.Invoke();
    //}

    //public void DaiFaHuo15()
    //{
    //    DaiFaHuo.Enqueue(11);
    //    DaiFaHuo.Enqueue(12);
    //    DaiFaHuo.Enqueue(13);
    //    DaiFaHuo.Enqueue(14);
    //    DaiFaHuo.Enqueue(15);
    //    DaiFaHuo.Enqueue(11);
    //    DaiFaHuo.Enqueue(12);
    //    DaiFaHuo.Enqueue(13);
    //    DaiFaHuo.Enqueue(14);
    //    DaiFaHuo.Enqueue(15);
    //    DaiFaHuo.Enqueue(11);
    //    DaiFaHuo.Enqueue(12);
    //    DaiFaHuo.Enqueue(13);
    //    DaiFaHuo.Enqueue(14);
    //    DaiFaHuo.Enqueue(15);
    //    BigWorld.Instance.DaFaHuoChange?.Invoke();
    //}

    //public void JieSuoLunChuan()
    //{
    //    IsJieSuoLunChuan = true;
    //    BigWorld.Instance.LockAreaChange?.Invoke();
    //    BigWorld.Instance.TransportationUnlockStateChange?.Invoke();
    //}

    //public void JieSuoFeiJi()
    //{
    //    IsJieSuoFeiJi = true;
    //    BigWorld.Instance.LockAreaChange?.Invoke();
    //    BigWorld.Instance.TransportationUnlockStateChange?.Invoke();
    //}
    //public void JieSuoQiCheUp()
    //{
    //    CarJieSuoLevel = CarJieSuoLevel + 1;
    //    BigWorld.Instance.TransportationUnlockStateChange?.Invoke();
    //}
    //public void JieSuoFeiJiUp()
    //{
    //    DataTest.FeiJIJieSuoLevel = DataTest.FeiJIJieSuoLevel + 1;
    //    BigWorld.Instance.TransportationUnlockStateChange?.Invoke();
    //}

    //#endregion

    #region 测试代码2

    public void QiRiJieSuo()
    {
        //BigWorldData.IsBigWorldUnlocked
    }

    public void DaiFaHuo15()
    {
        BigWorldData.Instance.AddDaiFaHuo(11);
        BigWorldData.Instance.AddDaiFaHuo(12);
        BigWorldData.Instance.AddDaiFaHuo(13);
        BigWorldData.Instance.AddDaiFaHuo(14);
        BigWorldData.Instance.AddDaiFaHuo(15);
        BigWorldData.Instance.AddDaiFaHuo(11);
        BigWorldData.Instance.AddDaiFaHuo(12);
        BigWorldData.Instance.AddDaiFaHuo(13);
        BigWorldData.Instance.AddDaiFaHuo(14);
        BigWorldData.Instance.AddDaiFaHuo(15);
        BigWorldData.Instance.AddDaiFaHuo(11);
        BigWorldData.Instance.AddDaiFaHuo(12);
        BigWorldData.Instance.AddDaiFaHuo(13);
        BigWorldData.Instance.AddDaiFaHuo(14);
        BigWorldData.Instance.AddDaiFaHuo(15);

        BigWorld.Instance.DaFaHuoChange?.Invoke();
    }

    public void JieSuoLunChuan()
    {
       //BigWorldData.Instance. IsJieSuoLunChuan = true;
        //BigWorld.Instance.LockAreaChange?.Invoke();
        //BigWorld.Instance.TransportationUnlockStateChange?.Invoke();
    }

    public void JieSuoFeiJi()
    {
        //BigWorldData.Instance.IsJieSuoFeiJi = true;
        //IsJieSuoFeiJi = true;
        //BigWorld.Instance.LockAreaChange?.Invoke();
        //BigWorld.Instance.TransportationUnlockStateChange?.Invoke();
    }
    public void JieSuoQiCheUp()
    {
        //BigWorldData.Instance.CarJieSuoLevel += 1;
        //CarJieSuoLevel = CarJieSuoLevel + 1;
        //BigWorld.Instance.TransportationUnlockStateChange?.Invoke();
    }
    public void JieSuoFeiJiUp()
    {
        //BigWorldData.Instance.FeiJIJieSuoLevel += 1;
        //DataTest.FeiJIJieSuoLevel = DataTest.FeiJIJieSuoLevel + 1;
        //BigWorld.Instance.TransportationUnlockStateChange?.Invoke();
    }

    #endregion
    //public void OnGUI2()
    //{
    //    if (GUILayout.Button("七日解锁"))
    //    {
    //        isSeven = true;
    //        BigWorld.Instance.LockAreaChange?.Invoke();
    //    }
    //    if (GUILayout.Button("待发货数量+15"))
    //    {
    //        //PlayerDate.Instance.Datas3D.DaiFaHuo.Enqueue(11);
    //        //PlayerDate.Instance.Datas3D.DaiFaHuo.Enqueue(12);
    //        //PlayerDate.Instance.Datas3D.DaiFaHuo.Enqueue(13);
    //        //PlayerDate.Instance.Datas3D.DaiFaHuo.Enqueue(14);
    //        //PlayerDate.Instance.Datas3D.DaiFaHuo.Enqueue(15);
    //        DaiFaHuo.Enqueue(11);
    //        DaiFaHuo.Enqueue(12);
    //        DaiFaHuo.Enqueue(13);
    //        DaiFaHuo.Enqueue(14);
    //        DaiFaHuo.Enqueue(15);
    //        DaiFaHuo.Enqueue(11);
    //        DaiFaHuo.Enqueue(12);
    //        DaiFaHuo.Enqueue(13);
    //        DaiFaHuo.Enqueue(14);
    //        DaiFaHuo.Enqueue(15);
    //        DaiFaHuo.Enqueue(11);
    //        DaiFaHuo.Enqueue(12);
    //        DaiFaHuo.Enqueue(13);
    //        DaiFaHuo.Enqueue(14);
    //        DaiFaHuo.Enqueue(15);
    //        BigWorld.Instance.DaFaHuoChange?.Invoke();
    //    }
    //    if (GUILayout.Button("解锁轮船"))
    //    {
    //        //PlayerDate.Instance.Datas3D.IsJieSuoLunChuan = true;
    //        IsJieSuoLunChuan = true;
    //        BigWorld.Instance.LockAreaChange?.Invoke();
    //        BigWorld.Instance.TransportationUnlockStateChange?.Invoke();

    //    }
    //    if (GUILayout.Button("解锁飞机"))
    //    {
    //        //PlayerDate.Instance.Datas3D.IsJieSuoFeiJi = true;
    //        IsJieSuoFeiJi = true;
    //        BigWorld.Instance.LockAreaChange?.Invoke();
    //        BigWorld.Instance.TransportationUnlockStateChange?.Invoke();
    //    }

    //    if (GUILayout.Button("解锁汽车等级+1"))
    //    {
    //        //PlayerDate.Instance.Datas3D.CarJieSuoLevel = PlayerDate.Instance.Datas3D.CarJieSuoLevel + 1;
    //        CarJieSuoLevel = CarJieSuoLevel + 1;
    //        BigWorld.Instance.TransportationUnlockStateChange?.Invoke();
    //    }
    //    if (GUILayout.Button("解锁飞机等级+1"))
    //    {
    //        //PlayerDate.Instance.Datas3D.CarJieSuoLevel = PlayerDate.Instance.Datas3D.CarJieSuoLevel + 1;
    //        DataTest.FeiJIJieSuoLevel = DataTest.FeiJIJieSuoLevel + 1;
    //        BigWorld.Instance.TransportationUnlockStateChange?.Invoke();
    //    }

    //}
    #endregion
}