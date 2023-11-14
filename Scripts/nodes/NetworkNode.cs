using Godot;
using System;
using System.Collections.Generic;

public partial class NetworkNode : GameNode, IPlaceable, IDraggable
{
	[ExportGroup("Labels")]
	[Export]
	public string NetworkName { get; set; }
	[ExportGroup("Labels")]
	[Export]
	public string AddressSpace { get; set; }

	private PopUpMenu popUpMenu;

	public List<string> ConnectedDevices { get; set; }
	public List<string> Subnets { get; set; }
	[Export]
	public RichTextLabel networkNameLabel;
	[Export]
	public RichTextLabel addressSpaceLabel;

	public bool IsShadow { get; set; }
	public bool IsPlaced { get; set; } = false;
	public bool IsFocus { get; set; }

	[Signal]
	public delegate void NewNodePlacedEventHandler(NetworkNode placeable);


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		networkNameLabel.Text = "[center]" + NetworkName + "[/center]";
		addressSpaceLabel.Text = "[center]" + AddressSpace + "[/center]";
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(IsShadow){
			this.Position = GetGlobalMousePosition();
		}
		base._Process(delta);
	}

	public void Place(){
		IsShadow = false;
		OpenPopUp();
	}

	private void OpenPopUp()
	{
		popUpMenu = GD.Load<PackedScene>("res://Prefabs/PopUpMenu.tscn").Instantiate() as PopUpMenu;
		popUpMenu.Size = new Vector2(360,202);
		Screen.AddChild(popUpMenu);
		//Center popup
		var screenSize = GetViewportRect().Size;
		popUpMenu.Position = new Vector2(screenSize.X/2 - popUpMenu.Size.X/2, screenSize.Y/2 - popUpMenu.Size.Y/2);
		popUpMenu.SubmitButton.Pressed += OnPopUpMenuSubmit;
		popUpMenu.CancelButton.Pressed += OnPopUpMenuCancel;
		popUpMenu.AddPopUpMenuItem("networkName","Network Name", NetworkName);
		popUpMenu.AddPopUpMenuItem("addressSpace","Address Space", AddressSpace);
		popUpMenu.Node = this;
		popUpMenu.IsShadow = IsShadow;
		GameManager.PopUpActive = true;
	}

	private void OnPopUpMenuSubmit(){
		var values = popUpMenu.GetValues();
		foreach(var item in values){
			if(item[0] == "Network Name"){
				NetworkName = item[1];
				networkNameLabel.Text = "[center]" + NetworkName + "[/center]";
			}
			if(item[0] == "Address Space"){
				AddressSpace = item[1];
				addressSpaceLabel.Text = "[center]" + AddressSpace + "[/center]";
			}
		}
		if(NetworkName == ""){
			popUpMenu.DrawRedBox("networkName");
		}
		if(AddressSpace == ""){
			popUpMenu.DrawRedBox("addressSpace");
		}

		if(NetworkName != "" && AddressSpace != "")
		{
			IsPlaced = true;
			popUpMenu.Close();
			GameManager.PopUpActive = false;
		}
	}

	private void OnPopUpMenuCancel(){
		popUpMenu.Close();
		GameManager.LockUI.Start();
		if(!IsPlaced)
		{
			QueueFree();
		}
		
	}

	private void OpenMenu()
	{
		var menuPopUp = GD.Load<PackedScene>("res://Prefabs/PopUpMenuNetwork.tscn").Instantiate() as PopUpMenuNetwork;
		Screen.AddChild(menuPopUp);
		//Open Menu at cursor
		menuPopUp.Position = GetGlobalMousePosition();
		menuPopUp.GameManager = GameManager;
		menuPopUp.Source = this;
	}

    public void HasFocus()
	{
		IsFocus = true;

	}
	public void LostFocus()
	{
		IsFocus = false;
	}

	public void OnClick(Node viewport, InputEvent @event, int shape_idx){
		if(@event is null)
		{
			return;
		}
		if(IsFocus && !IsShadow && !GameManager.PopUpActive && !GameManager.DrawLine)
		{
			var mouseEvent = @event as InputEventMouseButton;
			if(mouseEvent is null)
			{
				return;
			}
			if(mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed){
				//Create new popup
				OpenPopUp();
			}
			if(mouseEvent.ButtonIndex == MouseButton.Right && mouseEvent.Pressed){
				//Create new popup
				OpenMenu();
			}
		}
		if(IsFocus && !IsShadow && !GameManager.PopUpActive && GameManager.DrawLine)
		{
			var mouseEvent = @event as InputEventMouseButton;
			if(mouseEvent is null)
			{
				return;
			}
			if(mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed){
				//Create new popup
				GameManager.Link(this);
			}
		}
	}
}
