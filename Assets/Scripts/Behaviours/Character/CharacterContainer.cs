using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CharacterContainer : MonoBehaviour
{
    [Header("Character Asset Reference")]
    [SerializeField] private AssetReference character;

    [Space]
    [Header("Spawning and Releasing Controls")]
    [SerializeField] private int instanceCount = 1;
    [SerializeField] private CharacterSpawningLogicEnum characterSpawningLogic;
    [SerializeField] private bool destroy = true;
    [SerializeField] private bool releaseInstance = true;
    [SerializeField] private bool releaseAsset = true;
    [SerializeField] private bool staticMethods = false;

    private GameObject characterObj;
    private List<GameObject> characterObjs = new List<GameObject>();
    private List<AsyncOperationHandle<GameObject>> asyncObjs = new List<AsyncOperationHandle<GameObject>>();
    
    //-----------------------------TEST---------------------------------
    // Just enable and disable this behaviour to test the loading, spawning, and unloading.
    private async Task OnEnable() {
        switch(characterSpawningLogic) {
            case CharacterSpawningLogicEnum.ADDRESSABLE_LOAD_UNITY_INSTANTIATE:
            // UnityInstantiate(await LoadCharacterAsync());
            GameObject obj = await LoadCharacterAsync();

            for(int i = 0; i < instanceCount; i++) {
                UnityInstantiate(obj);
            }
            break;
            case CharacterSpawningLogicEnum.ADDRESSABLE_LOAD_ADDRESSABLE_INSTANTIATE:
            await LoadCharacterAsync();
            goto case CharacterSpawningLogicEnum.ADDRESABLE_INSTANTIATE;
            case CharacterSpawningLogicEnum.ADDRESABLE_INSTANTIATE:
            for(int i = 0; i < instanceCount; i++) {
                AddressableInstantiate();
            }
            break;
        }
    }

    private void OnDisable() {
        for(int i = 0; i < characterObjs.Count; i++) {
            if(destroy) {
                Destroy(characterObjs[i]);
            }
            if(releaseInstance) {
                if(staticMethods) {
                    Addressables.ReleaseInstance(characterObjs[i]);
                }
                else {
                    character.ReleaseInstance(characterObjs[i]);
                }
            }
        }

        if(destroy || releaseInstance) {
            characterObjs.Clear();
        }

        // if(releaseAsset) {
        //     for(int j = 0; j < asyncObjs.Count; j++) {
        //         Addressables.Release<GameObject>(asyncObjs[j]);
        //     }
        // }

        if(releaseAsset) {
            if(staticMethods) {
                for(int i = 0; i < asyncObjs.Count; i++) {
                    Addressables.Release<GameObject>(asyncObjs[i]);
                }
            }
            else {
                character.ReleaseAsset();
            }
        }

        if(releaseInstance || releaseAsset) {
            asyncObjs.Clear();
        }
    }
    //-----------------------------TEST---------------------------------

    private async Task<GameObject> LoadCharacterAsync() {
        Debug.Log("Started Loading");
        AsyncOperationHandle<GameObject> asyncOp
        = (staticMethods) ? Addressables.LoadAssetAsync<GameObject>(character) : character.LoadAssetAsync<GameObject>();
        // AsyncOperationHandle<GameObject> asyncOp = Addressables.LoadAssetAsync<GameObject>(character);

        asyncOp.Completed += (asyncObj) => {
            Debug.Log("Loaded " + asyncObj.Result.name + " into memory.");
        };
        asyncObjs.Add(asyncOp);

        return await asyncOp.Task;
    }

    private void AddressableInstantiate() {
        Debug.Log("Started Addressable Instantiate");
        AsyncOperationHandle<GameObject> asyncOp
        = (staticMethods) ? Addressables.InstantiateAsync(character, transform.position, transform.rotation) : character.InstantiateAsync(transform.position, transform.rotation);
        // AsyncOperationHandle<GameObject> asyncOp = Addressables.InstantiateAsync(character, transform.position, transform.rotation);

        asyncOp.Completed += (asyncObj) => {
            Debug.Log("Addressables Instantiated " + asyncObj.Result.name);
            // characterObj = asyncObj.Result;
            characterObjs.Add(asyncObj.Result);
        };
    }

    private void UnityInstantiate(GameObject obj) {
        Debug.Log("Started Instantiate");
        // characterObj = Instantiate<GameObject>(obj, transform.position, transform.rotation);
        characterObjs.Add(Instantiate<GameObject>(obj, transform.position, transform.rotation));
    }
}
