// See LICENSE.md for license terms of usage.
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAttribute = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;

namespace Sgry.Ini.Tests
{
	enum MyEnum
	{
		Foo = 1,
		Bar = 2
	}

	[TestClass]
	public class IniDocumentTest
	{
		#region Public methods and properties
		[Test]
		public void Get_T()
		{
			var ini = new IniDocument();

			MyAssert.Throws<ArgumentNullException>( ()=>{
				ini.Get( null, "foo", "" );
			} );
			MyAssert.DoesNotThrow( ()=>{
				ini.Get( "", "foo", "" );
			} );
			MyAssert.Throws<ArgumentNullException>( ()=>{
				ini.Get( "section", null, "" );
			} );
			MyAssert.Throws<ArgumentException>( ()=>{
				ini.Get( "section", "", "" );
			} );

			Assert.AreEqual( "1", ini.Get("no_such_section", "foo", "1") );
			Assert.AreEqual( 1, ini.Get("no_such_section", "foo", 1) );
			Assert.AreEqual( MyEnum.Foo, ini.Get("no_such_section", "foo", MyEnum.Foo) );

			ini.Set( "section", "foo", "2" );
			Assert.AreEqual( "1", ini.Get("no_such_section", "foo", "1") );
			Assert.AreEqual( 1, ini.Get("no_such_section", "foo", 1) );
			Assert.AreEqual( MyEnum.Foo, ini.Get("no_such_section", "foo", MyEnum.Foo) );
			Assert.AreEqual( "1", ini.Get("section", "no_such_property", "1") );
			Assert.AreEqual( 1, ini.Get("section", "no_such_property", 1) );
			Assert.AreEqual( MyEnum.Foo, ini.Get("section", "no_such_property", MyEnum.Foo) );
			Assert.AreEqual( "2", ini.Get("section", "foo", "1") );
			Assert.AreEqual( 2, ini.Get("section", "foo", 1) );
			Assert.AreEqual( MyEnum.Bar, ini.Get("section", "foo", MyEnum.Foo) );
		}

		[Test]
		public void Get()
		{
			var ini = new IniDocument();

			MyAssert.Throws<ArgumentNullException>( ()=>{
				ini.Get( null );
			} );
			MyAssert.DoesNotThrow( ()=>{
				ini.Get( "" );
			} );

			Assert.AreEqual( null, ini.Get("no_such_section") );

			ini.Set( "section", "foo", "2" );
			Assert.AreEqual( null, ini.Get("no_such_section") );
			Assert.AreNotEqual( null, ini.Get("section") );
		}

		[Test]
		public void GetInt()
		{
			var ini = new IniDocument();

			MyAssert.Throws<ArgumentNullException>( ()=>{
				ini.GetInt( null, "foo", 0, 0, 0 );
			} );
			MyAssert.DoesNotThrow( ()=>{
				ini.GetInt( "", "foo", 0, 0, 0 );
			} );
			MyAssert.Throws<ArgumentNullException>( ()=>{
				ini.GetInt( "section", null, 0, 0, 0 );
			} );
			MyAssert.Throws<ArgumentException>( ()=>{
				ini.GetInt( "section", "", 0, 0, 0 );
			} );

			Assert.AreEqual( 0, ini.GetInt("no_such_section", "foo", Int32.MinValue, Int32.MaxValue, 0) );

			ini.Set( "section", "foo", 2 );
			Assert.AreEqual( 1, ini.GetInt("no_such_section", "foo", 0, 9, 1) );
			Assert.AreEqual( 1, ini.GetInt("section", "no_such_property", 0, 9, 1) );
			Assert.AreEqual( 2, ini.GetInt("section", "foo", 0, 9, 1) );
			Assert.AreEqual( 1, ini.GetInt("section", "foo", 0, 1, 1) ); // out of range
			Assert.AreEqual( 1, ini.GetInt("section", "foo", 3, 4, 1) ); // out of range
		}

