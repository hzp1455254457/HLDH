using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jiaochengButton : MonoBehaviour
{
    public GameObject detailPart, taskPart;
    public Text text;
    private Button jiedanButton;
    // Start is called before the first frame update
    void Start()
    {
        jiedanButton = GetComponent<Button>();
        jiedanButton.onClick.AddListener(()=>
        {
            jiedanButton.interactable = false;
            showNextJiaoCheng();
            jiedanButton.interactable = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "再完成<color=red>" + (999 - userData.Instance.currentOrderID).ToString() + "</color>个订单，可得999元红包";
    }

    public void showNextJiaoCheng()
    {
        if (userData.Instance.currentOneOrder == null)
        {
            return;
        }

        detailPart.SetActive(false);
        taskPart.SetActive(true);

        if (userData.Instance.isInJiaoCheng)
        {
            FindObjectOfType<wangdianjiaocheng>().SetStep(1);
            //FindObjectOfType<wangdianjiaocheng>().NextStep();
        }

        userData.Instance.isCollectingOrder = true;
    }
}
