using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jesuoSprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerData.Instance.day >= 3)
        {
            Debug.Log("�����ɹ�");
            gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
