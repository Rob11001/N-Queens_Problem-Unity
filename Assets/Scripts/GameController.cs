using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    [SerializeField]
    private Vector3 startPosition;
    private int N = 8;
    private Vector3[,] chessBoardPositions;
    private Queen[] queensPos;
    [SerializeField]
    private float Xoffset, Zoffset;
    private GameObject queen;
    private bool flag = false;

    
    // Start is called before the first frame update
    void Start()
    {
        chessBoardPositions = new Vector3 [N,N];
        queensPos = new Queen[N];
        for(int i = 0; i < N; i++) {
            queensPos[i] = new Queen();
            for(int j = 0; j < N; j++) 
                chessBoardPositions[i,j] = startPosition + new Vector3(-Xoffset*j, 0f, Zoffset*i);
        }
        
        queen = Resources.Load<GameObject>("queen");

        /* for(int i = 0; i < N; i++)
            for(int j = 0; j < N; j++) 
                Instantiate(queen,chessBoardPositions[i,j], Quaternion.identity); */
        
    }

    /**
    if(r==n+1)
         print Q[1…n]  
    else
        for j=1 to n 
          legal=true
          for i=1 to r-1
               if((Q[i]==j)∨(Q[i]==j+r-i)∨(Q[i]==j-r+i))
                    legal=false
          if legal
               Q[r]=j
               PlaceQueens(Q[1…n],r+1)
    */

    public void N_QueensBacktracking() {
        if(!flag) {
            flag = true;
            StartCoroutine("PlaceQueens", 0);
        }
    }

    private IEnumerator PlaceQueens(int n) {
        if(n == N) {
            // Algoritmo finito
            //Destroy(queensPos[n-1].gameObject);
            printChess();
            yield return new WaitForSeconds(2);
        } else {
            for(int j = 0; j < N; j++) {
                if(queensPos[n].gameObject != null)
                    Destroy(queensPos[n].gameObject);
                
                bool legal = true;
                
                for(int i = 0; i < n; i++) {
                    if((queensPos[i].Pos == j) || (queensPos[i].Pos == j + n - i) || (queensPos[i].Pos == j - n + i)) 
                        legal = false;
                }

                if(legal) {
                    queensPos[n].Pos = j;
                    queensPos[n].gameObject = Instantiate(queen, chessBoardPositions[n,j], Quaternion.identity);
                    Coroutine coroutine = StartCoroutine("PlaceQueens", n+1);
                    yield return coroutine;
                } else {
                    if(queensPos[n].gameObject != null)
                        Destroy(queensPos[n].gameObject); 
                }
            }
        }
    }

    private void printChess() {
        string str = "";
        for(int i = 0; i < N; i++) {
            for(int j = 0; j < N; j++) {
                if(queensPos[i].Pos == j) 
                    str += " Q";
                else
                    str += " X";
            }
            str += '\n';
        }
        Debug.Log(str);
    }
}
