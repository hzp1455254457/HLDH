using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFollow : MonoBehaviour
{
    public Transform canvaTf;
    public Transform targer;
    public Canvas canvas;
    private void Start()
    {
       // canvas.worldCamera = CamareManager.Instance.mainCamere;
    }
    private void Update()
    {
        Vector3 vector = new Vector3( targer.localPosition.x, targer.localPosition.y+0.64f, targer.localPosition.z);
        canvaTf.localPosition = vector;
    }

}
