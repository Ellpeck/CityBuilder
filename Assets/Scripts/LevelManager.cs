using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance { get; private set; }

    public GameObject itemMenu;
    public BuildingButton slot;

    public int currentGroup;
    private ItemGroup[] itemGroups;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        var items = GameObject.FindGameObjectWithTag("Items").transform;
        this.itemGroups = new ItemGroup[items.childCount];
        for (var i = 0; i < this.itemGroups.Length; i++)
            this.itemGroups[i] = items.GetChild(i).GetComponent<ItemGroup>();

        this.PopulateLevel();
    }

    private void PopulateLevel() {
        foreach (var building in this.itemGroups[this.currentGroup].buildings) {
            var newSlot = Instantiate(this.slot, this.itemMenu.transform);
            newSlot.building = building;
        }
    }

    public void OnButtonRemoved() {
        if (this.itemMenu.transform.childCount <= 1) {
            this.currentGroup++;
            if (this.itemGroups.Length > this.currentGroup)
                this.PopulateLevel();
        }
    }

}