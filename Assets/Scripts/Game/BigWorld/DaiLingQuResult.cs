using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DaiLingQuResult : MonoBehaviour
{
    public float LifeTime = 0.4f;
    public int CoinValue;
    public TextMeshPro ValueText;
    void Start()
    {
        ValueText.text = "+" + CoinValue;
        Destroy(this.gameObject, LifeTime);
        //TODO 需要保存下来，调用保存方法
    }
}
