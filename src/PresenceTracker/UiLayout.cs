namespace PresenceTracker;

internal static class UiLayout
{
    public static Size MeasureText(string text, Font font, int maxWidth = int.MaxValue, TextFormatFlags flags = TextFormatFlags.Default)
    {
        if (string.IsNullOrEmpty(text))
            return Size.Empty;
        return TextRenderer.MeasureText(
            text,
            font,
            new Size(maxWidth <= 0 ? int.MaxValue : maxWidth, int.MaxValue),
            flags | TextFormatFlags.NoPadding);
    }

    public static Size PreferredButtonSize(string text, Font font, Padding padding, Size minimum, int extraHorizontal = 20, int extraVertical = 12)
    {
        var measured = MeasureText(text, font);
        return new Size(
            Math.Max(minimum.Width, measured.Width + padding.Horizontal + extraHorizontal),
            Math.Max(minimum.Height, measured.Height + padding.Vertical + extraVertical));
    }

    public static void FitButton(Button button, Size? minimum = null)
    {
        var min = minimum ?? new Size(72, 36);
        button.AutoSize = true;
        button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        button.MinimumSize = min;
        button.Size = PreferredButtonSize(button.Text, button.Font, button.Padding, min);
    }

    public static void EnableWrapping(Label label)
    {
        label.AutoSize = false;
        label.AutoEllipsis = false;
        label.MaximumSize = Size.Empty;
        // UseCompatibleTextRendering keeps word wrap stable across DPI/Windows versions.
        label.UseCompatibleTextRendering = true;
    }

    public static void ResizeListColumns(ListView list, params (float Weight, int MinWidth)[] columns)
    {
        if (list.Columns.Count == 0 || columns.Length == 0 || list.Columns.Count != columns.Length)
            return;

        var available = Math.Max(columns.Sum(c => c.MinWidth), list.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 4);
        var totalWeight = columns.Sum(c => c.Weight);
        if (totalWeight <= 0) totalWeight = columns.Length;

        var widths = new int[columns.Length];
        var assigned = 0;
        for (var i = 0; i < columns.Length; i++)
        {
            var ideal = (int)Math.Round(available * (columns[i].Weight / totalWeight));
            widths[i] = Math.Max(columns[i].MinWidth, ideal);
            assigned += widths[i];
        }

        var delta = available - assigned;
        widths[^1] = Math.Max(columns[^1].MinWidth, widths[^1] + delta);
        for (var i = 0; i < widths.Length; i++)
            list.Columns[i].Width = widths[i];
    }
}
