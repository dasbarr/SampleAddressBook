using System.ComponentModel;
using System.Windows;

namespace SampleAddressBook;

/// <summary>
/// Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow : Window
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

    //-------------------------------------------------------------
    // Constructor/finalizer
    //-------------------------------------------------------------

    public MainWindow()
    {
        InitializeComponent();

        _dataProxy = new();
        // apply address book data to the DataGrid
        addressBookGrid.ItemsSource = _dataProxy.DataProvider.Data;
    }

    //-------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------

    private readonly DataProxy _dataProxy;

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

    private void OnAddEntryButtonClick(object sender, RoutedEventArgs e)
    {
        AddressBookEntry newEntry = new();

        var entryShouldBeAdded = EntryEditWindow.Show(ref newEntry, "Create new entry", "Create");
        if (entryShouldBeAdded)
            _dataProxy.DataProvider.AddEntry(newEntry);
    }

    private void OnEditEntryButtonClick(object sender, RoutedEventArgs e)
    {
        var entryToEdit = ((FrameworkElement)sender).DataContext as AddressBookEntry;

        // make a copy and use it for editing
        var entryToEditCopy = entryToEdit.Clone();

        var changesShouldBeApplied = EntryEditWindow.Show(ref entryToEditCopy, "Edit entry", "Apply");
        if (changesShouldBeApplied)
        {
            // apply changes to the original entry
            entryToEdit.ApplyPropertiesFrom(entryToEditCopy);
            _dataProxy.DataProvider.UpdateEntry(entryToEdit);
        }
    }

    private void OnDeleteEntryButtonClick(object sender, RoutedEventArgs e)
    {
        var deletionQuestionResult = MessageBox.Show("Do you want to delete this entry?", "Entry deletion",
                                                     MessageBoxButton.YesNo, MessageBoxImage.Question,
                                                     MessageBoxResult.No);

        if (deletionQuestionResult == MessageBoxResult.Yes)
        {
            var entry = ((FrameworkElement)sender).DataContext as AddressBookEntry;
            _dataProxy.DataProvider.RemoveEntry(entry);
        }
    }

    private void OnWindowClosing(object sender, CancelEventArgs e)
    {
        _dataProxy.Dispose();
    }
}
