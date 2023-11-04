using Godot;
using System;
using System.Collections.Generic;

public partial class NetworkNode : Node2D, IDraggable
{
	[ExportGroup("Labels")]
	[Export]
	public string NetworkName { get; set; }
	[ExportGroup("Labels")]
	[Export]
	public string AddressSpace { get; set; }

	private Node2D Screen;

	public List<string> ConnectedDevices { get; set; }
	public List<string> Subnets { get; set; }
	[Export]
	public RichTextLabel networkNameLabel;
	[Export]
	public RichTextLabel addressSpaceLabel;

	[Export]
	public bool IsShadow { get; set; }

	public bool isFocus { get; set; } = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Screen == null)
		{
			Screen = GetNode<Node2D>("/root/GameScreen");
		}
		networkNameLabel.Text = "[center]" + NetworkName + "[/center]";
		addressSpaceLabel.Text = "[center]" + AddressSpace + "[/center]";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(IsShadow){
			this.Position = GetGlobalMousePosition();
		}
	}

	public void Place(){
		IsShadow = false;
	}

	public void HasFocus()
	{
		GD.Print("Has Focus");
		isFocus = true;

	}
	public void LostFocus()
	{
		GD.Print("Lost Focus");
		isFocus = false;
	}

	public void OnClick(Node viewport, InputEvent @event, int shape_idx){
		if(@event is null)
		{
			return;
		}
		if(isFocus && !IsShadow)
		{
			var mouseEvent = @event as InputEventMouseButton;
			if(mouseEvent is null)
			{
				return;
			}
			if(mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed){
				GD.Print("Clicked");
				//Create new popup
				var popup = GD.Load<PackedScene>("res://Prefabs/pop_up_create_node.tscn").Instantiate();
				popup.Set("Values", new string[]{"Network Name", "Address Space"});
				Screen.AddChild(popup);
			}

		}
	}
}
