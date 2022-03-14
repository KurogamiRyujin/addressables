using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Trigger for going into a new scene.
/// </summary>
public class ScenePortal : MonoBehaviour
{
    [SerializeField] private SceneEnum scene;
    [SerializeField] private SceneReferencesSO sceneReferences;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            EnterScene();
        }
    }

    private void OnTriggerEnter(Collider other) {
        EnterScene();
    }

    public void EnterScene() {
        //Scene to load
        SceneLoader.LoadScene(sceneReferences.sceneReferences.Find(sceneRef => sceneRef.SceneNum == scene).SceneReference);
        
        //Load Loading Scene
        SceneManager.LoadScene(sceneReferences.loadingSceneName, LoadSceneMode.Additive);
    }
}
