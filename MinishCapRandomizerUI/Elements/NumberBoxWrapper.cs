﻿using MinishCapRandomizerUI.DrawConstants;
using RandomizerCore.Randomizer.Logic.Options;

namespace MinishCapRandomizerUI.Elements;

public class NumberBoxWrapper : WrapperBase
{
    private const int DefaultBottomMargin = 15;
    private const int TextWidth = 225;
    private const int TextHeight = 15;
    private const int NumberBoxWidth = 130;
    private const int NumberBoxHeight = 23;
    private const int NumberBoxAlign = -2;
    private new static readonly int ElementWidth = TextWidth + NumberBoxWidth + Constants.WidthMargin;
    private new const int ElementHeight = TextHeight + DefaultBottomMargin;
    
    private Label? _label;
    private TextBox? _textBox;
    private LogicNumberBox _numberBox;

    public NumberBoxWrapper(LogicNumberBox numberBox) : base(ElementWidth, ElementHeight, numberBox.SettingGroup, numberBox.SettingPage)
    {
        _numberBox = numberBox;
    }

    public override List<Control> GetControls(int initialX, int initialY)
    {
        
        if (_label != null && _textBox != null)
            return new List<Control> { _label, _textBox };

        _label = new Label
        {
            AutoEllipsis = Constants.LabelsAndCheckboxesUseAutoEllipsis,
            AutoSize = false,
            Name = _numberBox.Name,
            Text = _numberBox.NiceName,
            Location = new Point(initialX, initialY),
            Height = TextHeight,
            Width = TextWidth,
            TextAlign = ContentAlignment.MiddleRight,
            UseMnemonic = Constants.UseMnemonic,
        };

        _textBox = new TextBox
        {
            AutoSize = false,
            Name = _numberBox.Name,
            Text = _numberBox.Value,
            Location = new Point(initialX + TextWidth + Constants.WidthMargin, initialY + NumberBoxAlign),
            Height = NumberBoxHeight,
            Width = NumberBoxWidth,
            SelectedText = $"{_numberBox.DefaultValue}",
        };        
        
        if (!string.IsNullOrEmpty(_numberBox.DescriptionText))
        {
            var tip = new ToolTip();
            tip.UseFading = true;
            tip.InitialDelay = 1000;
            tip.ReshowDelay = 500;
            tip.ShowAlways = true;
            tip.SetToolTip(_label, _numberBox.DescriptionText);
        }

        _textBox.TextChanged += (object sender, EventArgs e) =>
        {
            if (_textBox.Text.Length == 0)
                _textBox.Text = @"0";

            if (byte.TryParse(_textBox.Text, out var val))
                _numberBox.Value = val.ToString();
            else
                _textBox.Text = _numberBox.Value;
        };

        return new List<Control> { _label, _textBox };
    }
}