using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;

/// <summary>
/// SO for an addressable scene reference.
/// </summary>
[CreateAssetMenu(fileName = "Scene Reference", menuName = "Scenes/Scene Reference")]
public class SceneReferenceSO : ScriptableObject
{
    [SerializeField] private AssetReference sceneReference;
    public AssetReference SceneReference {
        get {
            return sceneReference;
        }
    }

    [SerializeField] private SceneEnum sceneEnum;
    public SceneEnum SceneNum {
        get {
            return sceneEnum;
        }
    }
}
