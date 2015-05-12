Abstract
========
Sgry.Ini is an INI file format parser written in pure C#. It is written
in a single source file under less-restrictive zlib/libpng license.

Supported INI Format
====================
For detail about the INI data format itself, please refer to Wikipedia:

- [INI file (English)](http://en.wikipedia.org/wiki/INI_file)

Below is how this class parses given INI data:

- Empty lines
  - Ignores
- Comment line
  - A line starting with semi-colon (;) is recognized as comment
  - Ignores whitespaces at the beginning of each line
  - No comments allowed after a property definition line nor a section starting
    line
- Section starting line
  - A line starting with '[' and ends with ']' is recognized as the start of a
    new section
  - Ignores whitespaces at the beginning and the end of each line
  - Substring between the first '[' and ']' becomes the name of the section
- Property definition line
  - A line which is none of the above, and which includes an equal sign '=' is
    recognized as a property definition line
  - Substring between line-beginning and the first equal sign '=' is recognized
    as the name of the property
  - Substring after the first equal sign '=' in a property line is recognized
    as the value of the property
  - Whitespaces surrounding property name is ignored
- Other
  - Can specify whether to ignore case of section and/or property names
  - Can recognize so-called "global properties"
    - To access use them, specify an empty string for section name.
  - Does not support escape characters such as "\n" nor "\x0041"
  - Does not support custom key/value delimiter (only '=' is supported)


Feature
=======
Load from or save to files and strings
--------------------------------------
To load analyze INI document content, use IniDocument.Load method. Load method
reads data from System.IO.TextReader so you can specify a
System.IO.StreamReader to read data in a file, or a System.IO.StringReader to
read data on the memory (string variable). See example below:

    var ini = new IniDocument();
    
    // Load INI data in a file
    using( var file = new StreamReader("data.ini", Encoding.UTF8) )
        ini.Load( file );
    
    // Load INI data on the memory (String object)
    var str = "...(INI data here)...";
    ini.Load( new StringReader(str) );

To serialize INI document content, use IniDocument.Save method. Save method
writes serialized data to a System.IO.TextWriter. Next is an example code:

    var ini = new IniDocument();
    
    // Sets some values
    ini.Set("", "foo", "bar");
    ini.Set("[section]", "Foo", "Bar");
    
    // Save INI data to a file
    using( var file = new StreamWriter("data.ini", false, Encoding.UTF8) )
    {
        file.NewLine = "\r\n";
        ini.Save( file );
    }
    
    // Save (serialize) INI data on the memory (as a StringBuilder object)
    var buf = new StringBuilder();
    var writer = new StringWriter( buf );
    writer.NewLine = "\r\n";
    ini.Save( new StringWriter(buf) );

Note that IniDocument.Set method does not write sections without a property. If
you need to write such section, you need to implement your own serialization
logic to do it by using IniDocument.Sections property.


Getting values
--------------
Getting values can be done with IniDocument.Get method. See example below:

    /* Content of the file "data.ini":
    [Profile]
    Name=Suguru
    IsGeek=True
    Age=31
    */
    
    // Load and parse INI document "data.ini"
    var ini = new IniDocument();
    using( var file = new StreamReader("data.ini", Encoding.UTF8) )
        ini.Load( file );
    
    // Get property values in various types
    ini.Get( "Profile", "Name", null ); // "Suguru" が返る
    ini.Get( "Profile", "IsGeek", false ); // true が返る
    ini.GetInt( "Profile", "Age", 0, Int32.MaxValue, 0 ); // 31 が返る
    ini.Get( "Profile", "Address", "unknown" ); // "unknown" が返る

Get method takes "default value" as the third parameter. This value will be
returned when the specified property was not found. Also, there are TryGet
method which returns whether the property was found or not.

If you read a property whose value is an integer, GetInt method may be useful
because it does not only get the value but also checks whether the value is in
a specified range. 


Adding or changing values
-------------------------
To add a new property or to change an existing property's value, use
IniDocument.Set method. If specified property does not exist, a new property
will be created in the IniDocument. Otherwise value of the existing property
will be overwritten. Note that Set method can take any objects as value. See
example below:

    var ini = new IniDocument();
    ini.Set( "Profile", "Name", "Suguru" );
    ini.Set( "Profile", "Name", "Suguru Yamamoto" );
    ini.Set( "Profile", "Age", 31 );
    ini.Save( System.Console.Out );
    
    // The code above outputs text below in the console window:
    //[Profile]
    //Name=Suguru Yamamoto
    //Age=31


Removing properties and sections
--------------------------------
To remove sections or properties in a IniDocument, use IniDocument.Remove
method.


Before using this library
=========================
This library is distributed under the [zlib/libpng license](LICENSE.md).
Note that zlib/libpng license is less restrictive for redistribution in the
form of source files (not built binary files). If you want to use this library
in a form of souce files, just pick up Source/Sgry.Ini/Ini.cs.
