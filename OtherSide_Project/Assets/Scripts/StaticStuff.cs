using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticStuff : MonoBehaviour
{
    public static StaticStuff i { get; set; }
    public LayerMask groundInside, groundOutside;
    public GameObject dieExplosionPrefab;
    public Transform spawnPointPrefab;
    public Transform whitePixel;
    public GameObject spawnPointSetEffect;



    [SerializeField] LayerMask playerInsideMask, playerOutsideMask;

    [HideInInspector] public int playerOutside, playerInside;
    private void Awake()
    {
        i = this;
        playerOutside = Helper.ToLayer(playerOutsideMask);
        playerInside = Helper.ToLayer(playerInsideMask);
    }
}
