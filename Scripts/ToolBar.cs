using Godot;
using System;

public partial class ToolBar : Panel
{
	[ExportGroup("Screen")]
	[Export]
	public Node2D Screen {get; set;}

	private IDraggable _draggedNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(Screen == null){
			Screen = GetNode<Node2D>("/root/GameScreen");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnIconButtonDown(string NodePath)
	{
		GD.Print("Icon Button Down");

		var newNetwork = GD.Load<PackedScene>("res://Prefabs/network_node.tscn").Instantiate();
		newNetwork.Set("NetworkName", "");
		newNetwork.Set("AddressSpace", "");
		newNetwork.Set("IsShadow", true);
		Screen.AddChild(newNetwork);
		_draggedNode = newNetwork as IDraggable;

	}

	public void OnIconButtonUp(string NodePath)
	{
		GD.Print("Icon Button Up");
		_draggedNode.IsShadow = false;
	}
}
