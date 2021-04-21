using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Player player;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

   void Update()
    {
        if (player.StartGame == true)
            StartGame();
        if (player.PlayerVelocity == -1)
            GameEndedWin();
        if (player.PlayerVelocity == -2)
            GameEndedLose();
    }
   public void GameEndedWin()
    {
        anim.SetBool("GameEndedWin", true);
    }

    public void GameEndedLose()
    {
        anim.SetBool("GameEndedLose", true);
    }

    public void StartGame()
    {
        anim.SetBool("StartGame", true);
    }
}
