using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;

public class GameHUDView : MonoBehaviour
{
    [SerializeField] private Text player1Score;
    [SerializeField] private Text player2Score;
    [SerializeField] private Text player3Score;
    [SerializeField] private Text player4Score;

    Dictionary<string, Text> tagToScoreTextDict;

    private void Start()
    {
        tagToScoreTextDict = new Dictionary<string, Text>(){
                { Constants.TAG_PLAYER_1, player1Score },
                { Constants.TAG_PLAYER_2, player2Score },
                { Constants.TAG_PLAYER_3, player3Score },
                { Constants.TAG_PLAYER_4, player4Score }
        };


    }
}
