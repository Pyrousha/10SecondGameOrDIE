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

        PlayerController.Instance.SetCanMove(false);
        PlayerController.Instance.Anim.SetTrigger("Die");

        StartCoroutine(SpawnGraveAndTransitionScene());

        //ReloadScene();
    }

    public IEnumerator SpawnGraveAndTransitionScene()
    {
        yield return new WaitForSeconds(1.05f);

        foreach (GrabbableID grab in grabbables)
        {
            grab.OnDeath();
        }

        yield return new WaitForSeconds(0.25f);

        TPCanvasController.Instance.Anim.SetTrigger("ClearToBlack");

        yield return new WaitForSeconds(10f / 60f);
        ReloadScene();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
