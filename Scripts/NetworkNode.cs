using Godot;
using System;
using System.Collections.Generic;

public partial class NetworkNode : Placeable, IDraggable
{
	[ExportGroup("Labels")]
	[Export]
	public string NetworkName { get; set; }
	[ExportGroup("Labels")]
	[Export]
	public string AddressSpace { get; set; }

	private Node2D Screen;
	private GameManager gameManager;

	private PopUpMenu popUpMenu;

	public List<string> ConnectedDevices { get; set; }
	public List<string> Subnets { get; set; }
	[Export]
	public RichTextLabel networkNameLabel;
	[Export]
	public RichTextLabel addressSpaceLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (Screen == null)
		{
			Screen = GetNode<Node2D>("/root/GameScreen");
		}
		if (gameManager == null)
		{
			gameManager = GetNode<GameManager>("/root/GameScreen/GameManager");
		}
		gameManager.RegisterPlaceableNode(this);
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
		gameManager.PopUpActive = true;
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
			popUpMenu.Close();
			gameManager.PopUpActive = false;
		}
	}

	private void OnPopUpMenuCancel(){
		popUpMenu.Close();
		gameManager.PopUpActive = false;
	}

    public void HasFocus()
	{
		GD.Print("Has Focus");
		IsFocus = true;

	}
	public void LostFocus()
	{
		GD.Print("Lost Focus");
		IsFocus = false;
	}

	public void OnClick(Node viewport, InputEvent @event, int shape_idx){
		if(@event is null)
		{
			return;
		}
		if(IsFocus && !IsShadow && !gameManager.PopUpActive)
		{
			var mouseEvent = @event as InputEventMouseButton;
			if(mouseEvent is null)
			{
				return;
			}
			if(mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed){
				GD.Print("Clicked");
				//Create new popup
				OpenPopUp();
			}

		}
	}
}
