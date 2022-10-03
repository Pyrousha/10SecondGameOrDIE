using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : Singleton<MainMenuCanvas>
{
    public bool canPlayerMove {get;set;}

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(InputHandler.Instance.Grab.down || InputHandler.Instance.Interact.down)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        PlayerController.Instance.SetCanMove(true);
        canPlayerMove = true;
        gameObject.SetActive(false);
    }
}
