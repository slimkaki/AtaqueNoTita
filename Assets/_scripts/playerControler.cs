using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour
{
    // GameManager gm;
    private float positionX, positionY;
    public float speed;
    public float jumpVelocity;
    public Rigidbody2D rb;
    private float lastHorizontal = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        // gm = GameManager.GetInstance();
        positionX = this.transform.position.x;
        positionY = this.transform.position.y;
        
    }

    void Jump() {
        rb.AddForce(Vector2.up*jumpVelocity, ForceMode2D.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {         
        if (Input.GetAxisRaw("Jump") != 0) {
            Jump();
        }
        
        float h = Input.GetAxisRaw("Horizontal");
        float horizontal = h * speed * Time.deltaTime;
        if ((lastHorizontal == 1.0f && h == -1.0f) || (lastHorizontal == -1.0f && h == 1.0f)) {
            this.transform.Rotate(this.transform.rotation.x, -180f, this.transform.rotation.z);
        } 
        this.transform.position = new Vector3(this.transform.position.x + horizontal, this.transform.position.y, this.transform.position.z);
        if (h != 0.0f && lastHorizontal != h) {
            lastHorizontal = h;
        }
    }

  
    // }

    // void OnTriggerEnter2D(Collider2D collision) {
    //     if (collision.CompareTag("roofTop")) {// && !jumpFlag) {
    //         jumpFlag = false;
    //     }
    // }
    
}

