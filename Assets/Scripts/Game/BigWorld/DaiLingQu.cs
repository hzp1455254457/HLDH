using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DaiLingQu : MonoBehaviour
{
    //领取特效
    public GameObject LingQuTeXiao;
    [HideInInspector]
    public int CoinValue;
    private void OnMouseDown()
    {
        //Debug.Log("领取发货金币");
        //领取特效
        //GameObject _lingQuTeXiao = Instantiate(LingQuTeXiao, this.transform.position, this.transform.rotation);
        //_lingQuTeXiao.GetComponent<DaiLingQuResult>().CoinValue = CoinValue;

        //播放广告、加金币
        //PlayerDate.Instance.GetGold(CoinValue);

        //弹出领取金币+红包弹窗，加金币操作由其内部处理
        AwardManagerNew.Instance.ShowUI(CoinValue, NumberGenenater.GetRedCount(), 0);

        this.gameObject.SetActive(false);
      
    }
}
