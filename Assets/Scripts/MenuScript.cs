using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    void Update()
    {
      if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
      if (Input.GetKey("backspace"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
