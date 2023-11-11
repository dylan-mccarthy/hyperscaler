using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
	[Export]
	public Node2D Screen {get; set;}

	public List<GameNode> Nodes { get; set; }

	public bool PopUpActive { get; set; }

	public bool DrawLine { get; set; } = false;

	public Timer LockUI { get; set; }

	private Line2D _line;

	private Vector2 _lineStart;
	private Vector2 _lineEnd;

	public GameNode SelectedNode { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Screen == null)
		{
			Screen = GetNode<Node2D>("/root/GameScreen");
		}
		Nodes = new List<GameNode>();
		LockUI = new Timer();
		LockUI.OneShot = true;
		LockUI.WaitTime = 0.5f;
		LockUI.Timeout += () => { PopUpActive = false; };
		this.AddChild(LockUI);
	}



    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		if(DrawLine)
		{
			var mousePos = GetViewport().GetMousePosition();
			_line.ClearPoints();
			//Calculate position around the Selected Node
			var position = SelectedNode.Position + (mousePos - SelectedNode.Position).Normalized() * 80;
			_lineStart = position;
			_line.AddPoint(position);
			_line.AddPoint(mousePos);
		}
	}

	public void Link(GameNode node)
	{
		SelectedNode.Link(node);
		node.Link(SelectedNode);
		var line = new Line2D();
		line.AddPoint(SelectedNode.Position + (node.Position - SelectedNode.Position).Normalized() * 80);
		line.AddPoint(node.Position + (SelectedNode.Position - node.Position).Normalized() * 80);
		line.DefaultColor = new Color(0, 0, 0, 1);
		line.Width = 4;
		line.Name = $"link-{SelectedNode.Name}-{node.Name}";
		Screen.AddChild(line);
		DrawLine = false;
		_line.QueueFree();
	}


	public void StartDrawLine(GameNode node)
	{
		DrawLine = true;
		SelectedNode = node;
		_line = new Line2D();
		_line.DefaultColor = new Color(0, 0, 0, 1);
		Screen.AddChild(_line);
	}

	public void PlaceNode(GameNode node){
		//Add node to screen and start create popup
	}

	public void RegisterGameNode(GameNode node)
	{
		Nodes.Add(node);
	}

	public void UnRegisterGameNode(GameNode node)
	{
		Nodes.Remove(node);
	}

    public override void _Input(InputEvent @event)
    {
        if(DrawLine)
		{
			if(@event is InputEventMouseButton eventMouseButton)
			{
				if(eventMouseButton.ButtonIndex == MouseButton.Right && eventMouseButton.Pressed)
				{
					DrawLine = false;
					_line.QueueFree();
				}
			}
		}
    }
}
