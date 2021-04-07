using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour
{
    // GameManager gm;
    private float positionX, positionY;
    public float speed;
    public float jumpVelocity;
    private Rigidbody2D rb;
    private float lastHorizontal = 1.0f;
    public bool isGrounded = false;
    private bool isOnAir = false;

    // Start is called before the first frame update
    void Start() 
    {   
        // gm = GameManager.GetInstance();
        rb = GetComponent<Rigidbody2D>();
        positionX = this.transform.position.x;
        positionY = this.transform.position.y;
    }

    void Jump() {
        // rb.AddForce(Vector2.up*jumpVelocity, ForceMode2D.Impulse);
        rb.velocity = new Vector2(rb.velocity.x, 10);
        isOnAir = true;
    }
    void Impulse() {
        // rb.AddForce(Vector2.up*jumpVelocity, ForceMode2D.Impulse);
        rb.velocity = new Vector2(rb.velocity.x*9, 16 );
        isOnAir = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {         
        if (Input.GetAxisRaw("Vertical") > 0f && isGrounded) {
            Jump();
        } 
        if (Input.GetAxisRaw("Jump") > 0f && isOnAir) {
            Impulse();
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
  

    void OnCollisionEnter2D(Collision2D collision) {        
        Debug.Log($"collision.collider.name: {collision.collider.name}");
        if(collision.gameObject.tag == "roofTop")
        {
        this.isGrounded = true;
        }
    } 
 
    void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.tag == "roofTop")
        {
        this.isGrounded = false;
        }
        }
    
}
 
