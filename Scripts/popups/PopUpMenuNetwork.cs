using Godot;
using System;

public partial class PopUpMenuNetwork : Control
{
	[Export]
	public Button CreateLinkButton;

	[Export]
	public Button DeleteButton;

	public GameNode Source {get; set;}

	public GameManager GameManager {get; set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameManager = GetNode<GameManager>("/root/GameScreen/GameManager");
		GameManager.PopUpActive = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_CreateLinkButton_pressed()
	{
		GameManager.StartDrawLine(Source);
		GetParent().RemoveChild(this);
		GameManager.LockUI.Start();
		QueueFree();
	}

	private void _on_DeleteButton_pressed()
	{
		Source.Delete();
		GetParent().RemoveChild(this);
		GameManager.LockUI.Start();
		QueueFree();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.Pressed)
			{
				//Check if mouse is over this control
				var mousePos = GetGlobalMousePosition();
				GD.Print(mousePos);
				var rect = GetRect();
				GD.Print(rect.Area);
				if (!rect.HasPoint(mousePos))
				{
					//Close popup
					GetParent().RemoveChild(this);
					QueueFree();
				}
			}
		}
	}
}
