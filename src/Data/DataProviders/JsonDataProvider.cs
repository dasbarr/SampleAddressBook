using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace SampleAddressBook;

/// <summary>
/// Data provider that uses data from the local JSON file.
/// </summary>
public class JsonDataProvider : IDataProvider<AddressBookEntry>
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

    public JsonDataProvider()
    {
        _jsonFilePath = System.AppDomain.CurrentDomain.BaseDirectory
                        + Properties.Settings.Default.JsonDataSourceFileName;

        LoadDataFromJson();
    }

    //-------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------

    /// <summary>
    /// Contains full path to the JSON file with address book data.
    /// </summary>
    private readonly string _jsonFilePath;

    /// <summary>
    /// Editable observable collection for internal usage.
    /// </summary>
    private ObservableCollection<AddressBookEntry> _internalData;

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
        _internalData.Add(entry);
        SaveDataToJson();
    }

    public void RemoveEntry(AddressBookEntry entry)
    {
        _internalData.Remove(entry);
        SaveDataToJson();
    }

    public void UpdateEntry(AddressBookEntry entry)
    {
        // just save, no need to update Data here
        SaveDataToJson();
    }

    public void Dispose()
    {
        Data = null;
        _internalData = null;
    }

    //-------------------------------------------------------------
    // Protected methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Private methods
    //-------------------------------------------------------------

    /// <summary>
    /// Loads address book data from the disk.
    /// </summary>
    private void LoadDataFromJson()
    {
        _internalData = null;

        if (File.Exists(_jsonFilePath))
        {
            try
            {
                var jsonStr = File.ReadAllText(_jsonFilePath);
                _internalData = JsonConvert.DeserializeObject<ObservableCollection<AddressBookEntry>>(jsonStr);
            }
            catch
            {
                SimpleLogger.Error("Can't read data from JSON file");
            }
        }

        if (_internalData == null)
            _internalData = new(); // create a new collection

        // wrap internal data with read-only collection
        Data = new ReadOnlyObservableCollection<AddressBookEntry>(_internalData);
    }

    /// <summary>
    /// Saves address book data to the disk.
    /// </summary>
    private void SaveDataToJson()
    {
        try
        {
            var serializedData = JsonConvert.SerializeObject(_internalData, Formatting.Indented);
            File.WriteAllText(_jsonFilePath, serializedData);
        }
        catch
        {
            SimpleLogger.Error("Can't save data to JSON file");
        }
    }

    //-------------------------------------------------------------
    // Handlers
    //-------------------------------------------------------------
}
