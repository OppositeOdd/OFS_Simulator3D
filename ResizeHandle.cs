using Godot;
using System;

public enum ResizeHandleType
{
	Top,
	Bottom,
	Left,
	Right
}

public partial class ResizeHandle : Control
{
	[Export]
	private ResizeHandleType handleType;
	private bool isResizing = false;
	private Vector2 resizePos = Vector2.Zero;
	private Vector2I startSize = Vector2I.Zero;

	public override void _Ready()
	{
		switch(handleType)
		{
			case ResizeHandleType.Top:
			case ResizeHandleType.Bottom:
				MouseDefaultCursorShape = CursorShape.Vsize;
				break;
			case ResizeHandleType.Left:
			case ResizeHandleType.Right:
				MouseDefaultCursorShape = CursorShape.Hsize;
				break;
		}
	}

	public override void _GuiInput(InputEvent ev)
	{
		if(ev is InputEventMouseButton button)
		{
			if(button.ButtonIndex == MouseButton.Left)
			{
				if(button.Pressed && !isResizing) {
					isResizing = true;
					startSize = DisplayServer.WindowGetSize();
					resizePos = GetGlobalMousePosition();
				}
				else
					isResizing = false;
			}
		}
	}

	public override void _Process(double delta)
	{
		if(isResizing)
		{
			var mousePos = GetGlobalMousePosition();
			switch(handleType)
			{
				case ResizeHandleType.Top:
				{
					var currentPos = DisplayServer.WindowGetPosition();
					var newPos = currentPos + (Vector2I)(mousePos - resizePos);
					newPos.X = currentPos.X;
					var deltaPos = currentPos - newPos;
					startSize += new Vector2I(0, deltaPos.Y);
					DisplayServer.WindowSetPosition(newPos);
					DisplayServer.WindowSetSize(startSize);
					break;
				}
				case ResizeHandleType.Bottom:
					DisplayServer.WindowSetSize(startSize + new Vector2I(0, (int)(mousePos.Y - resizePos.Y)));
					break;
				case ResizeHandleType.Left:
				{
					var currentPos = DisplayServer.WindowGetPosition();
					var newPos = currentPos + (Vector2I)(mousePos - resizePos);
					newPos.Y = currentPos.Y;
					var deltaPos = currentPos - newPos;
					startSize += new Vector2I(deltaPos.X, 0);
					DisplayServer.WindowSetPosition(newPos);
					DisplayServer.WindowSetSize(startSize);
					break;
				}
				case ResizeHandleType.Right:
					DisplayServer.WindowSetSize(startSize + new Vector2I((int)(mousePos.X - resizePos.X), 0));
					break;
			}
		}
	}
}
