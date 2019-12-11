using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PindahScene : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isEscapeToExit;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void MulaiPermainan()
    {
        SceneManager.LoadScene("Main");
    }

    public void KembaliKeMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
