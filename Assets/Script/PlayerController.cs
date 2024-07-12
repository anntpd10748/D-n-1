using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Speed = 8;
    private float Jump = 8;
    [SerializeField] Rigidbody2D rg2d;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(Speed * Time.deltaTime,0));
        JumpController();
    }
    public void JumpController()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rg2d.velocity = new Vector2(rg2d.velocity.x,Jump);
        }
    }
}
