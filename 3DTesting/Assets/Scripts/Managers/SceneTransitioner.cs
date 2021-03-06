﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour {

    [SerializeField]
    Scene nextScene;

    static GameObject LoadingUI;
    bool loading = false;

    void Awake()
    {
        if (LoadingUI == null)
            LoadingUI = transform.Find("loadingScreen").gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !loading)
        {
            other.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(LoadNextScene());
        }
    }

    /// <summary>
    /// Preps an asynchronous load for the next scene.
    /// </summary>
    IEnumerator LoadNextScene()
    {
        loading = true;
        AsyncOperation loader = SceneManager.LoadSceneAsync(nextScene.buildIndex);

        while(!loader.isDone)
        {
            Debug.Log(loader.progress);
            yield return null;
        }
    }

}
