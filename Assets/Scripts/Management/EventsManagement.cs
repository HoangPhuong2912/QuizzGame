using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EventsManagement : MonoBehaviourPunCallbacks
{
    public static EventsManagement Instance { get; private set; }
    [SerializeField] public GameObject MenuPlayerList;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void StartEvent(int eventId)
    {
        switch (eventId)
        {
            case 0: //Go back spaces
                Debug.Log("Reverse Dice");
                PlayerPiece.me.StartReverseMove();
                break;

            case 1:
                Debug.Log("Lost the Turn");
                PlayerPiece.me.isMoving = false;
                PlayerPiece.me.ResetItensProps();
                GameSystem.Instance.photonView.RPC("NextPlayer", RpcTarget.All);
                break;

            case 2:
                Debug.Log("I don't know");
                break;
        }
    }

    [PunRPC]
    void CursePlayer(string playerName)
    {
        Debug.LogError("Cursed dice on player: " + playerName);
        if (PlayerPiece.me.playerName == playerName) PlayerPiece.me.hasCursedDice = true;
    }


}

