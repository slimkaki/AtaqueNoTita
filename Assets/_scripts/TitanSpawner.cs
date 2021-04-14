using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanSpawner : MonoBehaviour {

    GameManager gm;
    List<Vector3> titanPos = new List<Vector3>();
    List<Vector3> genericPos = new List<Vector3>();
    public GameObject TitanPrefab;
    public GameObject GenericPrefab;
    void Start() {
        gm = GameManager.GetInstance();
        
        genericPos.Add(new Vector3(15.34f, -2.61f, 0f));
        titanPos.Add(new Vector3(63.81f, -3.34f, -10f));
        SpawnGeneric();
        SpawnSuperTitan();
    }

    public void SpawnGeneric() {
        GameObject[] actual_titans = GameObject.FindGameObjectsWithTag("GenericTitan");
        foreach (GameObject titan in actual_titans) {
            Destroy(titan);
        }

        foreach(Vector3 titan in genericPos) {
            Instantiate(GenericPrefab, titan, Quaternion.identity);
        }
    }
    public void SpawnSuperTitan() {
        GameObject[] actual_titans = GameObject.FindGameObjectsWithTag("Titan");
        foreach (GameObject titan in actual_titans) {
            Destroy(titan);
        }

        foreach(Vector3 titan in titanPos) {
            Instantiate(TitanPrefab, titan, Quaternion.identity);
        }
}
    void Update() {
        
        if (gm.gameState == GameManager.GameState.MENU){
            SpawnGeneric();
            SpawnSuperTitan();
        }
       
    }

}
