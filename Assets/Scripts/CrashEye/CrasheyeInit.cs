using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrasheyeInit : MonoBehaviour
{
    void Awake()
    {
        var versionNum = AndroidAdsDialog.Instance.GetVERSION_NAME();
        //Crasheye.SetAppVersion("1.1.3");
        //CrasheyeForAndroid.AddExtraData("youExtraDataKey", "youExtraDataValue");
        //CrasheyeForAndroid.SetFlushOnlyOverWiFi(false);

        CrasheyeForAndroid.SetAppVersion(versionNum);
        Debug.Log("Unity获取版本号"+versionNum);
        CrasheyeForAndroid.SetFlushOnlyOverWiFi(false);

    }

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(100, 100, 200, 100), "除零"))
    //    {

    //        Debug.Log("UnityError:" + "除零");
    //        int i = 0;
    //        int j = 1 / i;
    //    }
    //    if (GUI.Button(new Rect(100, 300, 200, 100), "空指针"))
    //    {
    //        Debug.Log("UnityError:" + "空指针");
    //        string s = null;
    //        s.CompareTo("abc");
    //    }
    //    if (GUI.Button(new Rect(100, 500, 200, 100), "数组越界"))
    //    {
    //        Debug.Log("UnityError:" + "数组越界");
    //        int[] array = new int[2];
    //        array[2] = 1;
    //    }
    //    if (GUI.Button(new Rect(100, 700, 200, 100), "主动上报异常"))
    //    {
    //        try
    //        {
    //            string s = null;
    //            s.CompareTo("abc");
    //        }
    //        catch (AndroidJavaException e)
    //        {
    //            Crasheye.SendScriptException(e);
    //        }
    //    }

    //}
}
