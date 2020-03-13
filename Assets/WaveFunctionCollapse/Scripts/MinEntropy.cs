using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinEntropy : MonoBehaviourWithPopulateButton
{
    public OutputGrid outputGrid;
    public Entropies entropies;
    public Entropies.Item minCell;
    public float entropy;
    public int i;
    public int x;
    public int y;

    private void OnValidate()
    {
        if (!outputGrid) outputGrid = FindObjectOfType<OutputGrid>();
        if (!entropies) entropies = FindObjectOfType<Entropies>();
    }

    public override void Populate()
    {
        entropy = entropies.all.Where(e => e.gridCell.possible.Count > 1).Min(e => e.entropy);
        minCell = entropies.all.Where(e => e.entropy == entropy).First();
        i = entropies.all.IndexOf(minCell);
        x = i % outputGrid.width;
        y = i / outputGrid.height;
    }
}
