using System;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse.Scripts
{
    [Serializable]
    public class Cell<TValue, TCell> where TCell : Cell<TValue, TCell>
    {
        [HideInInspector] public string name;
        [HideInInspector] public int x;
        [HideInInspector] public int y;
        public TValue value;
        [NonSerialized] public Grid<TValue, TCell> grid;
        public Dictionary<Neighbor, TCell> neighbors = new Dictionary<Neighbor, TCell>();

        public TCell this[Neighbor neighbor]
        {
            get => neighbors[neighbor];
            set => neighbors[neighbor] = value;
        }

        public Cell()
        {
            neighbors.Add(Neighbor.Up, null);
            neighbors.Add(Neighbor.Down, null);
            neighbors.Add(Neighbor.Left, null);
            neighbors.Add(Neighbor.Right, null);
        }
    }
}