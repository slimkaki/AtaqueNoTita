using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControler : MonoBehaviour
{
    GameManager gm;
    private float positionX, positionY;
    public float speed, jumpVelocity;
    private Rigidbody2D rb;
    private float lastHorizontal = 1.0f;
    public bool isGrounded = false;
    private bool isOnAir = false;
    Animator animator;
    public AudioClip shootSFX, swordSwoosh, mikasa_scream; 
    private bool canKillTitan = false;
    public GameObject floatingDamage;
    public float attackRate= 4f;
    float nextAttackTime = 0f;
    float lastGasReload = 0f;
    Vector2 velocitySave = new Vector2(0, 0);
    private AudioSource gas_sfx;

    void Start() {   
        gm = GameManager.GetInstance();
        rb = GetComponent<Rigidbody2D>();
        positionX = this.transform.position.x;
        positionY = this.transform.position.y;
        animator = GetComponent<Animator>();
        gas_sfx = GetComponent<AudioSource>();
    }

    void Jump() {
        // rb.AddForce(Vector2.up*jumpVelocity, ForceMode2D.Impulse);
        rb.velocity = new Vector2(rb.velocity.x, 10);
        isOnAir = true;
    }
    void Impulse() {
        // rb.AddForce(Vector2.up*jumpVelocity, ForceMode2D.Impulse);
        gas_sfx.Play();
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
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    bool IsTitanOnScreen() {
        List<GameObject> final_titans = new List<GameObject>();

        GameObject[] actual_titans = GameObject.FindGameObjectsWithTag("Titan") ;
        GameObject[] actual_titans_2 = GameObject.FindGameObjectsWithTag("GenericTitan");
        foreach(GameObject berthold in actual_titans) {
            final_titans.Add(berthold);
        }
        foreach(GameObject generic in actual_titans_2) {
            final_titans.Add(generic);
        }

        float curDist = 1000000; 
        GameObject titanToKill = null;
 
        foreach (GameObject titan in final_titans) {
            float dist = Vector3.Distance(transform.position, titan.transform.position);
            if (dist < curDist) {
                curDist = dist;
                titanToKill = titan;
            }
        }
        if (titanToKill.transform.position.x - this.transform.position.x <= 5f) {
            return true;
        }
        return false;
    }
    void SearchAndDestroy() {
        // Fonte do código: https://forum.unity.com/threads/find-and-delete-closest-gameobject-with-tag-solved.419205/
        // Search for the nearest titan and destroy it
        List<GameObject> final_titans = new List<GameObject>();

        GameObject[] actual_titans = GameObject.FindGameObjectsWithTag("Titan") ;
        GameObject[] actual_titans_2 = GameObject.FindGameObjectsWithTag("GenericTitan");
        foreach(GameObject berthold in actual_titans) {
            final_titans.Add(berthold);
        }
        foreach(GameObject generic in actual_titans_2) {
            final_titans.Add(generic);
        }

        float curDist = 1000000; 
        GameObject titanToKill = null;
 
        foreach (GameObject titan in final_titans) {
            float dist = Vector3.Distance(transform.position, titan.transform.position);
            if (dist < curDist) {
                curDist = dist;
                titanToKill = titan;
            }
        }
        if (titanToKill != null) {
            titanToKill.GetComponent<TitanController>().Die();
        }
        canKillTitan = false;
    }

    public void DestroySmoke() {
        GameObject[] smokes = GameObject.FindGameObjectsWithTag("Fumaca");
        foreach(GameObject smoke in smokes) {
            Destroy(smoke);
        }
    }

    void PauseErenPhysics() {
        rb.gravityScale = 0;
        velocitySave = rb.velocity;
        rb.velocity = new Vector2(0, 0);
    }
    public void UnpauseErenPhysics() {
        rb.gravityScale = 4;
        rb.velocity = velocitySave;
    }

    void Update() {
        if(gm.gameState != GameManager.GameState.GAME) return;
        if(gm.vidas <= 0){
            if(gm.gameState == GameManager.GameState.GAME) {
                Die();
                gm.ChangeState(GameManager.GameState.ENDGAME);
                
            }
        }
        // Tutorial
        if  (gm.firstPlay[0]) {
            // Tutorial inicial: teclas e comandos disponíveis
            PauseErenPhysics();
            gm.ChangeState(GameManager.GameState.TUTORIAL);
            gm.firstPlay[0] = false;
            return;
        }

        if (gm.firstPlay[2]) {
            // Tutorial: avisa como matar titãs
            if (IsTitanOnScreen()) {
                PauseErenPhysics();
                gm.ChangeState(GameManager.GameState.TUTORIAL);
                gm.firstPlay[2] = false;
                return;
            }
        }
        if (gm.gas <= 0 && gm.firstPlay[1]) {
            // Tutorial: Avisa como recarrega o gás
            PauseErenPhysics();
            gm.ChangeState(GameManager.GameState.TUTORIAL);
            gm.firstPlay[1] = false;
            return;
        }

    }

    void FixedUpdate() {         
        if(gm.gameState != GameManager.GameState.GAME) return;
        
        // Checa altitude do eren para ver se ele deveria morrer
        if (this.transform.position.y < -6.0f) {
            gm.vidas = 0;
            gm.ChangeState(GameManager.GameState.ENDGAME);
        }

        // ataque apenas duas vezes por segundo -> https://www.youtube.com/watch?v=sPiVz1k-fEs
        if(Time.time >= nextAttackTime){
            if (Input.GetKey(KeyCode.Q)) {
                nextAttackTime = Time.time + 1.0f / attackRate;
                animator.SetTrigger("atk");
                if (canKillTitan){
                    audioManeger.PlaySFX(shootSFX);
                    SearchAndDestroy();
                    gm.pontos+=1;
                    Impulse();
                } else {
                    audioManeger.PlaySFX(swordSwoosh);
                }
                
            }

        }

        // Caso do pulo básico ou salto do personagem
        if (Input.GetAxisRaw("Vertical") > 0f && isGrounded) {
            Jump();
            isGrounded = false;
        } 

        if ((Time.time - lastGasReload >= 5f) && (Input.GetKey(KeyCode.R))) {
            gm.tanque_de_gas--;
            gm.gas = 100;
            lastGasReload = Time.time;
        }

        if (canKillTitan) {
            gm.pressQ = true;
        } else {
            gm.pressQ = false;
        }

        if (Input.GetAxisRaw("Jump") > 0f && isOnAir && gm.gas >= 10) {
            Impulse();
            gm.gas -= 10;
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

        

        if(Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME)
        {
            gm.ChangeState(GameManager.GameState.PAUSE);
        }
        
    
        if(gm.vidas <= 0){
            if(gm.gameState == GameManager.GameState.GAME)
            {
                Die();
                gm.ChangeState(GameManager.GameState.ENDGAME);
                
            }
        }
    }
   void OnCollisionEnter2D(Collision2D collision) {        
        if (collision.gameObject.tag == "roofTop" || collision.gameObject.tag == "titanBack" || collision.gameObject.tag == "titanFront")
            this.isGrounded = true;
        if (collision.gameObject.tag == "titanBack")
            this.canKillTitan = true;
        if (collision.gameObject.tag == "Torre") {
            audioManeger.PlaySFX(mikasa_scream);
            gm.ChangeState(GameManager.GameState.ENDGAME);
        }
        if (collision.gameObject.tag == "titanMouth")
            gm.vidas=0;
        if (collision.gameObject.tag == "TitanHand" && gm.titanAtk){
            Instantiate(floatingDamage, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y+2f, this.transform.position.z), Quaternion.identity);
            gm.trapped = true;
            gm.vidas-=4;
            rb.velocity = new Vector2(-160, 16 );
            gm.titanAtk = false;
        }
    } 
 
    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "roofTop" || collision.gameObject.tag == "titanBack" || collision.gameObject.tag == "titanFront")
            this.isGrounded = false;
        if (collision.gameObject.tag == "titanBack")
            this.canKillTitan = false;
    }
    
}
 
