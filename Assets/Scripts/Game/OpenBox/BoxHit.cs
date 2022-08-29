using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxHit : MonoBehaviour
{
    public Rigidbody myRigidbody;

    public Material BoxMaterial;
    public Texture[] BoxTextures;
   //碰撞到墙壁
    public UnityEvent CollisionEvent;
    //只埋点一次点击拆快递按钮
    bool maiDian = false;

    public void Start()
    {
        maiDian = false;

    }

    public void OnEnable()
    {
        ShowCurrent();
    }
    void ShowCurrent()
    {
        int boxType = PlayerPrefs.GetInt(BoxGame.NextBoxType, 1);
        switch (boxType)
        {
            case 1:
                BoxMaterial.mainTexture = BoxTextures[0];
                break;
            case 2:
                BoxMaterial.mainTexture = BoxTextures[1];
                break;
            case 3:
                BoxMaterial.mainTexture = BoxTextures[2];
                break;
            default:
                break;
        }
    }


    public void HitBox()
    {
        if (!maiDian)
        {
            AndroidAdsDialog.Instance.UploadDataEvent("click_open_chaikuaidi");
            maiDian = true;
        }
        AudioManager.Instance.PlaySound("bouton");

        myRigidbody.AddForce(-1000, 1000, -1000);
        Debug.Log("add force");
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            AudioManager.Instance.PlaySound("crash");
            CollisionEvent?.Invoke();
        }

    }
}
