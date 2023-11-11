using System.Collections.Generic;
using Godot;

public abstract partial class GameNode : Node2D
{
    public GameManager GameManager { get; set; }
    public Node2D Screen { get; set; }

    public List<GameNode> Links { get; set; }

    public override void _Ready()
    {
        GameManager ??= GetNode<GameManager>("/root/GameScreen/GameManager");
        Screen ??= GetNode<Node2D>("/root/GameScreen");
        Links = new List<GameNode>();
        GameManager.RegisterGameNode(this);
    }

    public void Link(GameNode target)
	{
		Links.Add(target);
	}

	public void Unlink(GameNode target)
	{
		Links.Remove(target);
	}
    
}