		[Test]
		public void TryGet()
		{
			var ini = new IniDocument();
			string str;
			int num;
			MyEnum enumValue;

			MyAssert.Throws<ArgumentNullException>( ()=>{
				ini.TryGet( null, "foo", out str );
			} );
			MyAssert.DoesNotThrow( ()=>{
				ini.TryGet( "", "foo", out str );
			} );
			MyAssert.Throws<ArgumentNullException>( ()=>{
				ini.TryGet( "section", null, out str );
			} );
			MyAssert.Throws<ArgumentException>( ()=>{
				ini.TryGet( "section", "", out str );
			} );

			Assert.AreEqual( false, ini.TryGet("no_such_section", "foo", out str) );
			Assert.AreEqual( false, ini.TryGet("no_such_section", "foo", out num) );
			Assert.AreEqual( false, ini.TryGet("no_such_section", "foo", out enumValue) );

			ini.Set( "section", "foo", "2" );
			Assert.AreEqual( false, ini.TryGet("no_such_section", "foo", out str) );
			Assert.AreEqual( false, ini.TryGet("no_such_section", "foo", out num) );
			Assert.AreEqual( false, ini.TryGet("no_such_section", "foo", out enumValue) );
			Assert.AreEqual( true, ini.TryGet("section", "foo", out str) );
			Assert.AreEqual( true, ini.TryGet("section", "foo", out num) );
			Assert.AreEqual( true, ini.TryGet("section", "foo", out enumValue) );
			Assert.AreEqual( "2", str );
			Assert.AreEqual( 2, num );
			Assert.AreEqual( MyEnum.Bar, enumValue );
		}

		[Test]
		public void TryGetInt()
		{
			var ini = new IniDocument();
			int num;

			ini.Set( "section", "foo", 2 );
			Assert.AreEqual( false, ini.TryGetInt("section", "foo", 0, 1, out num) );

			num = 0;
			Assert.AreEqual( true,  ini.TryGetInt("section", "foo", 0, 2, out num) );
			Assert.AreEqual( 2, num );

			num = 0;
			Assert.AreEqual( true,  ini.TryGetInt("section", "foo", 2, 4, out num) );
			Assert.AreEqual( 2, num );

			Assert.AreEqual( false, ini.TryGetInt("section", "foo", 3, 4, out num) );

			ini.Set( "section", "foo", "bar" );
			Assert.AreEqual( false, ini.TryGetInt("section", "foo",
												  Int32.MinValue, Int32.MaxValue,
												  out num) );
		}

		[Test]
		public void Set()
		{
			var ini = new IniDocument();

			MyAssert.Throws<ArgumentNullException>( ()=>{
				ini.Set( null, "foo", "bar" );
			} );
			MyAssert.DoesNotThrow( ()=>{
				ini.Set( "", "foo", "bar" );
			} );
			MyAssert.Throws<ArgumentNullException>( ()=>{
				ini.Set( "section", null, "bar" );
			} );
			MyAssert.Throws<ArgumentException>( ()=>{
				ini.Set( "section", "", "bar" );
			} );

			ValidateStatus( ini );

			ini.Clear();
			Assert.AreEqual( 0, ini.Count );
			Assert.AreEqual( 0, ini.Sections.Count );

			ValidateStatus( ini );

			ini.Set( "section", "foo", "bar" );
			Assert.AreEqual( "bar", ini.Get("section", "foo", "") );
			Assert.AreEqual( 1, ini.Count );
			Assert.AreEqual( 1, ini.Sections.Count );

			ValidateStatus( ini );

			ini.Set( "section", "hoge", "piyo" );
			Assert.AreEqual( "bar", ini.Get("section", "foo", "") );
			Assert.AreEqual( "piyo", ini.Get("section", "hoge", "") );
			Assert.AreEqual( 1, ini.Count );
			Assert.AreEqual( 1, ini.Sections.Count );
			Assert.AreEqual( 2, ini.Sections["section"].Count );
			Assert.AreEqual( 2, ini.Sections["section"].Properties.Count );

			ValidateStatus( ini );
		}

