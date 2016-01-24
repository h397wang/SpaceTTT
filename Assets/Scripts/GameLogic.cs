using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameLogic : MonoBehaviour
{

    public int[,,] board = new int[4, 4, 4];
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void reInitializeArray()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k =0; k<4; k++)
                {
                    board[i, j, k] = 0;
                }
            }
        }
    }
    public bool insertMark(int mark, int x, int y, int z)
    {
        if (board[x, y, z] != 1 && board[x, y, z] != 2)
            board[x, y, z] = mark;
        else
            //Debug.Log("That spot is taken");
            return false;
        return true;
    }
    public bool isEmpty(int[,,] board)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    if (board[i, j, k] != 0) return false;
                }
            }
        }
        return true;
    }
    public bool isWin(int mark)
    {
        int sameCount = 0;
        int intMark = mark;

        if (!isEmpty(board))
        {
            //Check all wins going in z direction
            //Go through the horizontal planes on the x-y plane
            for (int i = 0; i < 4; i++)
            {
                //Go through the rows on the x-z plane
                for (int j = 0; j < 4; j++)
                {
                    sameCount = 0;

                    //Go through the cells in z-direction
                    for (int k = 0; k < 4; k++)
                    {
                        if (board[i, j, k] != intMark) break;
                        else sameCount++;
                    }
                    if (sameCount == 4) return true;
                }
            }

            //Check all wins going in y direction
            //Go through the vertical planes on the x-y plane
            for (int i = 0; i < 4; i++)
            {
                //Go through the columns on the x-z plane
                for (int j = 0; j < 4; j++)
                {
                    sameCount = 0;
                    
                    //Go through the cells in the y-direction
                    for (int k = 0; k < 4; k++)
                    {
                        if (board[k, i, j] != intMark) break;
                        else sameCount++;
                    }
                    if (sameCount == 4) return true;
                }
            }

            //Check all wins going in x direction
            //Go through the horizontal planes on the x-z plane
            for (int i = 0; i < 4; i++)
            {
                //Go through the rows on the x-y plane
                for (int j = 0; j < 4; j++)
                {
                    sameCount = 0;
                    //Go through the cells in the x-direction
                    for (int k = 0; k < 4; k++)
                    {
                        if (board[j, k, i] != intMark) break;
                        else sameCount++;
                    }
                    if (sameCount == 4) return true;

                }
            }
            sameCount = 0;

            //Diagonals starting from the bottom edge 1
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j, j] != intMark) break;
                    else sameCount++;
                }
                if (sameCount == 4) return true;
            }

            //Diagonals starting from the bottom edge 2
            for (int i = 0; i < 4; i++)
            {
                if (board[0, i, 0] != intMark) continue;
                for (int j = 0; j < 4; j++)
                {
                    if (board[j, i, j] != intMark) break;
                    else sameCount++;
                }
                if (sameCount == 4) return true;
            }

            //Diagonals starting from the bottom edge 3
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, 3 - j, j] != intMark) break;
                    else sameCount++;
                }
                if (sameCount == 4) return true;
            }

            //Diagonals starting from the bottom edge 4
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[3 - j, i, j] != intMark) break;
                    else sameCount++;
                }
                if (sameCount == 4) return true;
            }

            //Diagonal starting from the bottom corner 1
            if (board[0, 0, 0] == intMark)
            {
                sameCount = 0;
                for (int i = 1; i < 4; i++)
                {
                    if (board[i, i, i] != intMark) break;
                    else sameCount++;
                }
                if (sameCount == 4) return true;
            }

            //Diagonal starting from the bottom corner 2
            if (board[3, 0, 0] == intMark)
            {
                sameCount = 0;
                for (int i = 1; i < 4; i++)
                {
                    if (board[3 - i, i, i] != intMark) break;
                    sameCount++;
                }
                if (sameCount == 4) return true;
            }

            //Diagonal starting from the bottom corner 3
            if (board[0, 3, 0] == intMark)
            {
                sameCount = 0;
                for (int i = 1; i < 4; i++)
                {
                    if (board[i, 3 - i, i] != intMark) break;
                    sameCount++;
                }
                if (sameCount == 4) return true;
            }

            //Diagonals starting from the bottom corner 4
            if (board[3, 3, 0] == intMark)
            {
                sameCount = 0;
                for (int i = 1; i < 4; i++)
                {
                    if (board[3 - i, 3 - i, i] != intMark) break;
                    sameCount++;
                }
                if (sameCount == 4) return true;
            }

        }
        return false;
    }

}

