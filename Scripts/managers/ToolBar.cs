using Godot;
using System;

public partial class ToolBar : Panel
{
	[ExportGroup("Screen")]
	[Export]
	public Node2D Screen {get; set;}

	private IDraggable _draggedNode;

	private GameManager _gameManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(Screen == null){
			Screen = GetNode<Node2D>("/root/GameScreen");
		}
		_gameManager = GetNode<GameManager>("/root/GameScreen/GameManager");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnIconButtonDown(string NodePath)
	{
		GD.Print("Icon Button Down");

		var newNetwork = GD.Load<PackedScene>("res://Prefabs/network_node.tscn").Instantiate() as NetworkNode;
		newNetwork.Set("NetworkName", "");
		newNetwork.Set("AddressSpace", "");
		newNetwork.Set("IsShadow", true);
		newNetwork.Name = $"NetworkNode-{_gameManager.Nodes.Count}";
		newNetwork.NetworkName = $"NetworkNode-{_gameManager.Nodes.Count}";
		newNetwork.AddressSpace = "10.0.0.0/24";
		Screen.AddChild(newNetwork);
		_draggedNode = newNetwork as IDraggable;

	}

	public void OnIconButtonUp(string NodePath)
	{
		GD.Print("Icon Button Up");
		_draggedNode.Place();
	}
}
