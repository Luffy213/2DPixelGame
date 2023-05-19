using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class PlayerManager : MonoBehaviour
{
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        if(_photonView.IsMine)
        {
            CreateController();
        }
    }
    private void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), new Vector2(Random.Range(-2.47f, 2.46f), Random.Range(-4.68f, 4.67f)), Quaternion.identity);
    }
}


