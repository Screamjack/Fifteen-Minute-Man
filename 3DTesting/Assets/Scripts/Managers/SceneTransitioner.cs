using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : AbstractTrigger {

    [SerializeField]
    string nextScene;
    [SerializeField]
    int toIndex;

    static GameObject LoadingUI;
    bool loading = false;

    void Awake()
    {
        //if (LoadingUI == null)
        //    LoadingUI = transform.Find("loadingScreen").gameObject;   
    }

    public override void ActivateTrigger()
    {
        if (!CheckTrigger()) return;
        GameObject.Find("Character").GetComponent<PlayerController>().enabled = false;
        DoorMaster.Master.SetLDI(toIndex);
        StartCoroutine(LoadNextScene());
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
