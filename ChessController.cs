using UnityEngine;
using System.Collections;


public class ChessController : MonoBehaviour {

	// Variables for keeping track of input
	private Vector3 mousePos;
	private Square clicked;

	// Selection and other UI elements
	private Square mouseover;

	// Internal game vars
	private int playerTurn;
	private ChessPiece selection;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Get mouse pos for mouseover overlays
		Vector3 newPos = Input.mousePosition;
		// If mouse position has changed, figure out the new mouseover tile
		Square newMO = mouseover;
		if (newPos != mousePos) {
			mousePos = newPos;
			Ray ray = Camera.main.ScreenPointToRay (newPos);
			RaycastHit hit;
			// Cast a ray, figure out what it hits
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				GameObject obj = hit.collider.transform.gameObject;
				newMO = (Square)obj.GetComponent(typeof(Square));
				if (newMO == null) {
						newMO = ((ChessPiece)obj.GetComponent(typeof(ChessPiece))).CurrentTile;
				}
			// If the mouse hits nothing:
			} else {
				// No mouseover
				newMO = null;
			}
		}
		// Update mouseover overlay if new tile moused over
		if (newMO != mouseover || Input.GetMouseButtonDown (0)) {
			// Remove mouseover effect from last tile
			if(mouseover != null){
				mouseover.ClearOverlay();
			}
			// Add mouseover effect to new tile
			if(newMO != null){
				// If holding mouse button, change mouseover color to reflect that
				if(Input.GetMouseButton(0)) {
					clicked = newMO;
					newMO.SetOverlay();
				// Otherwise, color normally
				} else {
					newMO.SetOverlay();
				}
			}
			mouseover = newMO;
		}

		// Create click events when mouse is clicked and released on the same object
		if (Input.GetMouseButtonUp (0) && mouseover == clicked) {
				ClickEvent (mouseover);
		}
	}

	private void endTurn() {

	}

	private void ClickEvent(Square t) {
		
		// If t is not a valid tile, deselect
		if (t == null) {
			// Clear overlays there was a selection
			if(selection != null) {
				foreach(Square s in selection.MoveList) s.ClearOverlay();
				foreach(Square s in selection.AttackList) s.ClearOverlay();
			}
			// Deselect object
			selection = null;
			return;
		}
		ChessPiece p = t.Occupant;

		// If no selection and no p, return
		if (p == null && selection == null) return;
		
		// If no selection, set selection to p
		if (selection == null && p != null) {
			// Set selection, ask it to populate lists
			selection = p;
			p.populateLists();
			// Update overlays with move and attack info
			foreach(Square s in selection.MoveList) if(s!=null) s.SetOverlay();
			foreach(Square s in selection.AttackList) if(s!=null) s.SetOverlay();
			return;
		}
		
		// If re-clicking object, deselect
		if (selection == p && p != null) {
			// Clear overlays
			foreach(Square s in selection.MoveList) if(s!=null) s.ClearOverlay();
			foreach(Square s in selection.AttackList) if(s!=null) s.ClearOverlay();
			selection.clearLists();
			// Deselect object
			selection = null;
			return;
		}
		
		// If selected tile is a valid move, tell piece to move there
		if (p == null && selection.MoveList.Contains (t)) {
			// Clear overlays
			foreach(Square s in selection.MoveList) if(s!=null) s.ClearOverlay();
			foreach(Square s in selection.AttackList) if(s!=null) s.ClearOverlay();
			//  Tell piece to move to tile
			selection.moveToDest(t);
			//  Clear selection and advance turn
			selection.clearLists();
			selection = null;
			endTurn();
			return;
		}
		
		// If selected tile is a valid attack, set up attack
		if (p != null && selection.AttackList.Contains (t)) {
			// Clear overlays
			foreach(Square s in selection.MoveList) if(s!=null) s.ClearOverlay();
			foreach(Square s in selection.AttackList) if(s!=null) s.ClearOverlay();
			// Remove defending piece, tell piece to move to tile
			p.removePiece();
			selection.moveToDest(t);
			//  Clear selection and advance turn
			selection.clearLists();
			selection = null;
			endTurn();
			return;
		}
	}
}
