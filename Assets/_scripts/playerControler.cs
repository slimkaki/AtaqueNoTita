using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControler : MonoBehaviour
{
    GameManager gm;
    private float positionX, positionY;
    public float speed;
    public float jumpVelocity;
    private Rigidbody2D rb;
    private float lastHorizontal = 1.0f;
    public bool isGrounded = false;
    private bool isOnAir = false;
    Animator animator;
    public AudioClip shootSFX;
    private bool canKillTitan = false;


    // Start is called before the first frame update
    void Start() 
    {   
        gm = GameManager.GetInstance();
        rb = GetComponent<Rigidbody2D>();
        positionX = this.transform.position.x;
        positionY = this.transform.position.y;
        animator = GetComponent<Animator>();
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
     public void TakeDamage(int dano)
    {
       if(gm.vidas >= dano){
            gm.vidas-=dano;
       }else{
           gm.vidas = 0;
       }
       if (gm.vidas <= 0){
          Die();
        };
       if(gm.vidas <= 0 && gm.gameState == GameManager.GameState.GAME)
    {
        gm.ChangeState(GameManager.GameState.ENDGAME);
    }       
    }
    private void Die(){
        GetComponent<SpriteRenderer>().enabled = false;
    }
    void SearchAndDestroy() {
        // Fonte do código: https://forum.unity.com/threads/find-and-delete-closest-gameobject-with-tag-solved.419205/
        // Search for the nearest titan and destroy it
        GameObject[] actual_titans = GameObject.FindGameObjectsWithTag("Titan");
        float curDist = 1000000;
        GameObject titanToKill = null;
 
        foreach (GameObject titan in actual_titans) {
            float dist = Vector3.Distance(transform.position, titan.transform.position);
            if (dist < curDist) {
                curDist = dist;
                titanToKill = titan;
            }
        }
        if (titanToKill != null) {
            // Destroy(titanToKill);
            titanToKill.GetComponent<TitanController>().Die();
            // titanToKill.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
        canKillTitan = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {         
        if(gm.gameState != GameManager.GameState.GAME) return;
        if (this.transform.position.y < -6.0f) {
            // Debug.Log($"Die -> position.y = {this.transform.position.y}");
           TakeDamage(100);
           
        }

        if (Input.GetAxisRaw("Vertical") > 0f && isGrounded) {
            Jump();
        } 

        if (Input.GetKey(KeyCode.Q) && canKillTitan) {
            audioManeger.PlaySFX(shootSFX);
            SearchAndDestroy();
            gm.pontos+=1;
            Impulse();
        }

        if (canKillTitan) {
            gm.pressQ = true;
        } else {
            gm.pressQ = false;
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


        if (h != 0)
       {
           animator.SetFloat("velocity", 1.0f);
       }
       else
       {
           animator.SetFloat("velocity", 0.0f);
       }

        

        if(Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME) {
       gm.ChangeState(GameManager.GameState.PAUSE);
   }
        gm.time--;
        if(gm.time <= 0 && gm.gameState == GameManager.GameState.GAME)
        {

        gm.ChangeState(GameManager.GameState.ENDGAME);
        Die();
    }
    
        if(gm.vidas <= 0){
        if(gm.gameState == GameManager.GameState.GAME)
        {
            gm.ChangeState(GameManager.GameState.ENDGAME);
            Die();
        }
    }
    }
   void OnCollisionEnter2D(Collision2D collision) {        
        if (collision.gameObject.tag == "roofTop" || collision.gameObject.tag == "titanBack" || collision.gameObject.tag == "titanFront")
            this.isGrounded = true;
        if (collision.gameObject.tag == "titanBack")
            this.canKillTitan = true;
        if (collision.gameObject.tag == "Torre") {
            gm.ChangeState(GameManager.GameState.ENDGAME);
        }
        if (collision.gameObject.tag == "titanMouth")
            gm.vidas--;
    } 
 
    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "roofTop" || collision.gameObject.tag == "titanBack" || collision.gameObject.tag == "titanFront")
            this.isGrounded = false;
        if (collision.gameObject.tag == "titanBack")
            this.canKillTitan = false;
        

        // if (collision.gameObject.tag == "titanFront")
        //     TakeDamage(20);
    }
    
}
 
