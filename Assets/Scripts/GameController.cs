using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
    A script that controls the whole application
*/
public class GameController : MonoBehaviour
{
    
    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private Text backtracks;
    [SerializeField]
    private Button forwardButton;
    [SerializeField]
    private GameObject panel;
    private int N = 8;
    private Vector3[,] chessBoardPositions;
    private Queen[] queensPos;
    [SerializeField]
    private float Xoffset, Zoffset;
    private GameObject queen;
    private bool flag = false;
    private bool automatic = true;
    private Color defaultColor;

    void Start()
    {
        // Initialization

        chessBoardPositions = new Vector3 [N,N];
        queensPos = new Queen[N];
        
        for(int i = 0; i < N; i++) {
            queensPos[i] = new Queen();
            for(int j = 0; j < N; j++) 
                chessBoardPositions[i,j] = startPosition + new Vector3(-Xoffset*j, 0f, Zoffset*i);
        }
        
        // Load prefab
        queen = Resources.Load<GameObject>("queen");
        defaultColor = forwardButton.image.color;
        
    }

    /**
    PLACEQUEENS
    
    The idea of the backtracking algorithm is quite simple.
    We can place a only Queen in each row, so we can only decide
    in which column place the i-th Queen.
    Therefore the algorithm find a legal position for the i-th Queen,
    then place it and recall the recursive algorithm for place the i+1 Queen.
    When there are no "legal" position the algorithm "backtracks".
    */

    /**
        Function which calls the coroutine of the algorithm
    */
    public void N_QueensBacktracking() {
        if(!flag) {
            flag = true;
            StartCoroutine("PlaceQueens", 0);
        }
    }


    /**
        Backtracking algorithm
    */
    private IEnumerator PlaceQueens(int n) {
        if(n == N) {
            printChess();
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // After found a solution press Space to pass to next
        } else {
            // Loop that checks the position in which the n-queens could be place
            for(int j = 0; j < N; j++) {

                if(queensPos[n].gameObject != null)
                    Destroy(queensPos[n].gameObject);
                
                bool legal = true;
                
                if(!automatic)
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.A));

                // Checks if the j position is "legal"
                for(int i = 0; i < n; i++) {
                    if((queensPos[i].Pos == j) || (queensPos[i].Pos == j + n - i) || (queensPos[i].Pos == j - n + i)) 
                        legal = false;
                }

                if(legal) {
                    // Place the queen and branch
                    queensPos[n].Pos = j;
                    queensPos[n].gameObject = Instantiate(queen, chessBoardPositions[n,j], Quaternion.identity);

                    Coroutine coroutine = StartCoroutine("PlaceQueens", n+1);
                    yield return coroutine;
                    
                    // Backtracks 
                    backtracks.gameObject.SetActive(true);
                    StartCoroutine("disableMessage");
                    yield return new WaitForSeconds(0.8f);

                } else {
                    if(queensPos[n].gameObject != null)
                        Destroy(queensPos[n].gameObject);
                }
            }
            Destroy(queensPos[n-1].gameObject);
        }
    }

    // Function of utility to print in console the chessboard with the 8-queens 
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


    // GUI methods

    // Coroutine of utility
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

    public void showInfo() {
        panel.SetActive(!panel.active);
    }
}
