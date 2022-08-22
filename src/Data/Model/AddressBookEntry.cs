using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SampleAddressBook;

/// <summary>
/// Represents a single entry in the address book.
/// </summary>
public class AddressBookEntry : ICloneable, INotifyPropertyChanged
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

    //-------------------------------------------------------------
    // Variables
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Events
    //-------------------------------------------------------------

    public event PropertyChangedEventHandler PropertyChanged;

    //-------------------------------------------------------------
    // Properties
    //-------------------------------------------------------------

    [JsonIgnore] // useless for storing data in json file
    public int Id { get; init; }

    private string _firstName;
    public string FirstName
    {
        get => _firstName;
        set => SetAndNotify(ref _firstName, value);
        //set {
        //    _firstName = value;
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstName)));
        //    //OnPropertyChanged(nameof(FirstName));
        //}
    }

    private string _lastName;
    public string LastName
    {
        get => _lastName;
        set => SetAndNotify(ref _lastName, value);
    }

    private string _phoneNumber;
    public string PhoneNumber
    {
        get => _phoneNumber;
        set => SetAndNotify(ref _phoneNumber, value);
    }

    private string _email;
    public string Email
    {
        get => _email;
        set => SetAndNotify(ref _email, value);
    }

    //-------------------------------------------------------------
    // Public methods
    //-------------------------------------------------------------

    public AddressBookEntry Clone()
    {
        return this.MemberwiseClone() as AddressBookEntry;
    }
    object ICloneable.Clone() => Clone();

    /// <summary>
    /// Gets values for all settable properties from the source object and
    /// applies them to the corresponding properties of the current object.
    /// </summary>
    /// <param name="source">Source object.</param>
    public void ApplyPropertiesFrom(AddressBookEntry source)
    {
        // get all properties with getter/setter that are not init-only
        var regularProperties = from field in GetType().GetProperties(BindingFlags.Instance 
                                                                      | BindingFlags.Public
                                                                      | BindingFlags.GetProperty
                                                                      | BindingFlags.SetProperty)
                                where !field.IsInitOnly()
                                select field;

        // apply values for that properties
        foreach (var property in regularProperties)
        {
            property.SetValue(this, property.GetValue(source));
        }
    }

    //-------------------------------------------------------------
    // Protected methods
    //-------------------------------------------------------------

    //-------------------------------------------------------------
    // Private methods
    //-------------------------------------------------------------

    /// <summary>
    /// Sets the value for the specified field and notifies that the field was changed.
    /// Should be called from property setters.
    /// </summary>
    /// <typeparam name="T">Field type.</typeparam>
    /// <param name="field">Field.</param>
    /// <param name="value">Value to set.</param>
    /// <param name="propertyName">Name of the caller property.</param>
    private void SetAndNotify<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        field = value;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    //-------------------------------------------------------------
    // Handlers
    //-------------------------------------------------------------
}
