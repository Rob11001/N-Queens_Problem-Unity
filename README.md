# N-Queens_Problem-Unity
It's a very simple application made in **Unity**. I made it because I was studying Unity 3D for an exam and in the same time I was studying the **Backtracking** technique for the exact esponential algoritms.

# Installation
Simply download the project and then open it in Unity. The version used is  2019.3.3f1.

# Content
The application implements a simple backtracking solution for the N-Queens problem, in particular the version with a chessboard 8x8. Also it allows to execute the algorithm step by step.
Pseudo-code:
```
PlaceQueens(Q[1..n],r) {
	if(r == n+1)
		print Q[1…n] 
	else {
		for(j = 1; j < n + 1; j++){
			legal = true

			for(i = 1; i < r; i++) {
				if((Q[i] == j) ∨ (Q[i] == j+r-i) ∨ (Q[i] == j-r+i))
					legal = false
			}
			
			if (legal) {
				Q[r] = j
				PlaceQueens(Q[1…n],r+1)
			}
		}
	}
```
Note:
```
Q[i] : indicates the position of the i-th Queen
```