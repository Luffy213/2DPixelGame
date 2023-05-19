using Photon.Pun;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text playerName;
    private Player _player;

    public void SetUp(Player player)
    {
        _player = player;
        playerName.text = player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (_player == otherPlayer)
            Destroy(gameObject);
    }
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }



}
