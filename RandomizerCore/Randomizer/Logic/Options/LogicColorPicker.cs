﻿using System.Drawing;
using RandomizerCore.Randomizer.Enumerables;
using RandomizerCore.Randomizer.Logic.Defines;
using RandomizerCore.Utilities.Models;
using RandomizerCore.Utilities.Util;

namespace RandomizerCore.Randomizer.Logic.Options;

public class LogicColorPicker : LogicOptionBase
{
    public LogicColorPicker(string name, string niceName, string settingPage, string settingGroup, LogicOptionType type,
        Color startingColor) :
        base(name, niceName, true, settingGroup, settingPage, type)
    {
        BaseColor = startingColor;
        DefinedColor = startingColor;
        InitialColors = new List<Color>(1)
        {
            startingColor
        };
    }

    public LogicColorPicker(string name, string niceName, string settingPage, string settingGroup, LogicOptionType type,
        List<Color> colors) :
        base(name, niceName, true, settingGroup, settingPage, type)
    {
        BaseColor = colors[0];
        DefinedColor = colors[0];
        InitialColors = colors;
    }

    public Color BaseColor { get; set; }
    public Color DefinedColor { get; set; }
    public List<Color> InitialColors { get; set; }

    public override List<LogicDefine> GetLogicDefines()
    {
        var defineList = new List<LogicDefine>(3);

        // Only true if a color has been selected
        if (!Active) return defineList;

        defineList.Add(new LogicDefine(Name));

        defineList.AddRange(InitialColors
            .Select(color => new GbaColor(ColorUtil.AdjustHue(color, BaseColor, DefinedColor)))
            .Select((newColor, i) => new LogicDefine(Name + "_" + i, StringUtil.AsStringHex4(newColor.CombinedValue))));

        return defineList;
    }

    public override byte GetHashByte()
    {
        // Maybe not a great way to represent, leaves some info out and is likely to cause easy collisions
        return Active ? (byte)(DefinedColor.R ^ DefinedColor.G ^ DefinedColor.B) : (byte)00;
    }
}