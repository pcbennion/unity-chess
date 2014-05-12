using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class ChessPiece : MonoBehaviour {

	// Internal variables
	protected ArrayList move;
	protected ArrayList attack;

	// Publics and properties
	public Square tile;
	public Square CurrentTile { get { return tile; } } 
	public ArrayList MoveList { get { return move; } }
	public ArrayList AttackList { get { return attack; } }

	// Use this for initialization
	void Start () {
		move = new ArrayList ();
		attack = new ArrayList ();
		tile.Occupant = this;
	}

	/**
	 *	Clear the lists between moves
	 */
	public void clearLists() {
		move.Clear ();
		attack.Clear ();
	}

	// Abstract functions
	public abstract void moveToDest(Square s);
	public abstract void populateLists();
	public abstract void removePiece();
}
