using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShowTest : MonoBehaviour
{
    public GameObject[] LockObjs;

   public CinemachineDollyCart [] YunShuGongJus;

    bool isLock = false;
    public void ToogleLock()
    {
        foreach (var item in LockObjs)
        {
            item.SetActive(isLock);
        }

        if (isLock)
        {
            foreach (var item in YunShuGongJus)
            {
                item.m_Speed = 0;
                item.m_Position = 0;
            }
        }
        else
        {
            foreach (var item in YunShuGongJus)
            {
                item.m_Speed = 6;
                item.m_Position = 0;
            }
        }

        isLock = !isLock;
    }
}
