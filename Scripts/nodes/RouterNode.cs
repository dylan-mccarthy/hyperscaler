using Godot;
using System;
using System.Collections.Generic;

public partial class RouterNode: GameNode, IPlaceable, IDraggable
{
    [ExportGroup("Labels")]
    [Export]
    public string RouterName { get; set; }

    private PopUpMenu popUpMenu;

    public NetworkNode ConnectedNetwork { get; set; }
    [Export]
    public RichTextLabel routerNameLabel;

    public bool IsShadow { get; set; }
    public bool IsPlaced { get; set; } = false;
    public bool IsFocus { get; set; }

    [Signal]
    public delegate void NewRouterNodePlacedEventHandler(RouterNode placable);

    public override void _Ready()
    {
        routerNameLabel.Text = "[center]" + RouterName + "[/center]";
        base._Ready();
    }

    public override void _Process(double delta)
    {
        if(IsShadow){
            this.Position = GetGlobalMousePosition();
        }
        base._Process(delta);
    }

    public void Place()
    {
        IsShadow = false;
        OpenPopUp();
    }

    public void OpenPopUp()
    {
        GD.Print("OpenPopUp");
        popUpMenu = GD.Load<PackedScene>("res://Prefabs/PopUpMenu.tscn").Instantiate() as PopUpMenu;
        popUpMenu.Size = new Vector2(360,202);
        Screen.AddChild(popUpMenu);
        //Center popup
        var screenSize = GetViewportRect().Size;
        popUpMenu.Position = new Vector2(screenSize.X/2 - popUpMenu.Size.X/2, screenSize.Y/2 - popUpMenu.Size.Y/2);
        popUpMenu.SubmitButton.Pressed += OnPopUpMenuSubmit;
        popUpMenu.CancelButton.Pressed += OnPopUpMenuCancel;
        popUpMenu.AddPopUpMenuItem("routerName","Router Name", RouterName);
        if(IsPlaced)
        {
            popUpMenu.AddPopUpMenuList("ipRoutes");
        }
        popUpMenu.Node = this;
        popUpMenu.IsShadow = IsShadow;
        GameManager.PopUpActive = true;
    }

    private void OnPopUpMenuSubmit()
    {
        var values  = popUpMenu.GetValues();
        foreach(var item in values){
            if(item[0] == "Router Name"){
                RouterName = item[1];
                routerNameLabel.Text = "[center]" + RouterName + "[/center]";
            }
        }

        if(RouterName == "")
        {
            popUpMenu.DrawRedBox("routerName");
        }

        if(RouterName != "")
        {
            IsPlaced = true;
            popUpMenu.Close();
            GameManager.LockUI.Start();
        }
        
    }

    private void OnPopUpMenuCancel()
    {
        popUpMenu.Close();
        GameManager.LockUI.Start();
        if(!IsPlaced)
        {
            QueueFree();
        }
    }

    private void OpenMenu()
    {

    }

    public void HasFocus()
    {
        IsFocus = true;
    }

    public void LostFocus()
    {
        IsFocus = false;
    }

    public void OnClick(Node viewport, InputEvent @event, int shape_idx)
    {
        if(@event is null)
        {
            return;
        }
        if(IsFocus && !IsShadow && !GameManager.PopUpActive)
        {
            var mouseEvent = @event as InputEventMouseButton;
            if(mouseEvent is null)
            {
                return;
            }
            if(mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
            {
                OpenPopUp();
            }
            if(mouseEvent.ButtonIndex == MouseButton.Right && mouseEvent.Pressed)
            {
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