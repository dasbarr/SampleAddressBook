using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace SampleAddressBook;

/// <summary>
/// Data provider that uses data from the local database.
/// </summary>
public class LocalDBDataProvider : IDataProvider<AddressBookEntry>
{
    //-------------------------------------------------------------
    // Nested
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Class constants
    //-------------------------------------------------------------

    /// <summary>
    /// That message will be added to the log on saving attempt when
    /// database is not available.
    /// </summary>
    private const string cDummyDataSaveMessage = "Local database is not available, can't save";

    //-------------------------------------------------------------
    // Class variables
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Class methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Constructor/finalizer
    //-------------------------------------------------------------

    public LocalDBDataProvider()
    {
        try
        {
            _dbContext = new();
            _dbContext.AddressBookData.Load();

            var internalData = _dbContext.AddressBookData.Local.ToObservableCollection();
            // wrap internal data with read-only collection
            Data = new ReadOnlyObservableCollection<AddressBookEntry>(internalData);
        }
        catch
        {
            SimpleLogger.Error("Can't load local database");
            
            _dbContext = null;

            // init dummy data (saving to the disk will not be available in that case)
            _dummyData = new();
            Data = new ReadOnlyObservableCollection<AddressBookEntry>(_dummyData);
        }
    }

    //-------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------

    /// <summary>
    /// Local database context.
    /// </summary>
    private AddressBookDBContext _dbContext;

    /// <summary>
    /// Dummy data, used for maintaining app functionality without local
    /// database in case when that database can't be loaded. Note that
    /// saving address book to disk is not available in that case.
    /// </summary>
    private ObservableCollection<AddressBookEntry> _dummyData;

    //-------------------------------------------------------------
    // Events
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Properties
    //-------------------------------------------------------------

    public ReadOnlyObservableCollection<AddressBookEntry> Data { get; private set; }

    //-------------------------------------------------------------
    // Public methods
    //-------------------------------------------------------------

    public void AddEntry(AddressBookEntry entry)
    {
        if (_dbContext != null)
        {
            _dbContext.AddressBookData.Add(entry);
            _dbContext.SaveChanges();
        }
        else
        {
            SimpleLogger.Error(cDummyDataSaveMessage);

            // work with dummy data without saving
            _dummyData?.Add(entry);
        }
    }

    public void RemoveEntry(AddressBookEntry entry)
    {
        if (_dbContext != null)
        {
            _dbContext.AddressBookData.Remove(entry);
            _dbContext.SaveChanges();
        }
        else
        {
            SimpleLogger.Error(cDummyDataSaveMessage);

            // work with dummy data without saving
            _dummyData?.Remove(entry);
        }
    }

    public void UpdateEntry(AddressBookEntry entry)
    {
        if (_dbContext != null)
        {
            _dbContext.AddressBookData.Update(entry);
            _dbContext.SaveChanges();
        }
        else
        {
            SimpleLogger.Error(cDummyDataSaveMessage);

            // explicit update is not necessary for dummy data
        }
    }

    public void Dispose()
    {
        Data = null;
        _dummyData = null;

        _dbContext?.Dispose();
        _dbContext = null;
    }

    //-------------------------------------------------------------
    // Protected methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Private methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Handlers
    //-------------------------------------------------------------
}
