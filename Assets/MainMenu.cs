using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public int gameStartScene; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* vv*/
    }

    public void SartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }
}
