using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Script for the tiles that form the basis of a chessboard
// Maintains a set of pointers for adjacency
// Handles overlay rendering

[RequireComponent(typeof(Collider))]
public class Square : MonoBehaviour{

	// Enums for keeping track of adjacency directions.
	// Order is: cardinals clockwise, diagonals clockwise
	public enum dir : int {up=0, rt, dn, lf, ur, dr, dl, ul};

	// Internal variables
	private Transform transform;
	private MeshRenderer overlay;
	private int overlayCounter;

	// Publics and properties
	public Vector3 Position { get{ return (transform.localPosition); } }
	public Square[] adjacent;
	public ChessPiece Occupant { get; set; }

	/**
	 * 
	 */
	public void SetOverlay() {
		if (overlayCounter < 0) overlayCounter = 0;
		overlay.enabled = true;
		overlayCounter++;
	}

	/**
	 * 
	 */
	public void ClearOverlay() {
		if(--overlayCounter < 1) overlay.enabled = false;
	}

	// Use this for initialization
	void Start () {
		// Grab object
		transform = (Transform)this.GetComponent (typeof(Transform));
		// Init overlay, assert the child exists and has a mesh renderer
		Transform oPtr = transform.Find ("Overlay");
		if(oPtr == null)
			Debug.LogError("Squares must have a child named 'Overlay'");
		overlay = (MeshRenderer)oPtr.GetComponent (typeof(MeshRenderer));
		if(overlay == null)
			Debug.LogError("Squares must have a child with a mesh renderer");
		overlay.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
