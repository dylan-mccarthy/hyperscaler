using Godot;
using System;
using System.Collections.Generic;

public partial class RouterNode: GameNode, IPlaceable, IDraggable
{
    [ExportGroup("Labels")]
    [Export]
    public string RouterName { get; set; }

    private PopUpMenu popUpMenu;

    [Export]
    public RichTextLabel routerNameLabel;

    public bool IsFocus { get; set; }
    public bool IsShadow { get; set; }


    public override void _Ready()
    {
        routerNameLabel.Text = "[center]" + RouterName + "[/center]";
    }

    public override void _Process(double delta)
    {
        if(IsShadow){
            this.Position = GetGlobalMousePosition();
        }
    }

    public void Place()
    {
        IsShadow = false;
        OpenPopUp();
    }

    public void OpenPopUp()
    {

    }

    private void OnPopUpSubmit()
    {

    }

    private void OnPopUpCancel()
    {

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

    public void OnClick(InputEvent @event)
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
    }
}