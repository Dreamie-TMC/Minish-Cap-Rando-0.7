using MinishCapRandomizerUI.Elements;
using RandomizerCore.Controllers;
using RandomizerCore.Randomizer.Enumerables;

namespace MinishCapRandomizerUI.UI;

public partial class MinishCapRandomizerUI : Form
{
    private ShufflerController _shufflerController;
    
    public MinishCapRandomizerUI()
    {
        InitializeComponent();
        _shufflerController = new ShufflerController();
        _shufflerController.LoadLogicFile();
        var options = _shufflerController.GetLogicOptions();
        var wrappedOptions = WrappedLogicOptionFactory.BuildGenericWrappedLogicOptions(options);
        var pages = wrappedOptions.GroupBy(option => option.Page);

        foreach (var page in pages)
        {
            TabPane.TabPages.Add(UIGenerator.BuildUIPage(page.ToList(), page.Key));
        }
    }
}