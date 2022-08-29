using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI : MonoBehaviour
{
    public GameObject TestUIObj;
    public void ToogleTestUI()
    {
        TestUIObj.SetActive(!TestUIObj.activeInHierarchy);
    }
}
