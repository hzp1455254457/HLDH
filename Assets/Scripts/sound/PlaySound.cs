using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySound : MonoBehaviour,IPointerUpHandler,IPointerDownHandler

{
    public string name;
    public bool isClick=false;
    public ClickType clickType;
    bool isContinue = false;
  
    AudioSource audioSource;
    public AudioSource MyPlaySound()
    {
      return  AudioManager.Instance.PlaySound(name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isClick)
        {

            
            if (clickType==ClickType.Continue)
            {
                audioSource= AudioManager.Instance.PlaySound(name, true);


            }
           else if (clickType == ClickType.Loop)
            {
                isContinue = true;
                StartCoroutine(ContinuePlay());
            }
            else if (clickType == ClickType.Once)
            {
                audioSource = MyPlaySound();
                print("播放声音");
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isClick)
        {
            MyPlaySound();
        }
        else
        {
           if(clickType == ClickType.Continue)
            audioSource.Stop();
           else if (clickType ==ClickType.Loop )
            {
                isContinue = false;
            }
        }
    }

    private IEnumerator ContinuePlay()
    {while (isContinue)
        {
            MyPlaySound();
            yield return new WaitForSeconds(0.1f);
        }
    }

}
