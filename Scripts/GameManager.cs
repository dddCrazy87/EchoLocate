using System.Collections;
using System.Collections.Generic;
using Scream.UniMO.Common;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Transform[] positions;
    [SerializeField] private Transform[] players;
    [SerializeField] private Transform treasure, exit;
    void Start()
    {
        // randomly shuffle an array positions
        int PosLen = positions.Length;
        int PosID = Random.Range(0, PosLen);
        
        Transform[] children = positions[PosID].GetComponentsInChildren<Transform>();
        for (int i = 1; i < children.Length; i ++) {
            int RandIndex = Random.Range(1, children.Length);
            Transform tmp = children[i];
            children[i] = children[RandIndex];
            children[RandIndex] = tmp;
        }
        
        // randomly set the position of players
        for(int i = 0 ; i < players.Length; i ++) {
            players[i].position = children[i + 1].position;
        }
        
        // randomly set the position of treasure
        treasure.position = children[children.Length - 2].position;

        // randomly set the position of treasure
        exit.position = children[children.Length - 1].position;
    }
}
