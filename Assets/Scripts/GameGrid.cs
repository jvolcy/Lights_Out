using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameGrid : MonoBehaviour
{
    public TMP_Text NumLit;
    public TMP_Text NumClicks;
    public TMP_Text Win;

    public GameCell GameCellPrefab;
    public int GridWidth = 5;
    public int GridHeight = 5;
    public float GridSpacing = 0.05f;

    int num_clicks = 0;
    GameCell[,] Grid;
    bool won = false;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
        InitializeGrid(20);
    }

    // Update is called once per frame
    void Update()
    {
        if (won) return;

        //check if any cell has been clicked
        for (int i = 0; i < GridWidth; i++)
            for (int j = 0; j < GridHeight; j++)
            {
                if (Grid[i,j].NeedsToggle)
                {
                    ToggleNeighborhood(i, j);

                    num_clicks++;
                    NumClicks.text = "  # Clicks: " + num_clicks.ToString();

                }
            }
    }

        void CreateGrid()
    {

        Grid = new GameCell[GridWidth, GridHeight];

        float width = 400f * 0.4f / 100;  //400 pixel image scaled by 0.4 with 100 pixels per unit
        float height = 400f * 0.4f / 100;  //400 pixel image scaled by 0.4 with 100 pixels per unit

        float startX = -(width + GridSpacing) * (int)(GridWidth / 2);
        float startY = -(height + GridSpacing) * (int)(GridHeight / 2);

        for (int i = 0; i < GridWidth; i++)
            for (int j = 0; j < GridHeight; j++)
            {
                float x = startX + i * (width + GridSpacing);
                float y = startY + j * (height + GridSpacing);
                Grid[i, j] = Instantiate(GameCellPrefab, new Vector2 (x, y), Quaternion.identity);

                //let the cell know its position in the grid
                Grid[i, j].XPosition = i;
                Grid[i, j].YPosition = j;

                //Turn off the newly created cell (note that this call executes
                //before the Cell's Start() routine has the chance to run.
                //Do necessary initializations in Awake() )
                Grid[i, j].State = false;
                
            }
    }

    //function to initialize the grid
    void InitializeGrid(int RandomClicks)
    {
        //randomly click RandomClicks cells
        for (int i=0; i< RandomClicks; i++)
        {
            int x = Random.Range(0, GridWidth);
            int y = Random.Range(0, GridHeight);
            ToggleNeighborhood(x, y);
        }
    }


    int GetNumLitCells()
    {
        int count = 0;

        for (int i = 0; i < GridWidth; i++)
            for (int j = 0; j < GridHeight; j++)
            {
                //count the # of lit cells
                if (Grid[i, j].State) count++;
            }

        return count;
    }


    public void ToggleNeighborhood(int i, int j)
    {
        //function that toggles a cell and its 4 adjacent cells
        Debug.Log(i + "," + j);

        //toggle center
        Grid[i, j].State = !Grid[i, j].State;

        //toggle left
        if (i - 1 >= 0) Grid[i - 1, j].State = !Grid[i - 1, j].State;

        //toggle right
        if (i + 1 < GridWidth) Grid[i + 1, j].State = !Grid[i + 1, j].State;

        //toggle bottom
        if (j - 1 >= 0) Grid[i, j - 1].State = !Grid[i, j - 1].State;

        //toggle top
        if (j + 1 < GridHeight) Grid[i, j + 1].State = !Grid[i, j + 1].State;


        Grid[i, j].NeedsToggle = false;

        //update canvas text
        int numLit = GetNumLitCells();
        NumLit.text = "  # Lit: " + numLit.ToString();

        if (numLit == 0)
        {
            won = true;
            Win.enabled = true;
        }
    }
}
