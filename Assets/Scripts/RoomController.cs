using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomController : MonoBehaviourPunCallbacks
{
    public static RoomController instance;

    private void Start()
    {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PlayerController"), Vector3.zero, Quaternion.identity) ;
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
    }

}
