using Godot;
using System;

public class HelpPanel : Control
{
	private DynamicFont titleFont;
	private DynamicFont font;

	private static readonly string[][] Hotkeys = new string[][] {
		new[] { "H", "Toggle help window" },
		new[] { "L", "Toggle base line" },
		new[] { "D", "Toggle distance label" },
		new[] { "T", "Toggle tongue" },
		new[] { "V", "Toggle valve gauge" },
		new[] { "DPadL/DpadR", "Rotate view (Z-axis)" },
		new[] { "DPadU/DPadD", "Rotate view (X-axis)" },
		new[] { "Shift+DPadL/DpadR", "Rotate view (Y-axis)" },
		new[] { "R", "Reset camera rotation" },
	};

	public override void _Ready()
	{
		Visible = false;
		MouseFilter = MouseFilterEnum.Ignore;

		var fontData = new DynamicFontData();
		fontData.FontPath = "res://font/Roboto-Regular.ttf";

		titleFont = new DynamicFont();
		titleFont.FontData = fontData;
		titleFont.Size = 16;
		titleFont.OutlineSize = 1;
		titleFont.OutlineColor = new Color(0, 0, 0, 0.8f);

		font = new DynamicFont();
		font.FontData = fontData;
		font.Size = 12;
		font.OutlineSize = 1;
		font.OutlineColor = new Color(0, 0, 0, 0.6f);
	}

	public override void _Draw()
	{
		float padX = 16f;
		float padY = 12f;
		float lineHeight = 20f;
		float keyColWidth = 130f;

		float contentHeight = padY + 28f + (Hotkeys.Length * lineHeight) + padY;
		float contentWidth = padX + keyColWidth + 180f + padX;

		// Center in parent
		var parentSize = GetParent<Control>().RectSize;
		float x0 = (parentSize.x - contentWidth) / 2f;
		float y0 = (parentSize.y - contentHeight) / 2f;

		// Background
		DrawRect(new Rect2(x0, y0, contentWidth, contentHeight), new Color(0.08f, 0.08f, 0.08f, 0.9f));
		DrawRect(new Rect2(x0, y0, contentWidth, contentHeight), new Color(0.5f, 0.8f, 1f, 0.3f), false, 1f);

		// Title
		string title = "HOTKEYS";
		float titleW = titleFont.GetStringSize(title).x;
		DrawString(titleFont, new Vector2(x0 + (contentWidth - titleW) / 2f, y0 + padY + 14f),
			title, new Color(0.2f, 0.85f, 1.0f));

		// Divider
		float divY = y0 + padY + 22f;
		DrawLine(new Vector2(x0 + padX, divY), new Vector2(x0 + contentWidth - padX, divY),
			new Color(0.4f, 0.6f, 0.8f, 0.4f), 1f);

		// Rows
		float rowY = divY + lineHeight;
		var keyColor = new Color(1f, 1f, 1f, 0.95f);
		var descColor = new Color(0.75f, 0.75f, 0.75f, 0.9f);

		for (int i = 0; i < Hotkeys.Length; i++)
		{
			DrawString(font, new Vector2(x0 + padX, rowY), Hotkeys[i][0], keyColor);
			DrawString(font, new Vector2(x0 + padX + keyColWidth, rowY), Hotkeys[i][1], descColor);
			rowY += lineHeight;
		}
	}
}
