#load "..\Interfaces\IExtension.csx"
#load "..\Extensions\Extensions.csx"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public static List<IExtension> LoadExtensions()
{
    List<IExtension> extensions = new List<IExtension>();
    List<Type> extensionTypes = new List<Type>();
    var types = typeof(IExtension).Assembly.GetTypes();
    foreach (var type in types)
    {
        // starts to fail to add the extension types that derive from IExtension to the list.
        // which then prevents them from getting instances created (constructed)
		// (they are in the same assembly as IExtension).
        if (type.GetInterface(typeof(IExtension).FullName) != null)
        {
            extensionTypes.Add(type);
        }
    }

    foreach (var type in extensionTypes)
    {
        var extension = (IExtension)Activator.CreateInstance(type);
        extensions.Add(extension);
    }

    return extensions;
}
