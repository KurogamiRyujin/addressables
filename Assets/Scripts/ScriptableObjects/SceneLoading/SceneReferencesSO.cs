using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SO for scene references.
/// </summary>
[CreateAssetMenu(fileName = "Scene References", menuName = "Scenes/Scene References")]
public class SceneReferencesSO : ScriptableObject
{
    public List<SceneReferenceSO> sceneReferences;
    public Scene loadingScene;
    public string loadingSceneName;
}
