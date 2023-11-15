using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Размеры сетки
    public int width = 10;
    public int height = 10;
    public Sprite squareSprite;
    // Размер квадрата
    public float squareSize = 1f;
    public GameObject Camera;
    public Canvas canvas;
    // Двумерный массив квадратов
    private Square[,] grid;

    private void Start()
    {
        canvas.enabled = false;
        // Создание сетки при запуске игры
        CreateGrid();
        Vector3 camera_position = new Vector3(width/2, (height/2)-0.5f, -10);
        Camera.transform.position = camera_position;
    }
    void Update()
    {

    }

    private void CreateGrid()
    {
        grid = new Square[width, height]; // Инициализация двумерного массива

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Создание квадрата и его положение
                Vector3 position = new Vector3(x * squareSize, y * squareSize, 0f);
                Square square = new Square(position);

                // Создание GameObject для квадрата
                GameObject squareGO = new GameObject("Square");
                squareGO.transform.position = position;

                // Добавление компонента SpriteRenderer к GameObject и присваивание спрайта
                SpriteRenderer spriteRenderer = squareGO.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = squareSprite;

                // Добавление компонента BoxCollider2D к GameObject для обработки столкновений
                BoxCollider2D boxCollider = squareGO.AddComponent<BoxCollider2D>();
                boxCollider.size = new Vector2(squareSize, squareSize);

                // Добавление компонента SquareClickHandler к GameObject для обработки нажатий
                SquareClickHandler clickHandler = squareGO.AddComponent<SquareClickHandler>();
                clickHandler.square = square;
                clickHandler.canvas = canvas;

                // Добавление квадрата в сетку
                square.squareGO = squareGO;
                grid[x, y] = square;

            }
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                // Присваивание соседей каждому квадрату
                if (x > 0)
                {
                    grid[x, y].leftNeighbor = grid[x - 1, y];
                }
                if (x < width - 1)
                {
                    grid[x, y].rightNeighbor = grid[x + 1, y];
                }
                if (y > 0)
                {
                    grid[x, y].bottomNeighbor = grid[x, y - 1];
                }
                if (y < height - 1)
                {
                    grid[x, y].topNeighbor = grid[x, y + 1];
                }
            }
        }
    }
    
}

public class Square
{
    public Vector3 position;
    public Square leftNeighbor;
    public Square rightNeighbor;
    public Square topNeighbor;
    public Square bottomNeighbor;
    public GameObject squareGO;
    public string discription = "Небольшое описание каждой клетки";

    public Square(Vector3 pos)
    {
        position = pos;
    }
    
}
public class SquareClickHandler : MonoBehaviour
{
    public Square square;
    public Canvas canvas;

    private void OnMouseDown()
    {
        canvas.enabled = true;
        UnityEngine.UI.Text legacyTextComponent = canvas.GetComponentInChildren<UnityEngine.UI.Text>();
        legacyTextComponent.text = square.discription;
        Debug.Log(square.position);
        Debug.Log(square.leftNeighbor.position);
        Debug.Log(square.rightNeighbor.position);
        Debug.Log(square.topNeighbor.position);
        Debug.Log(square.bottomNeighbor.position);
    }
}
