using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanController : MonoBehaviour {
    private bool isDying = false;
    private float tempoDaMorte;
    private float x, y, z;
    public GameObject fumaca;

    void Start() {
        
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
}