		[Test]
        public void Remove_Section()
		{
			MyAssert.Throws<ArgumentNullException>( () => new IniDocument().Remove(null) );
			MyAssert.DoesNotThrow( () => new IniDocument().Remove("") );

			var ini = new IniDocument();

			ini.Remove( "" );
			Assert.AreEqual( 0, ini.Count );
			ValidateStatus( ini );

			ini.Set( "", "foo", "bar" );
			ini.Set( "section", "hoge", "piyo" );
			Assert.AreEqual( 2, ini.Count );

			// Removing a section
			ini.Remove( "" );
			Assert.AreEqual( 1, ini.Count );
			ValidateStatus( ini );

			// Removing non-existing (already removed) section
			ini.Remove( "" );
			Assert.AreEqual( 1, ini.Count );
			ValidateStatus( ini );

			// By removing a section, make the INI document emtpy
			ini.Remove( "section" );
			Assert.AreEqual( 0, ini.Count );
			ValidateStatus( ini );
		}

		[Test]
        public void Remove_Property()
		{
			MyAssert.Throws<ArgumentNullException>( () => new IniDocument().Remove("", null) );
			MyAssert.Throws<ArgumentException>( () => new IniDocument().Remove("", "") );

			var ini = new IniDocument();

			// Removing from an new object
			ini.Remove( "section", "foo" );
			Assert.AreEqual( 0, ini.Count );
			ValidateStatus( ini );

			ini.Set( "", "foo", "bar" );
			ini.Set( "section", "hoge", "piyo" );
			ini.Set( "section", "foo", "bar" );

			// Removing non-existing section
			ini.Remove( "no_such_section", "foo" );
			Assert.AreEqual( 2, ini.Count );
			Assert.AreEqual( 1, ini[""].Count );
			Assert.AreEqual( 2, ini["section"].Count );
			ValidateStatus( ini );

			// Removing non-existing property
			ini.Remove( "section", "no_such_property" );
			Assert.AreEqual( 2, ini.Count );
			Assert.AreEqual( 1, ini[""].Count );
			Assert.AreEqual( 2, ini["section"].Count );
			ValidateStatus( ini );

			// Removing a property
			ini.Remove( "section", "foo" );
			Assert.AreEqual( 2, ini.Count );
			Assert.AreEqual( 1, ini[""].Count );
			Assert.AreEqual( 1, ini["section"].Count );
			ValidateStatus( ini );

			// By removing a property, make an empty section 
			ini.Remove( "section", "hoge" );
			Assert.AreEqual( 2, ini.Count );
			Assert.AreEqual( 1, ini[""].Count );
			Assert.AreEqual( 0, ini["section"].Count );
			ValidateStatus( ini );
		}

		[Test]
        public void Clear()
		{
			var ini = new IniDocument();
			MyAssert.DoesNotThrow( () => {
				ini.Clear();
			} );
			Assert.AreEqual( 0, ini.Count );

			ini.Set( "", "foo", "bar" );
			Assert.AreEqual( 1, ini.Count );
			ValidateStatus( ini );

			ini.Clear();
			Assert.AreEqual( 0, ini.Count );
			ValidateStatus( ini );
		}

		[Test]
		public void Sections()
		{
			var ini = new IniDocument();
			ini.Set( "section1", "foo", "bar" );
			ini.Set( "section2", "hoge", "piyo" );

			var iter = ini.Sections.GetEnumerator();
			Assert.AreNotEqual( null, iter );
			Assert.AreEqual( null, iter.Current );
			Assert.AreEqual( true, iter.MoveNext() );
			Assert.AreNotEqual( null, iter.Current );
			Assert.AreEqual( "section1", iter.Current.Name );
			Assert.AreEqual( true, iter.MoveNext() );
			Assert.AreNotEqual( null, iter.Current );
			Assert.AreEqual( "section2", iter.Current.Name );
			Assert.AreEqual( false, iter.MoveNext() );
		}

