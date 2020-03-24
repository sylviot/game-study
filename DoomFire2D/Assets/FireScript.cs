using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireScript : MonoBehaviour
{
    private Color[] colors = new Color[]
    {
        new Color(7, 7, 7, 1),    new Color(31,7,7, 1),    new Color(47,15,7, 1),    new Color(71,15,7, 1),    new Color(87,23,7, 1),    new Color(103,31,7, 1),
        new Color(119,31,7, 1),    new Color(143,39,7, 1),    new Color(159,47,7, 1),    new Color(175,63,7, 1),    new Color(191,71,7, 1),    new Color(199,71,7, 1),
        new Color(223,79,7, 1),    new Color(223,87,7, 1),    new Color(223,87,7, 1),    new Color(215,95,7, 1),    new Color(215,95,7, 1),    new Color(215,103,15, 1),
        new Color(207,111,15, 1),    new Color(207,119,15, 1),    new Color(207,127,15, 1),    new Color(207,135,23, 1),    new Color(199,135,23, 1),    new Color(199,143,23, 1),
        new Color(199,151,31, 1),    new Color(191,159,31, 1),    new Color(191,159,31, 1),    new Color(191,167,39, 1),    new Color(191,167,39, 1),    new Color(191,175,47, 1),
        new Color(183,175,47, 1),    new Color(183,183,47, 1),    new Color(183,183,55, 1),    new Color(207,207,111, 1),    new Color(223,223,159, 1),    new Color(239,239,199, 1),
        new Color(255,255,255, 1)
    };

    public const int WIDTH = 160;
    public const int HEIGHT = 50;

    public TileBase tilebase;
    public Sprite square;


    protected Grid grid;
    protected Tilemap tilemap;
    protected int[] canvas;

    void Start()
    {
        this.canvas = new int[HEIGHT * WIDTH];
        this.Source();

        this.grid = GetComponent<Grid>();
        this.tilemap = GetComponentInChildren<Tilemap>();
    }

    public float time = .15f;
    float timeelapsed = 0;
    void Update()
    {
        this.timeelapsed += Time.deltaTime;

        if(timeelapsed > time)
        {
            this.CalculateFire();
            this.timeelapsed = 0;
        

        for (int row = 0; row < HEIGHT; row++)
        {
            for (int col = 0; col < WIDTH; col++)
            {
                var index = (row * WIDTH) + col;
                var position = new Vector3Int(col, row, 0);
                var color = this.colors[this.canvas[index]];

                this.tilemap.SetTile(position, this.tilebase);
                this.tilemap.SetTileFlags(position, TileFlags.None);
                this.tilemap.SetColor(position, new Color(r: color.r / 255, g: color.g / 255, b: color.b / 255, 1));
                
                // new Color(.7f, .7f, .7f, 1));
            }

        }
        }
    }
    private void CalculateFire()
    {
        for (int col = 0; col < WIDTH; col++)
        {
            for (int row = 0; row < HEIGHT; row++)
            {
                int index = (row * WIDTH) + col;
                this.UpdateFire(index);
            }
        }
    }

    private void UpdateFire(int index)
    {
        int bellowIndex = index - WIDTH;

        if (bellowIndex < 0 || bellowIndex >= HEIGHT * WIDTH) return;

        int decay = (int)Random.Range(0f, 3f);
        int source = this.canvas[bellowIndex];
        int power = (source - decay);
        //this.canvas[index] = power < 0?0:power;
        //if(index - decay >=0 && index - decay < HEIGHT * WIDTH)
        this.canvas[index - decay] = power < 0 ? 0 : power;
    }

    private void Source()
    {
        for (int col = 0; col < WIDTH; col++)
        {
            this.canvas[col] = Random.Range(1, 36);
        }
    }
}
