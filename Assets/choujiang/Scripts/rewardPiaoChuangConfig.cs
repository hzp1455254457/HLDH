using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class rewardPiaoChuangConfig : MonoBehaviour
{
    public GameObject rewardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitRewardPiaoChuang(int number1,float number2,int number3)
    {
        if (number1 == 0 && number2 == 0.0f && number3 == 0)
        {
            GameObject obj = Instantiate(rewardPrefab, transform);
            obj.GetComponent<rewardConfig>().InitRewardPanel(1, "100");
        }

        if (number1 > 0)
        {
            GameObject obj = Instantiate(rewardPrefab, transform);
            obj.GetComponent<rewardConfig>().InitRewardPanel(1,number1.ToString());
        }

        if (number2 > 0)
        {
            GameObject obj = Instantiate(rewardPrefab, transform);
            obj.GetComponent<rewardConfig>().InitRewardPanel(2, number2.ToString("F1"));
        }

        if (number3 > 0)
        {
            GameObject obj = Instantiate(rewardPrefab, transform);
            obj.GetComponent<rewardConfig>().InitRewardPanel(3, number3.ToString());
        }
       
    }
}
