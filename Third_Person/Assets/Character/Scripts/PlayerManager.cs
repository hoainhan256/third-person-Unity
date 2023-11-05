using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerManager : NetworkBehaviour
{
    InputManagerment inputsystem;
    [SerializeField] private GameObject cameraPrefab;
    Quaternion TargetRotate;
    Quaternion PlayerRotate;
    [SerializeField] List<GameObject> OtherPlayer;
    CamSpawner ListPlayer;
    private void Awake()
    {
        ListPlayer = FindObjectOfType<CamSpawner>();

    }
    private void Start()
    {
        ListPlayer.Players.Add(this.gameObject);
        foreach(var player in ListPlayer.Players)
        {
            if(player != null)
            {

            }
        }
    }
}
