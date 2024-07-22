using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Gamemodes Gamemode;
    public Speeds Speed;
    public bool gravity;
    public int State;

    public void initiatePortal(PlayerController movement)
    {
        movement.GotoPortal(Gamemode, Speed, gravity ? 1 : -1, State);
    }
}
