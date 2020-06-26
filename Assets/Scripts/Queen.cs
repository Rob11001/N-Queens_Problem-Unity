using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    A simple class that represents a Queen on the chessboard 
*/
public class Queen {
    private int _pos;
    private GameObject _gameObject;
    public Queen()
    {
        _pos = 0;
        _gameObject = null;
    }

    public int Pos {
        get => _pos;

        set {
            _pos = value;
        }
    }

    public GameObject gameObject {
        get => _gameObject;

        set {
            _gameObject = value;
        }
    }
}