using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DaiLingQu : MonoBehaviour
{
    //��ȡ��Ч
    public GameObject LingQuTeXiao;
    [HideInInspector]
    public int CoinValue;
    private void OnMouseDown()
    {
        //Debug.Log("��ȡ�������");
        //��ȡ��Ч
        //GameObject _lingQuTeXiao = Instantiate(LingQuTeXiao, this.transform.position, this.transform.rotation);
        //_lingQuTeXiao.GetComponent<DaiLingQuResult>().CoinValue = CoinValue;

        //���Ź�桢�ӽ��
        //PlayerDate.Instance.GetGold(CoinValue);

        //������ȡ���+����������ӽ�Ҳ��������ڲ�����
        AwardManagerNew.Instance.ShowUI(CoinValue, NumberGenenater.GetRedCount(), 0);

        this.gameObject.SetActive(false);
      
    }
}
