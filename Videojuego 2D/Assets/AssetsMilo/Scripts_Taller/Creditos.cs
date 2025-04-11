using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("WaitforEnd", 45f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu_IU");
        }
    }

    void WaitforEnd()
    {
        SceneManager.LoadScene("Menu_IU");
    }
}
