using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
	[Export]
	public Node2D Screen {get; set;}

	public List<Placeable> Nodes { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Screen == null)
		{
			Screen = GetNode<Node2D>("/root/GameScreen");
		}
		Nodes = new List<Placeable>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlaceNode(Placeable node){
		//Add node to screen and start create popup
	}

	public void RegisterPlaceableNode(Placeable node){
		node.NewNodePlaced += PlaceNode;
		Nodes.Add(node);
	}
}
