using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;

    [SerializeField] private TMP_InputField _roomInputField;
    [SerializeField] private TMP_Text _errorText;
    [SerializeField] private TMP_Text _roomNameText;
    [SerializeField] private Transform _roomList;
    [SerializeField] private GameObject _roomButtonPrefab;
    [SerializeField] private Transform _playerList;
    [SerializeField] private GameObject _playerNamePrefab;
    [SerializeField] private GameObject _startGameButton;

    private void Start()
    {
        instance = this;
        Debug.Log("Присоединиться к Мастер серверу");
        PhotonNetwork.ConnectUsingSettings();
        MenuControl.instance.OpenMenu("loading");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Присоединились к мастер серверу");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Присоединились к лобби");
        MenuControl.instance.OpenMenu("title");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_roomInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(_roomInputField.text);
        MenuControl.instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        _roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        MenuControl.instance.OpenMenu("room");

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < _playerList.childCount; i++)
        {
            Destroy(_playerList.GetChild(i).gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(_playerNamePrefab, _playerList).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _errorText.text = "Error: " + message;
        MenuControl.instance.OpenMenu("error");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuControl.instance.OpenMenu("loading");
    }
    public override void OnLeftRoom()
    {
        MenuControl.instance.OpenMenu("title");
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuControl.instance.OpenMenu("loading");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < _roomList.childCount; i++)
        {
            Destroy(_roomList.GetChild(i).gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(_roomButtonPrefab, _roomList).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player player)
    {
        Instantiate(_playerNamePrefab, _playerList).GetComponent<PlayerListItem>().SetUp(player);
    }
}
