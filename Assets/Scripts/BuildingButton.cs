using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour {

    public Building building;

    private void Start() {
        var image = this.GetComponent<Image>();
        image.sprite = this.building.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    public void OnPressed() {
        PlacementManager.Instance.SetPlacingObject(this);
    }

    public void Remove() {
        Destroy(this.gameObject);
        LevelManager.Instance.OnButtonRemoved();
    }

}