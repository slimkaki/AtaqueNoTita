using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanController : MonoBehaviour {
    private bool isDying = false;
    private float tempoDaMorte;
    private float x, y, z;
    public GameObject fumaca;
    private float canIWalk, mySpeed;
    private GameObject eren;

    GameManager gm;

    void Start() {
        gm =  GameManager.GetInstance();
        canIWalk = Random.Range(0.0f, 1.0f);
        mySpeed = Random.Range(0.01f, 0.1f);
        eren = GameObject.FindWithTag("Player");
    }

    public void Die() {
        isDying = true;
        x = this.transform.position.x;
        y = this.transform.position.y;
        z = this.transform.position.z;
        tempoDaMorte = Time.time;
        Quaternion smokeRotation = Quaternion.Euler(-90, 0, 0);
        Instantiate(fumaca, new Vector3(this.transform.position.x, this.transform.position.y - 3f, 0), smokeRotation);

    }

 
    void Update() {
        if (isDying) {
            if (Time.time - tempoDaMorte > 5.0f)
                Destroy(gameObject);
            x = this.transform.position.x;
            y = this.transform.position.y;
            z = this.transform.position.z;
            this.transform.position = new Vector3(x-0.05f, y-0.05f, z);
        }
        
    }
    
    void FixedUpdate() {
        if(gm.gameState != GameManager.GameState.GAME) return;
        if (canIWalk > 0.4f) { 
            TitanWalk();
        }
    }

    void TitanWalk() {
        
        if (this.transform.position.x - eren.transform.position.x < 9f && this.transform.position.x - eren.transform.position.x >= 1f) {
            this.transform.position = new Vector3(this.transform.position.x-mySpeed, this.transform.position.y, this.transform.position.z);
        }
    } 
}
