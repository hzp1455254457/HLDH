using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedCollider : MonoBehaviour
{
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Produce")
    //    {
    //      var pro=  collision.GetComponent<ProduceQiPao>();
    //        ProduceQiPaoManager.Instance.AddProduceQiPao(pro);
    //        ProduceQiPaoManager.Instance.Remove(pro);
    //        if(GuideManager.Instance.isFirstGame && ProduceQiPaoManager.Instance.produceQiPaolist.Count == 0)
    //        {
    //            FaHuoPanel.Instance.faHuoGuide.GuideFuncEvent2();
    //        }
    //    }
        
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Produce")
        {
            var pro = collision.GetComponent<ProduceQiPao>();
            ProduceQiPaoManager.Instance.AddProduceQiPao(pro);
            ProduceQiPaoManager.Instance.Remove(pro);
            if (GuideManager.Instance.isFirstGame && ProduceQiPaoManager.Instance.produceQiPaolist.Count == 0)
            {
                FaHuoPanel.Instance.faHuoGuide.GuideFuncEvent2();
            }
        }
    }
}
