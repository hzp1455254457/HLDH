using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class headUp : MonoBehaviour
{
    private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(parent.localEulerAngles.x, parent.localEulerAngles.y, -parent.localEulerAngles.z);
    }
}
