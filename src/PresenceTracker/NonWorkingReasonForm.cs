using PresenceTracker.Application;
using PresenceTracker.Domain;

namespace PresenceTracker;

internal sealed class NonWorkingReasonForm : Form
{
    private readonly ComboBox reason = new() { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Top };

    public BatchAction? SelectedAction { get; private set; }

    public NonWorkingReasonForm(ThemeMode theme)
    {
        Text = "Classificar dia não trabalhado";
        AutoScaleMode = AutoScaleMode.Dpi;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.Sizable;
        MinimizeBox = false;
        MaximizeBox = false;
        Size = new Size(460, 260);
        MinimumSize = new Size(420, 230);

        var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20), ColumnCount = 1, RowCount = 4 };
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        Controls.Add(root);

        var prompt = new Label
        {
            Text = "Qual foi o motivo do dia não trabalhado?", Dock = DockStyle.Top,
            Font = new Font("Segoe UI", 12, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft,
            Margin = new Padding(0, 0, 0, 8)
        };
        UiLayout.BindWrappingLabel(prompt, 28);
        root.Controls.Add(prompt, 0, 0);

        var classification = new Label
        {
            Text = "Classificação", Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleLeft,
            Margin = new Padding(0, 0, 0, 4)
        };
        UiLayout.BindWrappingLabel(classification, 22);
        root.Controls.Add(classification, 0, 1);

        reason.Items.AddRange(new object[] { "Férias", "Day off", "Feriado", "Não trabalhado" });
        reason.SelectedIndex = 0;
        reason.Margin = new Padding(0, 0, 0, 12);
        root.Controls.Add(reason, 0, 2);

        var buttons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft,
            WrapContents = true, AutoSize = true, Margin = Padding.Empty
        };
        var apply = new Button { Text = "Aplicar", Tag = "primary", Padding = new Padding(14, 0, 14, 0), MinimumSize = new Size(100, 36) };
        UiLayout.FitButton(apply, apply.MinimumSize);
        apply.Click += (_, _) =>
        {
            SelectedAction = reason.SelectedIndex switch
            {
                0 => BatchAction.Vacation,
                1 => BatchAction.DayOff,
                2 => BatchAction.Holiday,
                _ => BatchAction.NonWorkingDay
            };
            DialogResult = DialogResult.OK;
            Close();
        };
        var cancel = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Padding = new Padding(14, 0, 14, 0), MinimumSize = new Size(100, 36) };
        UiLayout.FitButton(cancel, cancel.MinimumSize);
        buttons.Controls.Add(apply);
        buttons.Controls.Add(cancel);
        root.Controls.Add(buttons, 0, 3);
        AcceptButton = apply;
        CancelButton = cancel;
        UiTheme.Apply(this, theme);
    }
}
