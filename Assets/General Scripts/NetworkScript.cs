﻿using System;
using Assets;
using UnityEngine;
using System.Collections;

public class NetworkScript : MonoBehaviour
{
    public Vector2 spawnPosition;
    private RoomOptions newRoomOptions;

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
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
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
    }

    private void SpawnMyPlayer()
    {
        myPlayer = PhotonNetwork.Instantiate("Character", spawnPosition, Quaternion.identity, 0);
        myPlayer.GetComponentInChildren<PlayerMovement>().enabled = true;
        myPlayer.GetComponentInChildren<CameraScript>().enabled = true;
        myPlayer.GetComponentInChildren<Camera>().enabled = true;
        myPlayer.tag = "Player";
    }
}
