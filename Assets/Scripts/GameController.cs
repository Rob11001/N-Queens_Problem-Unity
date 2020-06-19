using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    [SerializeField]
    private Vector3 startPosition;
    private int N = 8;
    private Vector3[,] chessBoardPositions;
    [SerializeField]
    private float Xoffset, Zoffset;
    private GameObject queen;

    
    // Start is called before the first frame update
    void Start()
    {
        chessBoardPositions = new Vector3 [N,N];
        for(int i = 0; i < N; i++)
            for(int j = 0; j < N; j++) 
                chessBoardPositions[i,j] = startPosition + new Vector3(-Xoffset*j, 0f, Zoffset*i);
        queen = Resources.Load<GameObject>("queen");

        for(int i = 0; i < N; i++)
            for(int j = 0; j < N; j++) 
                Instantiate(queen,chessBoardPositions[i,j], Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
