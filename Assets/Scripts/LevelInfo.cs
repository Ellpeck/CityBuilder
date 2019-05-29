using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class LevelInfo : ScriptableObject {

    public Building[] group1;
    public Building[] group2;
    public Building[] group3;
    public Building[] group4;
    public Building[] group5;

    public List<Building[]> Groups { get; private set; }

    private void OnEnable() {
        this.Groups = new List<Building[]>();
        foreach (var group in new[] {this.group1, this.group2, this.group3, this.group4, this.group5}) {
            if (group.Length > 0)
                this.Groups.Add(group);
        }
    }

}