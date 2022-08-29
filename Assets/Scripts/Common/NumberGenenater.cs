using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGenenater
{
    // Start is called before the first frame update
  public static int GetDaimondCount(int level,out int type  )
    {
       int num = Random.Range(1, 11);
        type = num <=0 ? 2 : 1;
        return num <= 0 ? (int)(Random.Range(100, 201)) : (int)(Random.Range(20, 61));
    }
    public static int GetDaimondCount( out int type)
    {
        int num = Random.Range(1, 11);
        type = num <= 1 ? 2 : 1;
        return num <= 1 ? (int)(Random.Range(100, 200)) : (int)(Random.Range(20, 61));
    }
    public static int GetProduceCount()
    {
       return Random.Range(40000, 70000);
    }
    public static int GetProduceCount(bool isRed)
    {
        return Random.Range(18000, 30000);
    }
    public static int GetRedCount()
    {
        float scale = 0.3f;
        if (PlayerData.Instance._videoClickRedAdwrdCount <= 4)
        {
            scale = 1;
        }
        if (AndroidAdsDialog.Instance.isBroadcast)
        {
          
            return (int)(JavaCallUnity.Instance.jiliEcpm * scale * 100);
        }
        else {
            if (JavaCallUnity.Instance.IsNewUser)
            {
                JavaCallUnity.Instance.IsNewUser = false;
                return 1500;
            }
            else
            {
                return (int)(5000*scale);
            }
        }

        //if (PlayerDate.Instance.red >= 0 && PlayerDate.Instance.red <= 3000)
        //    {
        //        return Random.Range(210, 230);

        //    }
        //    else
        //    {
        //        if (AndroidAdsDialog.Instance.GetFirstECPM() >= 80)
        //        {
        //            if (PlayerDate.Instance.red > 3000 && PlayerDate.Instance.red <= 5000)
        //            {
        //                return Random.Range(1000, 1500);
        //            }
        //            else if(PlayerDate.Instance.red > 5000 && PlayerDate.Instance.red <= 200000)
        //            {
        //                return Random.Range(4000, 8000);
        //            }
        //            else
        //            {
        //                return Random.Range(1000, 3000);
        //            }
        //        }
        //        else
        //        {
        //            if (PlayerDate.Instance.red > 3000 && PlayerDate.Instance.red <= 5000)
        //            {
        //                return Random.Range(600, 800);
        //            }
        //            else if (PlayerDate.Instance.red > 5000 && PlayerDate.Instance.red <= 200000)
        //            {
        //                return Random.Range(2000, 3000
        //                      );
        //            }
        //            else
        //            {
        //                return Random.Range(1000, 3000);
        //            }
        //        }
        //    }
        //}
    }
        public static int GetRedCount(bool isShop)
    {
        //if(PlayerDate.Instance.red <= 289900)
            return Random.Range(5, 13);
        //return 0;
        
    }
    public static int GetRedCount1()
    {
        //if(PlayerDate.Instance.red <= 289900)
        return Random.Range(3, 6)*(int)MoneyManager.redProportion;
        //return 0;

    }
    /// <summary>
    /// 获取进度值
    /// </summary>
    /// <param name="value">ecpm</param>
    /// <param name="value1">挡位金额</param>
    /// <param name="value2">当前进度值</param>
    /// <returns></returns>
    public static float GetTiXianValue(int value,float value1,float value2)
    {
       if (JavaCallUnity.Instance.IsFirstGetRedValue())
            
        {
            if(AndroidAdsDialog.Instance. GetFirstECPM()>=65)
            return 0.5f;
            else
            {
                return 0.2f;
            }
        }
        
        else
        {
            float baseValue = 0f;
            if (AndroidAdsDialog.Instance.GetFirstECPM() >= 65)
            {
                baseValue = 0.3f;
              
            }
            else
            {
                baseValue = 0.2f;
            }
            if (value1 < 0.5f)
            {
                if (value <= 10)
                {
                    return 0.001f;
                }
                else
                {
                    return value / 1000f * baseValue / value1 ;
                }
            }
            else
            {
                if (value2 >= 0.999f)
                {
                    return 0f;
                }
                else
                {
                    return 0.0001f;
                }
            }
        }
    }
    public static float GetTiXianValue1(int ecpm, float dangwei,bool isChaPing=false)
    {
        
            float value = 0;
            float value1 = 0.1f;
      
            if (AndroidAdsDialog.Instance.GetFirstECPM() >= 65)
            {
                value1 = value1 * 2;
            }
        if (dangwei == 0.3f)
        {
            value = (ecpm / 1000f * value1 * 10000) / 3000f;

            if (PlayerData.Instance.TiXianCount == 0)//如果还没提现
            {
                if (PlayerData.Instance.FirstTableEcpm <= 20 && PlayerData.Instance.FirstTableEcpm > 0)//如果获得到了ecpm
                {
                    if (isChaPing)//插屏
                    {
                       value = GetValue(dangwei);
                    }
                    else
                    {
                        if (PlayerData.Instance.TixianValues[0] <= 0.8)
                        {
                           
                        }
                        else
                        {
                            value *= 0.2f;
                        }
                    }
                }
                else if (PlayerData.Instance.FirstTableEcpm > 20)
                {
                    if (PlayerData.Instance.AddTiXIanCount_Table <= 5)
                    {
                        if (isChaPing)
                        {
                            value = GetValue(dangwei) * 2;
                        }
                        else
                        {
                           // value = GetValue2(value);
                        }

                    }

                }
            }


        }
        else if (dangwei == 0.5f)
        {
            value = (ecpm / 1000f * value1 * 10000) / 5000f;
            if (PlayerData.Instance.TiXianCount == 0)
            {
                if (PlayerData.Instance.FirstTableEcpm <= 20 && PlayerData.Instance.FirstTableEcpm > 0)
                {
                    if (isChaPing)
                    {
                        value = GetValue(dangwei);
                    }
                    else
                    {
                        if (PlayerData.Instance.TixianValues[0] <= 0.8)
                        {

                        }
                        else
                        {
                            value *= 0.2f;
                        }
                    }
                }
            }
            else if (PlayerData.Instance.FirstTableEcpm > 20)
            {
                if (PlayerData.Instance.AddTiXIanCount_Table <= 5)
                {
                    if (isChaPing)
                    {
                        value = GetValue(dangwei) * 2;
                    }
                    else
                    {
                      //  value = GetValue2(value);
                    }
                }

            }
            }
            else
            {
                if (PlayerData.Instance.TixianValues[2] < 0.9f)
                    value = (ecpm / 1000f * value1 * 10000) / 50000f;
                else if (PlayerData.Instance.TixianValues[2] >= 0.9f && PlayerData.Instance.TixianValues[2] < 0.9995f)
                {
                    value = 0.0001f;
                }
                else
                {
                    value = 0;
                }
                if (PlayerData.Instance.TiXianCount == 0)
                {
                    if (PlayerData.Instance.FirstTableEcpm <= 20 && PlayerData.Instance.FirstTableEcpm > 0)
                    {
                        if (isChaPing)
                        {
                        value = GetValue(dangwei);
                    }
                        else
                        {
                            if (PlayerData.Instance.TixianValues[0] <= 0.8)
                            {

                            }
                            else
                            {
                                value *= 0.2f;
                            }
                        }
                    }
                    else if (PlayerData.Instance.FirstTableEcpm > 20)
                    {
                        if (PlayerData.Instance.AddTiXIanCount_Table <= 5)
                        {
                        if (isChaPing)
                        {
                            value = GetValue(dangwei) * 2;
                        }
                        else
                        {
                            //value = GetValue2(value);
                        }

                    }

                    }
                }
            }
            return value;


        }

    private static float GetValue2(float value)
    {
        if (PlayerData.Instance.TixianValues[0] <= 0.8)
        {
            value *= 1;
        }
        else
        {
            value *= 0.2f * 1;
        }

        return value;
    }

    private static float GetValue(float dangwei)
    {
        float value = 0;
        if(dangwei == 0.3f)
        {
            if (PlayerData.Instance.TixianValues[0] <= 0.9)
            {
                value = 0.05f;
            }
            else
            {
                value = 0.0001f;
            }
           
        }
        else if (dangwei == 0.5f)
        {
            if (PlayerData.Instance.TixianValues[0] <= 0.9)
            {
                value = 0.03f;
            }
            else
            {
                value = 0.00006f;
            }
        }
        else
        {
            if (PlayerData.Instance.TixianValues[0] <= 0.9)
            {
                value = 0.003f;
            }
            else
            {
                value = 0.000006f;
            }
        }
        return value;
    }

}
