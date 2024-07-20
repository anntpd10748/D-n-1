using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Generic
{
    static public void VelocityLimit(float limit, Rigidbody2D rb)
    {
        int gravityMultiplier = (int)(Mathf.Abs(rb.gravityScale) / rb.gravityScale);
        if (rb.velocity.y * -gravityMultiplier > limit )
        {
            rb.velocity = Vector2.up * gravityMultiplier * -limit;
        }
    }
    static public void CreateGameMod(Rigidbody2D rb, PlayerController host, bool onGround, float initalVelocity,float gravityScale, bool canHold = false, bool flipOnClick = false, float rotationMod = 0, float yVelocityLimit = Mathf.Infinity)
    {
        if(!Input.GetKey(KeyCode.Space) || canHold && host.checkJump2())
        {
            host.clickProcessed = false;
        }

        rb.gravityScale = gravityScale * host.Gravity;
        VelocityLimit(yVelocityLimit, rb); 

        if(Input.GetKey(KeyCode.Space))
        {
            if(host.checkJump2() && !host.clickProcessed || !onGround && !host.clickProcessed)
            {
                host.clickProcessed = true;
                rb.velocity = Vector2.up * initalVelocity * host.Gravity;
                host.Gravity *= flipOnClick ? -1 : 1;
            }
        }
        if (host.checkJump2() || !onGround)
        {
            host.Sprite.rotation = Quaternion.Euler(0, 0, Mathf.Round(host.Sprite.rotation.z /90) * 90);
        }
        else
        {
            host.Sprite.Rotate(Vector3.back, rotationMod * Time.deltaTime * host.Gravity);
        }
    }
}
