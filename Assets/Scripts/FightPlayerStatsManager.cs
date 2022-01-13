using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FightPlayerStatsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text playerNameTxt;
    public Text playerHPTxt;
    public Text playerMPTxt;
    public Text playerStatusTxt;
    private Entity player;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player = FightManager.player;
        playerNameTxt.text = player.Name;

        playerHPTxt.text = $"HP: {player.Statistics["Hp"]} / {player.Statistics["HpMax"]}";
        if (((float)player.hp / player.hpMax) <= 0.1)
            playerHPTxt.color = Color.red;
        else if (((float)player.hp / player.hpMax) <= 0.25)
            playerHPTxt.color = Color.yellow;
        else
            playerHPTxt.color = Color.white;
        playerMPTxt.text = "MP: " + player.mp.ToString() + "/" + player.mpMax.ToString();
        if (((float)player.mp / player.mpMax) <= 0.1)
            playerMPTxt.color = Color.red;
        else if (((float)player.mp / player.mpMax) <= 0.25)
            playerMPTxt.color = Color.yellow;
        else
            playerHPTxt.color = Color.white;

        string statusText = "";
        switch(player.entityStatus)
        {
            case Status.NONE:
                break;
            case Status.CONFUSED:
                statusText = "<color=#FFA500>CONFUSED</color>";
                break;
            case Status.POISONED:
                statusText = "<color=purple>POISONED</color>";
                break;
            case Status.BURNED:
                statusText = "<color=red>IN FIRE</color>";
                break;
            case Status.PARALIZED:
                statusText = "<color=yellow>PARALIZED</color>";
                break;
            default:
                break;
        }
        playerStatusTxt.text = statusText;
    }
}
