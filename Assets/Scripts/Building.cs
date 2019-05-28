using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    private static readonly int Ghost = Animator.StringToHash("Ghost");
    private static readonly int PlaceHere = Animator.StringToHash("CanPlaceHere");

    public LayerMask disallowedLayers;

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
        return !this.collider.IsTouchingLayers(this.disallowedLayers);
    }

}