using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour, IPointerClickHandler {

    public static PlacementManager Instance { get; private set; }

    private Camera mainCamera;

    private BuildingButton placingObject;
    private Building currentGhost;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        this.mainCamera = Camera.main;
    }

    private void Update() {
        if (this.placingObject) {
            var worldPos = this.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var snappedWorldPos = new Vector3(Mathf.Ceil(worldPos.x - 0.5F), Mathf.Floor(worldPos.y + 0.5F), 0);

            if (!this.currentGhost) {
                this.currentGhost = Instantiate(this.placingObject.building, snappedWorldPos, Quaternion.identity);
                this.currentGhost.SetGhost();
            }
            this.currentGhost.Move(snappedWorldPos);

            var ghostActive = !EventSystem.current.IsPointerOverGameObject();
            this.currentGhost.SetGhostActive(ghostActive);

            if (ghostActive && Input.GetMouseButtonDown(0) && this.currentGhost.CanPlaceHere()) {
                Instantiate(this.placingObject.building, snappedWorldPos, Quaternion.identity);
                this.placingObject.Remove();
                this.SetPlacingObject(null);
            }
        }
    }

    public void SetPlacingObject(BuildingButton button) {
        this.placingObject = button;
        if (this.currentGhost) {
            Destroy(this.currentGhost.gameObject);
            this.currentGhost = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
    }

}