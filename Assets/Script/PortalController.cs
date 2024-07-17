using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Gamemodes Gamemode;
    public Speeds Speed;
    public bool gravity;
    public int State;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            PlayerController movement = collision.gameObject.GetComponent<PlayerController>();
            movement.GotoPortal(Gamemode, Speed, gravity ? 1 : -1, State);
        }
        catch { }
    }
}