		[Test]
		public void Item()
		{
			var ini = new IniDocument();

			Assert.AreEqual( null, ini["section"] );

			ini.Set( "section", "hoge", "piyo" );
			Assert.AreEqual( null, ini["no_such_section"] );
			Assert.AreNotEqual( null, ini["section"] );
			Assert.AreEqual( "piyo", ini["section"]["hoge"] );
		}

		[Test]
		public void Load()
		{
			var iniData = @"
key1=value1
;key2=value2
[section]
KEY1 =	VALUE1
KEY2=VALUE2";
			var ini = new IniDocument();
			var reader = new StringReader(iniData);

			MyAssert.Throws<ArgumentNullException>( () => ini.Load(null) );

			Assert.AreEqual( 0, ini.Sections.Count );

			ini.Load( reader );

			Assert.AreEqual( 2, ini.Sections.Count );
			Assert.AreNotEqual( null, ini.Get("") );
			Assert.AreEqual( "", ini.Get("").Name );
			Assert.AreEqual( "key1", ini.Get("").Get("key1").Name );
			Assert.AreEqual( "value1", ini.Get("").Get("key1").Value );
			Assert.AreEqual( null, ini.Get("").Get("key2") );
			Assert.AreEqual( "KEY1", ini.Get("section").Get("KEY1").Name );
			Assert.AreEqual( "\tVALUE1", ini.Get("section").Get("KEY1").Value );

			Assert.AreEqual( "value1", ini[""]["key1"] );
			Assert.AreEqual( "\tVALUE1", ini["section"]["KEY1"] );
			Assert.AreEqual( "VALUE2", ini["section"]["KEY2"] );

			ValidateStatus( ini );
		}

		[Test]
		public void Save()
		{
			var inputData = "key1 = value1\n[section]  \n key1=  value1\nkey2  =value2\n[empty_section]\n";
			var expectedData = "key1= value1\n[section]\nkey1=  value1\nkey2=value2\n";
			var input = new StringReader(inputData);
			var output = new StringWriter(){ NewLine = "\n" };
			var ini = new IniDocument();
			ini.Load( input );
			MyAssert.Throws<ArgumentNullException>( () => ini.Save(null) );

			ini.Save( output );
			Assert.AreEqual( expectedData, output.GetStringBuilder().ToString() );
		}
		#endregion

		#region Other perspective
		[Test]
		public void ComparisonType()
		{
			var ini1 = new IniDocument( StringComparison.Ordinal, StringComparison.OrdinalIgnoreCase );
			var ini2 = new IniDocument( StringComparison.OrdinalIgnoreCase, StringComparison.Ordinal );
			ini1.Set( "section", "foo", "bar" );
			ini2.Set( "section", "foo", "bar" );

			// Read - Section
			Assert.AreNotEqual( null, ini1["section"] );
			Assert.AreNotEqual( null, ini2["section"] );
			Assert.AreEqual(    null, ini1["SECTION"] );
			Assert.AreNotEqual( null, ini2["SECTION"] );

			// Read - Property
			Assert.AreNotEqual( null, ini1["section"]["foo"] );
			Assert.AreNotEqual( null, ini2["section"]["foo"] );
			Assert.AreNotEqual( null, ini1["section"]["FOO"] );
			Assert.AreEqual(    null, ini2["section"]["FOO"] );

			ValidateStatus( ini1 ); ValidateStatus( ini2 );

			// Write
			ini1.Set( "SECTION", "FOO", "BAR" );
			ini2.Set( "SECTION", "FOO", "BAR" );
			Assert.AreEqual( 2, ini1.Count );
			Assert.AreEqual( 1, ini1["section"].Count );
			Assert.AreEqual( 1, ini1["SECTION"].Count );
			Assert.AreEqual( 1, ini2.Count );
			Assert.AreEqual( 2, ini2["section"].Count );
			Assert.AreEqual( 2, ini2["SECTION"].Count );
			Assert.AreEqual( "bar", ini1["section"]["foo"] );
			Assert.AreEqual( "BAR", ini1["SECTION"]["foo"] );
			Assert.AreEqual( "bar", ini1["section"]["FOO"] );
			Assert.AreEqual( "BAR", ini1["SECTION"]["FOO"] );
			Assert.AreEqual( "bar", ini2["section"]["foo"] );
			Assert.AreEqual( "bar", ini2["SECTION"]["foo"] );
			Assert.AreEqual( "BAR", ini2["section"]["FOO"] );
			Assert.AreEqual( "BAR", ini2["SECTION"]["FOO"] );

			ValidateStatus( ini1 ); ValidateStatus( ini2 );
		}

