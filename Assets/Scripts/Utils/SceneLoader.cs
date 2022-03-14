using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using System;

/// <summary>
/// Scene Loading functions class.
/// </summary>
public class SceneLoader
{
    private static AsyncOperationHandle<SceneInstance> asyncLoading;

    public static event Action OnLoadingComplete;

    public static float LoadingProgress {
        get {
            return asyncLoading.PercentComplete;
        }
    }

    public static void LoadScene(AssetReference sceneReference) {
        asyncLoading = sceneReference.LoadSceneAsync();
        
        asyncLoading.Completed += (async) => {
            if(OnLoadingComplete != null) {
                OnLoadingComplete.Invoke();
            }
        };
    }
}
