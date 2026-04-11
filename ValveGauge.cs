using Godot;
using System;

public class ValveGauge : Control
{
	[Export(PropertyHint.Range, "0,1,0.01")]
	public float Value { get; set; } = 0.0f;

	private const float BarMargin = 6f;
	private const float TopPadding = 40f;
	private const float BottomPadding = 40f;
	private const float TickCount = 5;

	private DynamicFont font;

	public override void _Ready()
	{
		var fontData = new DynamicFontData();
		fontData.FontPath = "res://font/Roboto-Regular.ttf";
		font = new DynamicFont();
		font.FontData = fontData;
		font.Size = 12;
		font.OutlineSize = 1;
		font.OutlineColor = new Color(0, 0, 0, 0.6f);
	}

	public override void _Process(float delta)
	{
		Update();
	}

	public override void _Draw()
	{
		var size = RectSize;

		// Background panel
		DrawRect(new Rect2(Vector2.Zero, size), new Color(0.1f, 0.1f, 0.1f, 0.7f));
		DrawRect(new Rect2(Vector2.Zero, size), new Color(1f, 1f, 1f, 0.15f), false, 1f);

		// Title
		var title = "VALVE";
		var titleWidth = font.GetStringSize(title).x;
		DrawString(font, new Vector2((size.x - titleWidth) / 2f, 16f), title, new Color(0.85f, 0.85f, 0.85f));

		// "Closed" label just above bar
		var closedColor = Value >= 0.99f ? new Color(0.2f, 0.85f, 1.0f) : new Color(0.85f, 0.85f, 0.85f);
		var closedSize = font.GetStringSize("Closed");
		DrawString(font, new Vector2((size.x - closedSize.x) / 2f, TopPadding - 8f),
			"Closed", closedColor);

		// Bar area
		float barX = BarMargin + 20f;
		float barY = TopPadding;
		float barWidth = Mathf.Max(size.x - barX - BarMargin, 4f);
		float barHeight = Mathf.Max(size.y - TopPadding - BottomPadding, 4f);

		// Bar background
		DrawRect(new Rect2(barX, barY, barWidth, barHeight), new Color(0.05f, 0.05f, 0.05f, 0.9f));
		DrawRect(new Rect2(barX, barY, barWidth, barHeight), new Color(0.4f, 0.4f, 0.4f, 0.5f), false, 1f);

		// Fill from bottom
		float fillHeight = barHeight * Mathf.Clamp(Value, 0f, 1f);
		float fillY = barY + barHeight - fillHeight;

		// Color gradient
		var lowColor = new Color(0.0f, 0.3f, 0.5f, 0.9f);
		var highColor = new Color(0.2f, 0.85f, 1.0f, 0.95f);
		var fillColor = lowColor.LinearInterpolate(highColor, Value);

		DrawRect(new Rect2(barX, fillY, barWidth, fillHeight), fillColor);

		// Glow line at top of fill
		if (Value > 0.01f)
		{
			var glowColor = new Color(0.5f, 0.95f, 1.0f, 0.7f);
			DrawLine(new Vector2(barX, fillY), new Vector2(barX + barWidth, fillY), glowColor, 2f);
		}

		// Tick marks and labels
		for (int i = 0; i <= TickCount; i++)
		{
			float frac = i / TickCount;
			float tickY = barY + barHeight - (barHeight * frac);
			var tickColor = new Color(0.6f, 0.6f, 0.6f, 0.5f);

			// Tick line across bar
			DrawLine(new Vector2(barX, tickY), new Vector2(barX + barWidth, tickY), tickColor, 1f);

			// Label to the left
			string label = $"{(int)(frac * 100)}";
			var labelSize = font.GetStringSize(label);
			DrawString(font, new Vector2(barX - labelSize.x - 3f, tickY + labelSize.y * 0.35f),
				label, new Color(1f, 1f, 1f, 0.9f));
		}

		// "Open" label just below bar
		var openColor = Value <= 0.01f ? new Color(0.2f, 0.85f, 1.0f) : new Color(0.85f, 0.85f, 0.85f);
		var openSize = font.GetStringSize("Open");
		DrawString(font, new Vector2((size.x - openSize.x) / 2f, barY + barHeight + openSize.y + 2f),
			"Open", openColor);
	}
}
