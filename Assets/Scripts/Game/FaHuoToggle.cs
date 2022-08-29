using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class FaHuoToggle : MonoBehaviour
{
    public Image image;
    public GameObject tipsGo;
    AudioSource audioSource;
    public CanvasGroup canvasGroup;
    public void SetShow(bool value)
    {
        image.gameObject.SetActive(value);
    }
    private void Start()
    {
        if (FaHuoPanel.Instance.ziDongFaHuo.IsZiDong)
        {
           ZiDongFaHuoNew();
        }
        SetAlpha(PlayerData.Instance.ProduceQiPaoList.Count * 0.02f + 0.2f);
    }
    Tweener tweener;
    public void OnPointer()
    {
        ProduceQiPaoManager.Instance.dropedInCarCollider.SetShow(true);
        ProduceQiPaoManager.Instance.isFahuo = true;
        isFahuo = true;
        if (tweener != null)
        {
            tweener.Kill();

        }
        tweener = canvasGroup.DOFade(1, 0.8f);
       // tweener.onComplete = () => SetAlpha(ProduceQiPaoManager.Instance.produceQiPaolist.Count * 0.02f + 0.2f);
        if (tipsGo.activeSelf)
        {
            tipsGo.SetActive(false);
        }
        ToggleManager.Instance.isShowFaHuoTips = false;
        filled = false;
        print("按下中");
        //  if( Time.frameCount % 10 == 0)
        isPressed = false;
        isPress = true;
        if (!FaHuoPanel.Instance.ziDongFaHuo.IsZiDong)
        {
            isYaoHuang = true;
        }
    }
    bool isYaoHuang = false;
    bool isPressed = false;
    bool isPress = false;
    bool filled = false;
    bool pressFilled = false;
    bool isFahuo = false;
    public void SetAlpha(float value)
    {
        if (isFahuo) return;
        canvasGroup.alpha = value;
    }
    public void OnPointerUp()
    {
        ProduceQiPaoManager.Instance.isFahuo = false;
        isFahuo = false;
        RecoverColor();

        //audioSource.Stop();
        pressFilled = true;
        filled = false;
        isPress = false;
        isPressed = true;
        isYaoHuang = false;
        if (image.fillAmount >= 1)
        {
            filled = true;
        }
        print("抬起时");
    }

    private void RecoverColor()
    {
        if (tweener != null)
        {
            tweener.Kill();
            tweener = null;
           // tweener.Pause();
          canvasGroup.alpha= ProduceQiPaoManager.Instance.produceQiPaolist.Count * 0.02f + 0.2f;
        }
    }

  public  void OnPointerUp1()
    {
        ProduceQiPaoManager.Instance.isFahuo = false; 
        RecoverColor();
        isFahuo = false;
        // audioSource.Stop();
        isPress = false;
        isPressed = true;
        filled = true;
       
        isYaoHuang = false;
    }
    private void FixedUpdate()
    {
        if (isPressed)
        {
           // print("unity+挡板向右移动");
            if (image.fillAmount <= 0)
            {
               
                isPressed = false;
                return;
               
                //image.fillAmount -= Time.deltaTime * 2;
            }
            if (filled)
            {
                FaHuoPanel.Instance.RecoverDanBang();
            }
            image.fillAmount -= Time.fixedDeltaTime*2 * 0.33f;
           
        }
        if (isPress)
        {
          //  print("unity+挡板向左移动");
            time += Time.fixedDeltaTime;
            if (time >= 0.3f)
            {
                AudioManager.Instance.PlaySound("fahuo_btn");
                time = 0;
            }
            if (image.fillAmount >= 1)
            {
                if (pressFilled)
                {
                    AudioManager.Instance.Shake(MoreMountains.NiceVibrations.HapticTypes.Warning);
                    pressFilled = false;
                }
                FaHuoPanel.Instance.MoveDanBang();
                return;
            }
            image.fillAmount += Time.fixedDeltaTime * 2;
        }
    }
    float time = 0;
    private void OnApplicationFocus(bool focus)
    {
        print("游戏失去焦点时，unity+++");
        if (FaHuoPanel.Instance.ziDongFaHuo.IsZiDong) return;
        OnPointerUp1();
    }
    public IEnumerator ZiDongFaHuo()
    {
        OnPointer();
        yield return new WaitForSeconds(5f);
        OnPointerUp();
        ProduceQiPaoManager.Instance.isZiDong = true;
    }
    public void ZiDongFaHuoNew()
    {
        OnPointer();
      
    }
    //Vector2 dir;
    //float times = 0f;
    //private void FixedUpdate()
    //{
    //    times += Time.fixedDeltaTime;
    //    if (isYaoHuang/*&& times >= 0.1f*/)
    //    {
    //        //times = 0f;
    //        //dir = Vector2.zero;
    //        //dir.x = Input.acceleration.x*100;
    //        //dir.y = Input.acceleration.y*250;
    //        float x = Input.acceleration.x;
    //        float y = Input.acceleration.y;
    //        float z = Input.acceleration.z;
    //        Vector3 force = new Vector3(x * 50.0F, 0, 0);
    //        //ball.forward(force);
    //        //for (int i = 0; i < ProduceQiPaoManager.Instance.produceQiPaoDropedsInCars.Count; i++)
    //        //{
    //        //    //ProduceQiPaoManager.Instance.produceQiPaoDropedsInCars[i]._rigidbody2D.gravityScale = 0;
    //        //    ProduceQiPaoManager.Instance.produceQiPaoDropedsInCars[i]._rigidbody2D.AddForce(force);
    //        //}
    //        for (int i = 0; i < ProduceQiPaoManager.Instance.produceQiPaoNoDropeds.Count; i++)
    //        {
    //           // ProduceQiPaoManager.Instance.produceQiPaoNoDropeds[i]._rigidbody2D.gravityScale = 0;
    //            ProduceQiPaoManager.Instance.produceQiPaoNoDropeds[i]._rigidbody2D.AddForce(force);
    //        }
    //    }
    //}
}