		[Test]
		public void FormatVariation()
		{
			var ini = new IniDocument();

			// Supports INI data without a section
			ini.Load( new StringReader("foo=bar") );
			Assert.AreEqual( "foo=bar\n", ToString(ini) );

			// Whitespaces around the section title token are ignored
			ini.Clear();
			ini.Load( new StringReader("\t [section]\t \nfoo=bar") );
			Assert.AreEqual( "[section]\nfoo=bar\n", ToString(ini) );

			// Whitespaces around the property name are ignored
			ini.Clear();
			ini.Load( new StringReader("[section]\n \tfoo \t=bar") );
			Assert.AreEqual( "[section]\nfoo=bar\n", ToString(ini) );

			// Whitespaces around the property value are NOT ignored
			ini.Clear();
			ini.Load( new StringReader("[section]\nfoo= \tbar \t") );
			Assert.AreEqual( "[section]\nfoo= \tbar \t\n", ToString(ini) );

			// (nit-pick) section name will be extracted from one character after the first '[' and
			// to one character before to the last ']'.
			ini.Clear();
			ini.Load( new StringReader(" \t[a [b]\tc] \t\nfoo=bar") );
			Assert.AreEqual( "[a [b]\tc]\nfoo=bar\n", ToString(ini) );

			// Latter definition wins if there are duplicate properties (not by design though...)
			ini.Clear();
			ini.Load( new StringReader("foo=bar\nfoo=blah") );
			Assert.AreEqual( "foo=blah\n", ToString(ini) );
		}
		#endregion

		#region Internal methods and utilities
		void ValidateStatus( IniDocument ini )
		{
			Assert.AreEqual( ini.Count, ini.Sections.Count );
			var iter1 = ini.Sections.GetEnumerator();
			var iter2 = ini.GetEnumerator();
			while( iter1.MoveNext() )
			{
				Assert.AreEqual( true, iter2.MoveNext() );
				Assert.AreEqual( iter1.Current, iter2.Current );
				Assert.AreSame( iter1.Current, iter2.Current );
				ValidateStatus( iter1.Current );
				ValidateStatus( iter2.Current );
			}
			Assert.AreEqual( false, iter2.MoveNext() );
		}

		void ValidateStatus( IniSection section )
		{
			Assert.AreEqual( section.Count, section.Properties.Count );
			var iter1 = section.Properties.GetEnumerator();
			var iter2 = section.GetEnumerator();
			while( iter1.MoveNext() )
			{
				Assert.AreEqual( true, iter2.MoveNext() );
				Assert.AreEqual( iter1.Current, iter2.Current );
				Assert.AreSame( iter1.Current, iter2.Current );
			}
			Assert.AreEqual( false, iter2.MoveNext() );
		}

		string ToString( IniDocument ini )
		{
			var writer = new StringWriter();
			writer.NewLine = "\n";
			ini.Save( writer );
			return writer.ToString();
		}
		#endregion
	}
}
