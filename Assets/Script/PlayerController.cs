using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Fastest = 4 };
public enum Gamemodes { Cube = 0, Ship = 1, Saw = 2 };


public class PlayerController : MonoBehaviour
{
   
    public float GroundCheckRadius;
    public LayerMask GroundMask;
    private float Jump = 8;
    private BoxCollider2D coll2d;
    public Transform Sprite;
    private Rigidbody2D rg2d;
    [SerializeField] private LayerMask Ground;
    float[] SpeedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };
    public Speeds CurrentSpeed;
    public Gamemodes CurrentGameMode;
    public int Gravity = 1;
    public bool clickProcessed = false;
    void Start()
    {
        coll2d = GetComponent<BoxCollider2D>();
        rg2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;
        Invoke(CurrentGameMode.ToString(),0);
    }

    void Ship()
    {
        rg2d.gravityScale = 2.93f * (Input.GetKey(KeyCode.Space) ? -1 : 1) * Gravity;
        Generic.VelocityLimit(9.95f, rg2d);
        Sprite.rotation = Quaternion.Euler(0, 0, rg2d.velocity.y * 2);
    }

    void Saw()
    {
        Generic.CreateGameMod(rg2d, this, true, 238.29f, 6.2f, false, true, 0, 238.29f);
    }

    void Cube()
    {
        Generic.CreateGameMod(rg2d, this, true, 19.5269f, 9.057f, true, false, 409.1f);
    }

    public void JumpController()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rg2d.velocity = Vector2.zero;
            rg2d.AddForce(Vector2.up * 26.6581f * Gravity, ForceMode2D.Impulse);
        }
    }

    public bool checkJump()
    {
        return Physics2D.BoxCast(coll2d.bounds.center, coll2d.bounds.size, 0f, Vector2.down, 1f, Ground);
    }

    public bool checkJump2()
    {
        return Physics2D.OverlapBox(transform.position + Vector3.down * Gravity * 0.5f, Vector2.right * 1.1f + Vector2.up * GroundCheckRadius, 0, GroundMask);
    }

    public void GotoPortal(Gamemodes Gamemode, Speeds Speed, int gravity, int State)
    {
        switch (State)
        {
            case 0:
                CurrentSpeed = Speed;
                break;
            case 1:
                CurrentGameMode = Gamemode;
                break;
            case 2:
                Gravity = gravity;
                rg2d.gravityScale = Mathf.Abs(rg2d.gravityScale) * gravity;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PortalController portal = collision.gameObject.GetComponent<PortalController>();
        if(portal)
        {
            portal.initiatePortal(this);
        }
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Spike"))
        {
            LoadScene(5);
        }
        if (collision.gameObject.tag.Equals("Finish"))
        {
            LoadScene(6);
        }
    }
}
