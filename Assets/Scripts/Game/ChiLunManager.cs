using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class ChiLunManager : MonoBehaviour
{
    public SurfaceEffector2D surfaceEffector2D;
    public FahuoDanBan fahuoDanBan;
    public UnityArmatureComponent unityArmatureComponent;
    public void StartAnim(bool value)
    {if (value)
        { surfaceEffector2D.speed = 5;
            unityArmatureComponent.animation.Play("idel").playTimes = 0;
            unityArmatureComponent.animation.timeScale = 2;
        }
        else
        {
            surfaceEffector2D.speed = 0;
            unityArmatureComponent.animation.Stop("idel");
        }
        fahuoDanBan.SetShowOrHide(value);
    }
    // Update is called once per frame
   
}
