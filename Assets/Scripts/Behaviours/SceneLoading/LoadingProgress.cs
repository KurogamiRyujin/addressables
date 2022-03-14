using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingProgress : MonoBehaviour
{
    [SerializeField] private Text loadingProgress;

    private bool sceneLoaded;

    private void Start() {
        sceneLoaded = false;

        SceneLoader.OnLoadingComplete += OnSceneLoaded;
    }

    private void OnDestroy() {
        SceneLoader.OnLoadingComplete -= OnSceneLoaded;
    }

    private void Update() {
        if(!sceneLoaded) {
            loadingProgress.text = (SceneLoader.LoadingProgress * 100).ToString() + "%";
        }
    }

    private void OnSceneLoaded() {
        sceneLoaded = true;
        UnloadThisScene();
    }

    private void UnloadThisScene() {
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
