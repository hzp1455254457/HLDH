using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class stageControl : MonoBehaviour
{
    private bool isOnce = true;
    public GameObject sthShow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "ball")
        {
            Debug.Log("ball click");
            AudioManager.Instance.PlaySound("desk");
            if (sthShow != null)
            {
                sthShow.gameObject.SetActive(true);
                sthShow.gameObject.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.5f);
            }
        }
    }
}
