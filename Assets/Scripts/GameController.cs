using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    
    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private Text backtracks;
    [SerializeField]
    private Button forwardButton;
    private int N = 8;
    private Vector3[,] chessBoardPositions;
    private Queen[] queensPos;
    [SerializeField]
    private float Xoffset, Zoffset;
    private GameObject queen;
    private bool flag = false;
    private bool automatic = false;
    private Color defaultColor;

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
        defaultColor = forwardButton.image.color;
        
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
            printChess();
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        } else {
            for(int j = 0; j < N; j++) {
                if(queensPos[n].gameObject != null)
                    Destroy(queensPos[n].gameObject);
                
                bool legal = true;
                
                if(!automatic)
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.A));

                for(int i = 0; i < n; i++) {
                    if((queensPos[i].Pos == j) || (queensPos[i].Pos == j + n - i) || (queensPos[i].Pos == j - n + i)) 
                        legal = false;
                }

                if(legal) {
                    queensPos[n].Pos = j;
                    queensPos[n].gameObject = Instantiate(queen, chessBoardPositions[n,j], Quaternion.identity);
                    Coroutine coroutine = StartCoroutine("PlaceQueens", n+1);
                    yield return coroutine;
                    backtracks.gameObject.SetActive(true);
                    StartCoroutine("disableMessage");
                    yield return new WaitForSeconds(1f);

                } else {
                    if(queensPos[n].gameObject != null)
                        Destroy(queensPos[n].gameObject);
                }
            }
            Destroy(queensPos[n-1].gameObject);
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

    private IEnumerator disableMessage() {
        yield return new WaitForSeconds(0.5f);
        backtracks.gameObject.SetActive(false);
    }

    public void toogleAuto() {
        automatic = automatic == false;
        if(!automatic) {
            Color color = forwardButton.image.color;
            color.a = 255;
            forwardButton.image.color = color;
        } else {
            forwardButton.image.color = defaultColor;
        }
    }
}
