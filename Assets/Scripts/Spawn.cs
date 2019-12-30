using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    //Reference from Unity IDE
    public GameObject GameObjectChessman;

    //Matrix creation
    private GameObject[,] positions = new GameObject[8,8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    //Whose turn
    private string currentPlayer = "black";

    //Game Ending
    private bool gameOver = false;

    public void Start()
    {
        playerBlack = new GameObject[] { Create("black_rook", 0, 0), Create("black_knight",1,0), Create("black_bishop",2,0), Create("black_queen",3,0), Create("black_king",4,0), Create("black_bishop",5,0), Create("black_knight",6,0), Create("black_rook",7,0), Create("black_pawn", 0, 1), Create("black_pawn", 1, 1), Create("black_pawn", 2, 1), Create("black_pawn", 3, 1), Create("black_pawn", 4, 1), Create("black_pawn", 5, 1), Create("black_pawn", 6, 1), Create("black_pawn", 7, 1) };
        playerWhite = new GameObject[] { Create("white_rook", 0, 7), Create("white_knight", 1, 7), Create("white_bishop", 2, 7), Create("white_queen", 3, 7), Create("white_king", 4, 7), Create("white_bishop", 5, 7), Create("white_knight", 6, 7), Create("white_rook", 7, 7), Create("white_pawn", 0, 6), Create("white_pawn", 1, 6), Create("white_pawn", 2, 6), Create("white_pawn", 3, 6), Create("white_pawn", 4, 6), Create("white_pawn", 5, 6), Create("white_pawn", 6, 6), Create("white_pawn", 7, 6) };

        //Set all piece positions on positions board
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(GameObjectChessman, new Vector3(0, 0, 0), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        //Overwrites either empty space or whatever was there
        positions[cm.GetXBoard(),cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        //Sets position null
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white") 
        { 
            currentPlayer = "black"; 
        } 
        else 
        { 
            currentPlayer = "white"; 
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;

        //Using UnityEngine.UI is needed here
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            //Using UnityEngine.SceneManagement is needed here
            SceneManager.LoadScene("Game"); //Restarts the game by loading the scene over again
        }
    }

    public bool PieceOnBoard(int x, int y, string name)
    {
        if (!PositionOnBoard(x, y)) {
            return false;
        }

        return GetPosition(x + 1, y + 1).name == name;
    }
}
