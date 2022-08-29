using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterMainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
       
        Debug.Log("进入带货");
        //SceneManager.UnloadSceneAsync("BigWorld");
        SceneManager.LoadScene(1);
    }
}
