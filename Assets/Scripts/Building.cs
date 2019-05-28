using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    private static readonly int Ghost = Animator.StringToHash("Ghost");
    private static readonly int PlaceHere = Animator.StringToHash("CanPlaceHere");

    public LayerMask disallowedLayers;
    public LayerMask requiredBuildings;
    public int requiredBuildingAmount;
    public Collider2D requiredBuildingRange;

    private Animator anim;
    private Rigidbody2D body;
    private new BoxCollider2D collider;

    private void Awake() {
        this.anim = this.GetComponent<Animator>();
        this.body = this.GetComponent<Rigidbody2D>();
        this.collider = this.GetComponent<BoxCollider2D>();
    }

    public void SetGhost() {
        this.body.bodyType = RigidbodyType2D.Dynamic;
        this.anim.SetBool(Ghost, true);
    }

    public void Move(Vector3 position) {
        this.transform.position = position;
        this.anim.SetBool(PlaceHere, this.CanPlaceHere());
    }

    public bool CanPlaceHere() {
        if (this.collider.IsTouchingLayers(this.disallowedLayers))
            return false;
        if (this.requiredBuildings == 0)
            return true;

        var filter = new ContactFilter2D();
        filter.useTriggers = true;
        filter.SetLayerMask(this.requiredBuildings);

        var collisions = new List<Collider2D>();
        Physics2D.OverlapCollider(this.requiredBuildingRange, filter, collisions);
        return collisions.Count >= this.requiredBuildingAmount;
    }

}