using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsShowBase 
{
    private static TipsShowBase _Instance;

    public static TipsShowBase Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new TipsShowBase();
            }
            return _Instance;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">Ԥ��������</param>
    /// <param name="bornTf">����λ��</param>
    /// <param name="targetTf">Ŀ��λ��</param>
    /// <param name="value">text������ʾ�����֣�Ҫ��ʾ������Ҫ���뼸��</param>
    /// <param name="sprite">ui������ʾ��ͼƬ</param>
    /// <param name="color">text��ʾ����ɫ</param>
    /// <param name="unityAction">����ʱ�ûص�����</param>
    /// <param name="scale">�Ŵ����</param>
    public void Show(string name,Transform bornTf, Transform targetTf, string[] value, Sprite[] sprite, Color[] color, UnityEngine.Events.UnityAction unityAction = null, float scale = 1f)
    {
            var effect = GameObjectPool.Instance.CreateObject(name, ResourceManager.Instance.GetProGo(name), bornTf, Quaternion.identity);
            effect.GetComponent<TipsEffectBase>().Show(targetTf, value, sprite, unityAction, color, scale);
        
    }
}
