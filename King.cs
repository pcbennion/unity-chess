using UnityEngine;
using System.Collections;

public class King : ChessPiece {

	// Update is called once per frame
	void Update () {
	
	}

	/**
	 *	Set piece to move to specified square
	 *	TODO: make it an arc instead of teleport
	 */
	override public void moveToDest(Square s) {
		tile.Occupant = null;
		s.Occupant = this;
		this.tile = s;
		((Transform)this.GetComponent (typeof(Transform))).localPosition = s.Position;
	}

	/**
	 *	Start a recursive search for all possible moves
	 */
	override public void populateLists() {
		for (int i = (int)Square.dir.up; i<=(int)Square.dir.ul; i++) {
			Square s = tile.adjacent[i];
			if(s != null) {
				ChessPiece p = s.Occupant;
				if(p!=null) attack.Add(s);
				else move.Add(s); 
			}
		}
	}

	/**
	 *	Gracefully remove piece from board
	 *	TODO: fade out, possible scorekeeping
	 */
	override public void removePiece() {
		tile.Occupant = null;
		Destroy (this);
	}
}
