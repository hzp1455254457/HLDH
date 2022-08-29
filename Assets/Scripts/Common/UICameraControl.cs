using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class UICameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        float designWidth = 750f;//�����зֱ��ʵĿ��
        float designHeight = 1334f;//�����зֱ��ʵĸ߶�
        float designOrthographicSize = 6.70f;//����ʱ����������Ĵ�С��3.2*100*2=640����100����ΪUnity�е�pixels per unit��100����2����Ϊ�����ó���Ļ��һ��
        float designScale = designWidth / designHeight;
        float scaleRate = (float)Screen.width / (float)Screen.height;
        if (scaleRate < designScale)//�ж�������Ƶı�����ʵ�ʱ����Ƿ�һ�£����������õĴ����������Ӧ���ã�С�Ļ������Զ�����Ӧ
        {
            float scale = scaleRate / designScale;
            GetComponent<Camera>().orthographicSize = designOrthographicSize / scale;
        }
        else
        {
            GetComponent<Camera>().orthographicSize = designOrthographicSize;
        }
    }
}
