using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    public Transform Sprite;
    [SerializeField] private Rigidbody2D sprite;
    [SerializeField] private float junp = 26.6581f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            sprite.AddForce(Vector2.up * junp, ForceMode2D.Impulse);
        }
    }
}
