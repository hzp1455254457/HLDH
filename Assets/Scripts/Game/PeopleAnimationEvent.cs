using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PeopleAnimationEvent : MonoBehaviour
{
    Animator animator;
    public SkeletonGraphic peopleAnim;
    bool value = true;
    GameObject go;
    public UnityEngine.Transform bornTf;
    public bool startStop;
    public SurfaceEffector2D effector2D;
   public CanKu canKu;

    void Start()
    {
        animator = GetComponent<Animator>();
        go = ResourceManager.Instance.GetProGo("Ïä×Ó");
        animator.speed = 3;
        if (GuideManager.Instance.isFirstGame)
        {
            SetSudu();
        }
        //kuPanel = UIManager.Instance.GetPanel("Panel_Canku") as CanKuPanel;
    }
    public void SetSudu()
    {
        animator.speed *= 3;
        effector2D.speed *= 3;

    }

    public void RecoverSudu()
    {
        animator.speed = 3;
        effector2D.speed = 200;
    }
    // Update is called once per frame
    int i = 0;
    public void SetWalk()
    {
      //  value = !value;
        animator.SetBool("walk", true);
        SwitchAnim(ConfigManager.Instance.animName[0]);
        
        GameObjectPool.Instance.CreateObject("Ïä×Ó", go, bornTf, Quaternion.identity);
        canKu.RefreshCount();
      
        if (canKu.courier.deliever_item_num > 1)
        {
            canKu. courier.deliever_item_num -= 1;
        }
        else
        {
            canKu. courier.deliever_item_num = 0;
        }
        if (canKu.courier.deliever_item_num <= 0)
        {
            canKu.StopSell();
            // courier.Busy_state = 0;
        }
        if (GuideManager.Instance.isFirstGame)
        {
            i++;
            if (i ==4)
            {
                animator.speed = 0;
            }
        }
    }
    public UnityAction animAction;
    public void SetWalk1()
    {
        //animator.speed = 3;
        //value = !value;
        animator.SetBool("walk", false);
        SwitchAnim(ConfigManager.Instance.animName[1]);
        if (startStop)
        {
            gameObject.SetActive(false);
            canKu.isStop = true;
        }
    }
    public void SwitchAnim(string name)
    {
        value = !value;
       // peopleAnim.AnimationState.SetAnimation(0, name, true);
        peopleAnim.initialFlipX = value;
        peopleAnim.Initialize(true);
        peopleAnim.AnimationState.SetAnimation(0, name, true);
    }
    public void StopAnimation()
    {
        startStop = true;
    }
    private void OnDisable()
    {
        startStop = false;
    }
    private void OnEnable()
    {
        canKu.isStop = false;
        startStop = false;
        peopleAnim.initialFlipX = true;
        
        peopleAnim.AnimationState.SetAnimation(0,"walk2",true);
    }
    public void SetSpeed()
    {
        
    }
}
