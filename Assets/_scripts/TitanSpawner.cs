using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanSpawner : MonoBehaviour {

    GameManager gm;
    List<Vector3> titanPos = new List<Vector3>(); 
    public GameObject TitanPrefab;
    void Start() {
        gm = GameManager.GetInstance();
        
        titanPos.Add(new Vector3(16.34f, -2.61f, -10f));
        titanPos.Add(new Vector3(63.81f, -3.34f, -10f));
        Spawn();
    }

    public void Spawn() {
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
            Spawn();
        }
       
    }

}
