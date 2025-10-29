using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Menu : MonoBehaviourPunCallbacks
{
    [SerializeField] private MenuEntrance _menuEntrance;
    [SerializeField] private MenuLobby _menuLobby;
    [SerializeField] private GameObject _menuLoading;

    void Start()
    {
        _menuEntrance.gameObject.SetActive(false);
        _menuLobby.gameObject.SetActive(false);
        _menuLoading.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        _menuLoading.SetActive(false);
        _menuEntrance.gameObject.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        ChangeMenu(_menuLobby.gameObject);
        _menuLobby.photonView.RPC("UpdatePlayerList", RpcTarget.All);
    }

    public void ChangeMenu(GameObject menu)
    {
        _menuEntrance.gameObject.SetActive(false);
        _menuLobby.gameObject.SetActive(false);

        menu.SetActive(true);
    }

    public void LeftLooby()
    {
        NetworkManager.Instance.LeftLobby();
        ChangeMenu(_menuEntrance.gameObject);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        _menuLobby.UpdatePlayerList();
    }

    public void StartGame(string sceneName)
    {
        NetworkManager.Instance.photonView.RPC("StartGame", RpcTarget.All, sceneName);
    }

}
