using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : Singleton<DeathController>
{ 
    [SerializeField] private GrabbableID[] grabbables;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        ReloadScene();
    }

    public void OnDeath()
    {
        foreach (GrabbableID grab in grabbables)
        {
            grab.OnDeath();
        }

        ReloadScene();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
