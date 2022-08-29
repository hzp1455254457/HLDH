using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopNllItem : MonoBehaviour
{
    public Text name_Text;
    public Text louceng;
    public void SetText(string value1 ,string value2)
    {
        name_Text.text = value1;
        louceng.text = value2;
    }
}
