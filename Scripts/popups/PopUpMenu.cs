using Godot;
using System;
using System.Collections.Generic;

public partial class PopUpMenu : Control
{
	private VBoxContainer _vContainer;
	public Button CancelButton;
	public Button SubmitButton;

	public GameNode Node { get; set; }
	public bool IsShadow { get; set; }

	[Signal]
	public delegate void SubmitButtonPressedEventHandler();
	[Signal]
	public delegate void CancelButtonPressedEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_vContainer = GetNode<VBoxContainer>("VContainer");
		CancelButton = GetNode<Button>("CancelButton");
		SubmitButton = GetNode<Button>("SubmitButton");
		//AddPopUpMenuItem("Testing","Value");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void AddPopUpMenuItem(string menuName, string name, string value = "")
	{
		var newPopUpMenuItem = GD.Load<PackedScene>("res://Prefabs/PopUpMenuItem.tscn").Instantiate() as PopUpMenuItem;
		newPopUpMenuItem.Name = menuName;
		newPopUpMenuItem.SizeFlagsVertical = SizeFlags.ExpandFill;
		newPopUpMenuItem.SizeFlagsHorizontal = SizeFlags.Fill;
		_vContainer.AddChild(newPopUpMenuItem);
		newPopUpMenuItem.AddLabel(name);
		newPopUpMenuItem.AddTextEdit(value);
		
	}

	public void AddPopUpMenuList(string menuName)
	{
		var newPopUpMenuList = GD.Load<PackedScene>("res://Prefabs/PopUpMenuList.tscn").Instantiate() as PopUpMenuList;
		newPopUpMenuList.Name = menuName;
		newPopUpMenuList.SizeFlagsVertical = SizeFlags.ExpandFill;
		newPopUpMenuList.SizeFlagsHorizontal = SizeFlags.Fill;
		_vContainer.AddChild(newPopUpMenuList);
	}

	public List<string[]> GetValues()
	{
		var values = new List<string[]>();
		foreach (var item in _vContainer.GetChildren())
		{
			if (item is PopUpMenuItem)
			{
				values.Add((item as PopUpMenuItem).GetValue());
			}
		}
		return values;
	}

	public void Close(){
		QueueFree();
	}

	public void DrawRedBox(string menuName)
	{
		foreach (var item in _vContainer.GetChildren())
		{
			if (item is PopUpMenuItem)
			{
				if ((item as PopUpMenuItem).Name == menuName)
				{
					(item as PopUpMenuItem).DrawRedBox();
				}
			}
		}
	}
}
