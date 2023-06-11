using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls.Selection;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaRC11.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
    }

    [RelayCommand]
    private void MoveUpButtonPressed()
    {
        if (Selection.Count == 0) return;
        if (Selection.SelectedIndexes[0] == 0) return;
        var selectedIndexes = Selection.SelectedIndexes.ToList();
        string? temp = null;

        for (var i = 0; i < selectedIndexes.Count; i++)
        {
            temp ??= Items[selectedIndexes[i] - 1];
            Items[selectedIndexes[i] - 1] = Items[selectedIndexes[i]];
            Selection.Select(selectedIndexes[i] - 1);

            if (i != selectedIndexes.Count - 1 && selectedIndexes[i + 1] - selectedIndexes[i] <= 1) continue;
            Items[selectedIndexes[i]] = temp;
            temp = null;
        }
    }

    [RelayCommand]
    private void MoveDownButtonPressed()
    {
        if (Selection.Count == 0) return;
        if (Selection.SelectedIndexes[^1] == Items.Count - 1) return;
        var selectedIndexes = Selection.SelectedIndexes.ToList();
        string? temp = null;

        for (var i = selectedIndexes.Count - 1; i >= 0; i--)
        {
            temp ??= Items[selectedIndexes[i] + 1];
            Items[selectedIndexes[i] + 1] = Items[selectedIndexes[i]];
            Selection.Select(selectedIndexes[i] + 1);

            if (i != 0 && selectedIndexes[i] - selectedIndexes[i - 1] <= 1) continue;
            Items[selectedIndexes[i]] = temp;
            temp = null;
        }
    }

    public ObservableCollection<string> Items { get; set; } = new()
    {
        "One",
        "Two",
        "Three",
        "Four",
        "Five",
        "Six",
        "Seven",
        "Eight",
        "Nine",
        "Ten",
    };

    public SelectionModel<string> Selection { get; } = new()
    {
        SingleSelect = true
    };
}