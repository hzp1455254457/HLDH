using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamareManager : MonoSingleton<CamareManager>
{
    public Camera uiCamera, uiCamera1,camera_Toggle;
 public Camera mainCamere;
    void Start()
    {
      
    }
    public void SetStates(bool value)
    {
      UIManager.Instance. _Canvas.enabled = value;

        //uiCamera.enabled = value;
        // uiCamera1.enabled = value;
        if (!value)
        {
            mainCamere.enabled = value;
        }
    }
    public void SetStates1(bool value)
    {
        uiCamera.enabled = value;
         uiCamera1.enabled = value;
        camera_Toggle.enabled = value;
        //mainCamere.enabled = value;
    }

    public void SetUIFirst(bool value,int depth=1)
    {
        //if (value)
        //{
        //    uiCamera.depth = depth;
        //    mainCamere.depth = -depth;
        //}
        //else
        //{
        //    uiCamera.depth = -depth;
        //    mainCamere.depth = depth;
          
        //}
     mainCamere.enabled=(!value);
    }
   
}
