using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance { get; private set; }

    public LevelInfo currentLevel;
    public GameObject itemMenu;
    public BuildingButton slot;

    public int currentGroup;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        this.PopulateLevel();
    }

    private void PopulateLevel() {
        foreach (var building in this.currentLevel.Groups[this.currentGroup]) {
            var newSlot = Instantiate(this.slot, this.itemMenu.transform);
            newSlot.building = building;
        }
    }

    public void OnButtonRemoved() {
        if (this.itemMenu.transform.childCount <= 1) {
            this.currentGroup++;
            if (this.currentLevel.Groups.Count > this.currentGroup)
                this.PopulateLevel();
        }
    }

}