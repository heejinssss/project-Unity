using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public sealed class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }

    public Row[] rows;
    public Tile[,] Tiles { get; private set; }

    public int Width => Tiles.GetLength(0);
    public int Height => Tiles.GetLength(1);

    private Tile _selectedTile1;
    private Tile _selectedTile2;

    private readonly List<Tile> _selection = new List<Tile>();

    private const float TweenDuration = 0.52f;


    private void Awake() => Instance = this;

    private void Start()
    {
        Tiles = new Tile[rows.Max(row => row.tiles.Length), rows.Length];

        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var tile = rows[y].tiles[x];

                tile.x = x;
                tile.y = y;

                Tiles[x, y] = rows[y].tiles[x];
                tile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)];
            }
        }

        /* 타일 섞기 버튼[S] */
        var shuffleButton = GameObject.Find("ShuffleButton").GetComponent<Button>();
        shuffleButton.onClick.AddListener(ShuffleTiles);
    }

    public void ShuffleTiles()
    {
        List<Tile> allTiles = new List<Tile>();
        List<Item> allItems = new List<Item>();

        // 모든 타일과 아이템을 리스트에 추가
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                allTiles.Add(Tiles[x, y]);
                allItems.Add(Tiles[x, y].Item);
            }
        }

        // 아이템 리스트를 랜덤하게 섞음
        for (int i = 0; i < allItems.Count; i++)
        {
            Item temp = allItems[i];
            int randomIndex = Random.Range(i, allItems.Count);
            allItems[i] = allItems[randomIndex];
            allItems[randomIndex] = temp;
        }

        // 각 타일에 랜덤하게 섞인 아이템을 할당
        for (int i = 0; i < allTiles.Count; i++)
        {
            allTiles[i].Item = allItems[i];
        }
    }
    /* 타일 섞기 버튼[E] */

    public async void Select(Tile tile)
    {
        if (!_selection.Contains(tile)) _selection.Add(tile);

        // 두 개의 타일이 선택되었는가?
        if (_selection.Count < 2) return;

        // 첫 번째 타일과 두 번째 타일이 인접해 있는지 확인
        if (!IsAdjacent(_selection[0], _selection[1]))
        {
            Debug.Log("Tiles are not adjacent.");
            _selection.Clear();
            return;
        }

        Debug.Log($"Selected tiles at ({_selection[0].x}, {_selection[0].y}) ({_selection[1].x}, {_selection[1].y})");

        await Swap(_selection[0], _selection[1]);

        if (CanPop())
        {
            Pop();
        }
        else
        {
            await Swap(_selection[0], _selection[1]);
        }

        _selection.Clear();
    }

    // 인접한 타일인지 확인하는 메서드
    private bool IsAdjacent(Tile tile1, Tile tile2)
    {
        return (tile1.x == tile2.x && Mathf.Abs(tile1.y - tile2.y) == 1) ||
               (tile1.y == tile2.y && Mathf.Abs(tile1.x - tile2.x) == 1);
    }

    public async Task Swap(Tile tile1, Tile tile2)
    {
        var icon1 = tile1.icon;
        var icon2 = tile2.icon;

        var icon1Transform = icon1.transform;
        var icon2Transform = icon2.transform;

        var sequence = DOTween.Sequence();

        sequence.Join(icon1Transform.DOMove(icon2Transform.position, TweenDuration))
                .Join(icon2Transform.DOMove(icon1Transform.position, TweenDuration));

        await sequence.Play().AsyncWaitForCompletion();

        icon1Transform.SetParent(tile2.transform);
        icon2Transform.SetParent(tile1.transform);

        tile1.icon = icon2;
        tile2.icon = icon1;

        var tile1Item = tile1.Item;

        tile1.Item = tile2.Item;
        tile2.Item = tile1Item;
    }

    private bool CanPop()
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                if (Tiles[x, y].GetConnectedTiles().Skip(1).Count() >= 2)
                    return true;
        return false;
    }

    private async void Pop()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var tile = Tiles[x, y];

                var connectedTiles = tile.GetConnectedTiles();

                if (connectedTiles.Skip(1).Count() < 2) continue;

                var deflateSequence = DOTween.Sequence();

                foreach (var connectedTile in connectedTiles) deflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.zero, TweenDuration));

                await deflateSequence.Play().AsyncWaitForCompletion();

                ScoreCounter.Instance.Score += tile.Item.value * connectedTiles.Count;

                var inflateSequence = DOTween.Sequence();

                foreach (var connectedTile in connectedTiles)
                {
                    connectedTile.Item = ItemDatabase.Items[Random.Range(0, ItemDatabase.Items.Length)];

                    inflateSequence.Join(connectedTile.icon.transform.DOScale(Vector3.one, TweenDuration));
                }

                await inflateSequence.Play().AsyncWaitForCompletion();

                x = 0;
                y = 0;
            }
        }
    }
}
