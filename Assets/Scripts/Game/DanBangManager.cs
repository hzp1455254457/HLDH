using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanBangManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform danbanRect;
    public Transform danbanRect1;
    public float speed = 5f;
    bool isRecovered = false;
    bool isRecovered1 = false;
    //bool canMove = true;
    public void MoveLeft()
    {//if (!canMove) return;
        isRecovered1 = true;
           isRecovered = false;
           Vector3 ver = danbanRect.localRotation.eulerAngles;
        //Debug.LogError(ver.z);
        if (ver.z <= 270 && ver.z != 0) {
            ver.z = 270;
            danbanRect1.localRotation = Quaternion.Euler(-ver);
            danbanRect.localRotation = Quaternion.Euler(ver);
            return; }
        ver.z-= Time.fixedDeltaTime * speed;
        danbanRect1.localRotation= Quaternion.Euler(-ver);
        danbanRect.localRotation=Quaternion.Euler( ver);
    }
    public void MoveRight()
    {
        //  if (!canMove) return;
        Vector3 ver = danbanRect.localRotation.eulerAngles;

        if (ver.z >=350)
        {
            ver.z = 359;
            
            isRecovered = true;
            if (isRecovered && isRecovered1)
            {
                isRecovered1 = false;
                danbanRect.localRotation = Quaternion.Euler(ver);
                danbanRect1.localRotation = Quaternion.Euler(-ver);
               
                //   canMove = false;
                //UnityActionManager.Instance.DispatchEvent("FaHuoEvent");
                StartCoroutine(Global.Delay(1.5f, () =>
                    {
                        UnityActionManager.Instance.DispatchEvent("FaHuoEvent");
                    //canMove = true;
                }));
                
                //UnityActionManager.Instance.DispatchEvent("FaHuoEvent");
            }
            return;
        }
        ver.z += Time.fixedDeltaTime* speed * 2 * 0.33f;
        danbanRect.localRotation = Quaternion.Euler(ver);
        danbanRect1.localRotation = Quaternion.Euler(-ver);
    }
}
