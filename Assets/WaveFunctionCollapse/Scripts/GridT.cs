using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WaveFunctionCollapse.Scripts
{
    public class Grid<TValue, TCell> : IEnumerable<TCell> where TCell : Cell<TValue, TCell>
    {
        public List<TCell> data = new List<TCell>();
        public int width;
        public int height;

        public int Count => width * height;
        public TCell this[int i] => data[i];
        public TCell this[int x, int y] => data[x + y * width];

        public Grid() { }

        public Grid(int width, int height)
        {
            Reset(width, height);
        }

        public void Reset(int newWidth, int newHeight)
        {
            data.Clear();
            width = newWidth;
            height = newHeight;
            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                var newCell = Activator.CreateInstance<TCell>();
                newCell.name = $"Cell ({x},{y})";
                data.Add(newCell);
            }

            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                this[x, y].grid = this;
                this[x, y].up = this[x, WrapIndex(y - 1, newHeight)];
                this[x, y].down = this[x, WrapIndex(y + 1, newHeight)];
                this[x, y].left = this[WrapIndex(x - 1, newWidth), y];
                this[x, y].right = this[WrapIndex(x + 1, newWidth), y];
            }
        }

        public void SetValue(TValue value, int i)
        {
            this[i].value = value;
        }

        public void ClearValue(int i)
        {
            this[i].value = default;
        }

        public void SetValue(TValue value, int x, int y)
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

        IEnumerator<TCell> IEnumerable<TCell>.GetEnumerator() => data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => data.GetEnumerator();
    }

    [Serializable]
    public class Cell<TValue, TCell> where TCell : Cell<TValue, TCell>
    {
        [HideInInspector] public string name;
        public TValue value;
        [NonSerialized] public Grid<TValue, TCell> grid;
        public TCell left;
        public TCell right;
        public TCell down;
        public TCell up;
    }
}