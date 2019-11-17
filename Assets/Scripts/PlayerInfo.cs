using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    private static readonly int MAX_PLAYERS_POSSIBLE = 4;
    private PlayerController[] allPlayers = new PlayerController[MAX_PLAYERS_POSSIBLE];
    private int[] playerScores = new int[] {-1, -1, -1, -1};
    private bool gameOver = false;
    private ControlScheme[] controlSchemes = new ControlScheme[5];

    // Start is called before the first frame update
    void Start()
    {
        //KeyBoard input
        var kbControls = gameObject.AddComponent<ControlScheme>();
        kbControls.SetControlScheme(ControlScheme.Controller.keyboard);
        //make other 4 for joysticks
        var playerControls = GameObject.FindObjectsOfType<PlayerController>();
        for (int i = 0; i < playerControls.Length; i++)
        {
            allPlayers[i] = playerControls[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Rturns playerID of a player if player won, returns null if draw or everyone's dead somehow, and returns 0 if game is not over yet
        var gameStateResult = CheckForWinner();
        if(gameStateResult == null)
        {
            //do draw
            print("Draw");
            gameOver = true;
        }
        else if(gameStateResult == 0)
        {           
        }
        else
        {
            //Specific player won
            //Add to player's points and display victory screen
            Reset();
        }
    }
    private void Reset()
    {
        gameOver = false;

        //Load gun level up screen. 
        
    }
    private PlayerController.PlayerID? CheckForWinner()
    {
        ArrayList alivePlayers = new ArrayList();
        foreach (PlayerController e in allPlayers)
        {
            if (e != null)
            {
                if (!e.isDead())
                {
                    alivePlayers.Add(e.GetID());
                }
            }
        }
        if(alivePlayers.Count == 1)
        {
            //One player won. Display Last player alive's id
            //change player score
            return (PlayerController.PlayerID)alivePlayers[0];
        }
        else if(alivePlayers.Count <= 0)
        {
            //Either draw or something funky happened. Display draw.
            //Neither player scores point
            return null;
        }
        return 0;
    }
}
