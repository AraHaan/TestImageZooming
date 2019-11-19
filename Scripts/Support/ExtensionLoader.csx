#r "System.Windows.Forms"
#load "..\Interfaces\IExtension.csx"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

public static List<IExtension> LoadExtensions()
{
    var extensions = new List<IExtension>();
    var extensionTypes = new List<Type>();
    var types = Assembly.GetExecutingAssembly()/*typeof(IExtension).Assembly*/.GetTypes();
    var typesStr = string.Empty;
    foreach (var type in types)
    {
        typesStr += $"{type.FullName} ({type.GetInterface(typeof(IExtension).FullName)?.FullName}), ";
    }

    // MessageBox.Show($"Types found in this assembly:{Environment.NewLine}{typesStr}", "Info!");
    foreach (var type in types)
    {
        // starts to fail to add the extension types that derive from IExtension to the list.
        // which then prevents them from getting instances created (constructed)
        // (they are in the same assembly as IExtension).
        if (type.GetInterface(/*typeof(IExtension).FullName*/nameof(IExtension)) != null)
        {
            extensionTypes.Add(type);
        }
    }

    // MessageBox.Show($"Found {extensionTypes.Count} types in this assembly.", "Info!");
    foreach (var type in extensionTypes)
    {
        var extension = (IExtension)Activator.CreateInstance(type);
        extensions.Add(extension);
    }

    return extensions;
}
