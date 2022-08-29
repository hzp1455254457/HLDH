using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class stickShake : MonoBehaviour
{
    public float lowAngle = -120.0f,highAngle = -60.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 0, highAngle), 2.0f).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
    