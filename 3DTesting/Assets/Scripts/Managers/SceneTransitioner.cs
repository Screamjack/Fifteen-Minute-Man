using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : AbstractTrigger {

    [SerializeField]
    string nextScene;

    static GameObject LoadingUI;
    bool loading = false;

    void Awake()
    {
        //if (LoadingUI == null)
        //    LoadingUI = transform.Find("loadingScreen").gameObject;   
    }

    public override void ActivateTrigger()
    {
        GameObject.Find("Character").GetComponent<PlayerController>().enabled = false;
        StartCoroutine(LoadNextScene());
    }

    public override bool CheckTrigger()
    {
        return true;
    }

    public override void SetFlag()
    {
    }

    /// <summary>
    /// Preps an asynchronous load for the next scene.
    /// </summary>
    IEnumerator LoadNextScene()
    {
        loading = true;
        GameManager.manager.TickTheClock = false;
        AsyncOperation loader = SceneManager.LoadSceneAsync(nextScene);

        while(!loader.isDone)
        {
            Debug.Log(loader.progress);
            yield return null;
        }
    }

}
