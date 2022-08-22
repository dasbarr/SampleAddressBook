using System.Windows;

namespace SampleAddressBook;

/// <summary>
/// Interaction logic for EntryEditWindow.xaml. Window allows address book entry editing.
/// </summary>
public partial class EntryEditWindow : Window
{
    //-------------------------------------------------------------
    // Nested
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Class constants
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Class variables
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Class methods
    //-------------------------------------------------------------

    /// <summary>
    /// Shows the window.
    /// </summary>
    /// <param name="entryToEdit">Entry that will be edited in that window.</param>
    /// <param name="title">Window title.</param>
    /// <param name="applyButtonLabel">Text on the 'Apply' button.</param>
    /// <returns></returns>
    public static bool Show(ref AddressBookEntry entryToEdit, string title, string applyButtonLabel)
    {
        var editWindow = new EntryEditWindow(ref entryToEdit, title, applyButtonLabel);
        return editWindow.ShowDialog() ?? false;
    }

    //-------------------------------------------------------------
    // Constructor/finalizer
    //-------------------------------------------------------------

    private EntryEditWindow(ref AddressBookEntry entryToEdit, string title, string applyButtonLabel)
    {
        InitializeComponent();

        // set window params
        Title = title;
        ApplyButton.Content = applyButtonLabel;

        // bind entry
        DataContext = entryToEdit;
    }

    //-------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Events
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Properties
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Public methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Protected methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Private methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Handlers
    //-------------------------------------------------------------

    private void OnApplyButtonClick(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
    }

    private void OnCancelButtonClick(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
    }
}
