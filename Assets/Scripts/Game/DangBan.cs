using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangBan : MonoBehaviour
{
    public CanKu canKu;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "XiangZi")
        {
          
            GameObjectPool.Instance.CollectObject(collision.gameObject);
            canKu.SellProduce(1);
            canKu.ShowFaHuoWin();
            //if (GuideManager.Instance.isFirstGame)
            //{
            //  canKu.kuPanel.FulledWallet();
            //}
        }
    }
}
