using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Fastest = 4 };
public enum Gamemodes { Cube = 0, Ship = 1 };


public class PlayerController : MonoBehaviour
{
    public Transform GroundCheckTransform;
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
    int Gravity = 1;
    void Start()
    {
        coll2d = GetComponent<BoxCollider2D>();
        rg2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;
        Invoke(CurrentGameMode.ToString(),0);
    }

    void Ship()
    {
        Sprite.rotation = Quaternion.Euler(0, 0, rg2d.velocity.y * 2);

        if (Input.GetKey(KeyCode.Space))
            rg2d.gravityScale = -4.314969f;
        else
            rg2d.gravityScale = 4.314969f;

        rg2d.gravityScale = rg2d.gravityScale * Gravity;
    }

    void Cube()
    {
        rg2d.gravityScale = 12.41067f * Gravity;

        if (checkJump())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            Sprite.rotation = Quaternion.Euler(Rotation);

            JumpController();
        }
        else
        {
            Sprite.Rotate(Vector3.back, 452.4152186f * Time.deltaTime * Gravity);
        }
    }

    public void JumpController()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rg2d.velocity = Vector2.zero;
            rg2d.AddForce(Vector2.up * 26.6581f * Gravity, ForceMode2D.Impulse);
        }
    }

    bool OnGround()
    {
        return Physics2D.OverlapBox(GroundCheckTransform.position + Vector3.up - Vector3.up * (Gravity - 1 / -2), Vector2.right * 1.1f + Vector2.up * GroundCheckRadius, 0, GroundMask);
    }

     bool checkJump()
    {
        return Physics2D.BoxCast(coll2d.bounds.center, coll2d.bounds.size, 0f, Vector2.down, 1f, Ground);
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
}
