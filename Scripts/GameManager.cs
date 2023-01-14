using System.Collections;
using System.Collections.Generic;
using Scream.UniMO.Common;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Transform[] positions;
    [SerializeField] private Transform[] players;
    [SerializeField] private Transform treasure, exit;
    private void Start()
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

    private float TimePassed = 0;
    private bool DoOnetime = true;
    [SerializeField] private Transform[] TreasureCompass, ExitCompass;
    private void Update() {
        if(DoOnetime) {
            GenCompass();
            TimePassed += Time.deltaTime;
        }
        if(TimePassed > 5f) {
            DoOnetime = false;
            TimePassed = 0;
            DesCompass();
        }
    }

    // generate campass
    private void GenCompass() {
        for(int i = 0; i < players.Length; i ++) {
            float x = treasure.position.x - players[i].position.x;
            float y = treasure.position.y - players[i].position.y;
            string name = "Compass";
            if(x > 0 && y > 0) name += 1.ToString();
            if(x < 0 && y > 0) name += 2.ToString();
            if(x < 0 && y < 0) name += 3.ToString();
            if(x > 0 && y < 0) name += 4.ToString();
            for(int j = 0; j < TreasureCompass[i].childCount; j ++) {
                if(TreasureCompass[i].GetChild(j).name == name) {
                    TreasureCompass[i].GetChild(j).gameObject.SetActive(true);
                }
                else {
                    TreasureCompass[i].GetChild(j).gameObject.SetActive(false);
                }
            }

            x = exit.position.x - players[i].position.x;
            y = exit.position.y - players[i].position.y;
            name = "Compass";
            if(x > 0 && y > 0) name += 1.ToString();
            if(x < 0 && y > 0) name += 2.ToString();
            if(x < 0 && y < 0) name += 3.ToString();
            if(x > 0 && y < 0) name += 4.ToString();
            for(int j = 0; j < ExitCompass[i].childCount; j ++) {
                if(ExitCompass[i].GetChild(j).name == name) {
                    ExitCompass[i].GetChild(j).gameObject.SetActive(true);
                }
                else {
                    ExitCompass[i].GetChild(j).gameObject.SetActive(false);
                }
            }
        }
    }

    private void DesCompass() {
        for(int i = 0; i < players.Length; i ++) {
            Destroy(TreasureCompass[i].gameObject);
            Destroy(ExitCompass[i].gameObject);
        }
    }
}
