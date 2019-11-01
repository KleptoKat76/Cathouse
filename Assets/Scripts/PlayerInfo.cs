using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private PlayerGameState[] allPlayers = new PlayerGameState[4];
    private ControlScheme keyboard;

    // Start is called before the first frame update
    void Start()
    {
        keyboard = new ControlScheme(PlayerController.Controller.keyboard);
        //make other 4 for joysticks
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
