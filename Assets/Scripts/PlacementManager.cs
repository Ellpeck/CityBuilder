using UnityEngine;

public class PlacementManager : MonoBehaviour {

    public Building placingObject;

    private Camera mainCamera;

    private Building currentGhost;

    private void Start() {
        this.mainCamera = Camera.main;
    }

    private void Update() {
        if (this.placingObject) {
            var mousePos = Input.mousePosition;
            if (mousePos.x < 0 || mousePos.y < 0 || mousePos.x >= this.mainCamera.pixelWidth || mousePos.y >= this.mainCamera.pixelHeight)
                return;

            var worldPos = this.mainCamera.ScreenToWorldPoint(mousePos);
            var snappedWorldPos = new Vector3(Mathf.Ceil(worldPos.x - 0.5F), Mathf.Floor(worldPos.y + 0.5F), 0);

            if (!this.currentGhost) {
                this.currentGhost = Instantiate(this.placingObject, snappedWorldPos, Quaternion.identity);
                this.currentGhost.SetGhost();
            }
            this.currentGhost.Move(snappedWorldPos);

            if (Input.GetMouseButtonDown(0) && this.currentGhost.CanPlaceHere()) {
                Instantiate(this.placingObject, snappedWorldPos, Quaternion.identity);
                Destroy(this.currentGhost.gameObject);
                this.currentGhost = null;
            }
        }
    }

}