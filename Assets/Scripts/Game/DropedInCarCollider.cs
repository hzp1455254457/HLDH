using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedInCarCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Produce")
        {
            
          var produce=  collision.GetComponent<ProduceQiPao>();
            ProduceQiPaoManager.Instance.AddInCar(produce);
            ProduceQiPaoManager.Instance.RemoveProduceQiPao(produce);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Produce")
        {

            var produce = collision.GetComponent<ProduceQiPao>();
            if (!produce.isInCar)
            {
                ProduceQiPaoManager.Instance.RemoveInCar(produce);
                ProduceQiPaoManager.Instance.AddProduceQiPao(produce);
            }
        }
    }
    public void SetShow(bool value)
    {
        if (gameObject.activeInHierarchy != value)
            gameObject.SetActive(value);
    }
}
