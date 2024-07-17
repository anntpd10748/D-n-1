using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;



public class PlayerController : MonoBehaviour
{   
    private float Speed = 8;
    private float Jump = 8;
    private bool isGrounded;
    private BoxCollider2D coll2d;
    public Transform Sprite;
    private Rigidbody2D rg2d;
    [SerializeField] private LayerMask Ground;
    // Start is called before the first frame update
    public float maxTimer = 0.5f;
    float[] SpeedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };
    public Speeds CurrentSpeed;
    void Start()
    {
        coll2d = GetComponent<BoxCollider2D>();
        rg2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.deltaTime;
        if (checkJump())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            Sprite.rotation = Quaternion.Euler(Rotation);

            JumpController();
        }
        else
        {
            Sprite.Rotate(Vector3.back * 3);
        }
    }
 
    public void JumpController()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rg2d.velocity = Vector2.zero;
            rg2d.AddForce(Vector2.up * Jump, ForceMode2D.Impulse);
        }
    }
    private bool checkJump()
    {
        return Physics2D.BoxCast(coll2d.bounds.center, coll2d.bounds.size, 0f, Vector2.down, 1f, Ground);
    }
}
