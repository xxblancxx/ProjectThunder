using System;
using Assets;
using UnityEngine;
using System.Collections;

public class NetworkScript : MonoBehaviour
{
    public Vector2 spawnPosition;
    private RoomOptions newRoomOptions;
    private bool gameStarted;
 
    private GameObject myPlayer;
    // Use this for initialization
    void Start()
    {
        Connect();
       
    }

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("V0.1");
        Debug.Log(PhotonNetwork.room);
    }

    void OnGUI()
    {
        if (!gameStarted)
        {
            GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        }
    }
    void DoMyWindow(int windowID)
    {
        if (GUILayout.Button("Hello World"))
            print("Got a click");

    }
    void OnJoinedLobby()
    {
        newRoomOptions = new RoomOptions();
        newRoomOptions.isOpen = true;
        newRoomOptions.isVisible = true;
        newRoomOptions.maxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("BeginningRoom", newRoomOptions, null);

    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("JoinFailed was Hit");
        PhotonNetwork.CreateRoom("BeginningRoom");
    }

    void OnJoinedRoom()
    {
        // Spawn char etc.
        SpawnMyPlayer();
        gameStarted = true;
    }

    private void SpawnMyPlayer()
    {
        myPlayer = PhotonNetwork.Instantiate("Character", spawnPosition, Quaternion.identity, 0);
        myPlayer.GetComponentInChildren<PlayerMovement>().enabled = true;
        myPlayer.GetComponentInChildren<CameraScript>().enabled = true;
        myPlayer.GetComponentInChildren<Camera>().enabled = true;
        myPlayer.GetComponentInChildren<PolygonCollider2D>().enabled = true;
        myPlayer.GetComponentInChildren<AudioListener>().enabled = true;
        myPlayer.gameObject.transform.FindChild("Player").tag = "Player";

    }

    void OnPhotonPlayerDisconnected(PhotonPlayer other)
    { // When character closes game - doesn't leave dead character in there.
        PhotonNetwork.DestroyPlayerObjects(other);
    }
}
