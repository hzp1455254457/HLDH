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
        float designWidth = 750f;//开发中分辨率的宽度
        float designHeight = 1334f;//开发中分辨率的高度
        float designOrthographicSize = 6.70f;//开发时正交摄像机的大小，3.2*100*2=640；×100是因为Unity中的pixels per unit是100，×2是因为想设置成屏幕的一半
        float designScale = designWidth / designHeight;
        float scaleRate = (float)Screen.width / (float)Screen.height;
        if (scaleRate < designScale)//判断我们设计的比例跟实际比例是否一致，若我们设置的大则进入自适应设置，小的话他会自动自适应
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
