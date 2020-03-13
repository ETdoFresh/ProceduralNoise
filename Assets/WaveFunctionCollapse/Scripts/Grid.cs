using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid<T>
{
    private List<Cell<T>> data = new List<Cell<T>>();
    public int width;
    public int height;

    public int Count => width * height;
    public Cell<T> this[int i] => data[i];
    public Cell<T> this[int x, int y] => data[x + y * width];

    public Grid() { }

    public Grid(int width, int height)
    {
        Reset(width, height);
    }

    public void Reset(int width, int height)
    {
        data.Clear();
        this.width = width;
        this.height = height;
        for (var i = 0; i < Count; i++)
            data.Add(new Cell<T>());

        for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                this[x, y].grid = this;
                this[x, y].up = this[x, WrapIndex(y - 1, height)];
                this[x, y].down = this[x, WrapIndex(y + 1, height)];
                this[x, y].left = this[WrapIndex(x - 1, width), y];
                this[x, y].right = this[WrapIndex(x + 1, width), y];
            }
    }

    public void SetValue(T value, int i)
    {
        this[i].value = value;
    }

    public void ClearValue(int i)
    {
        this[i].value = default;
    }

    public void SetValue(T value, int x, int y)
    {
        this[x, y].value = value;
    }

    public void ClearValue(int x, int y)
    {
        this[x, y].value = default;
    }

    private int WrapIndex(int value, int count)
    {
        if (count <= 0) return 0;

        while (value < 0) value += count;
        while (value >= count) value -= count;
        return value;
    }
}

public class Cell<T>
{
    public T value;
    public Grid<T> grid;
    public Cell<T> left;
    public Cell<T> right;
    public Cell<T> down;
    public Cell<T> up;
}