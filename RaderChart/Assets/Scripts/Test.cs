using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private StatRaderChart _statRaderChart;
    
    private void Start()
    {
        //access the Stat constructor
        //*user input*
        Stat stats = new Stat(9, 3, 3, 7, 4);

        _statRaderChart.SetStats(stats);
    }
}
