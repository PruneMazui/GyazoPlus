// See LICENSE.md for license terms of usage.
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAttribute = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;

namespace Sgry.Ini.Tests
{
    [TestClass]
    public class SectionTest
    {
        [Test]
        public void ctor()
        {
			var ctype = StringComparison.Ordinal;

            MyAssert.Throws<ArgumentNullException>( () => new IniSection(null, ctype) );
            MyAssert.DoesNotThrow( () => new IniSection("", ctype) );

			var section = new IniSection( "foo", ctype );
            Assert.AreEqual( "foo", section.Name );
            Assert.AreEqual( 0, section.Count );
		}

        [Test]
        public void Name()
        {
			var ctype = StringComparison.Ordinal;

            MyAssert.Throws<ArgumentNullException>( () => {
				var tmp = new IniSection( null, ctype );
			} );
            MyAssert.DoesNotThrow( () => {
				var tmp = new IniSection( "", ctype );
			} );

            MyAssert.Throws<ArgumentNullException>( () => {
				new IniSection("foo", ctype).Name = null;
			} );
            MyAssert.Throws<ArgumentException>( () => {
				new IniSection("foo", ctype).Name = "";
			} );
			var section = new IniSection( "foo", ctype );
            Assert.AreEqual( "foo", section.Name );
            section.Name = "FOO";
            Assert.AreEqual( "FOO", section.Name );
		}

		[Test]
        public void Get()
		{
			var section = new IniSection( "section", StringComparison.Ordinal );
			IniProperty prop;

			MyAssert.Throws<ArgumentNullException>( () => {
				section.Get( null );
			} );
			MyAssert.Throws<ArgumentException>( () => {
				section.Get( "" );
			} );

			prop = section.Get( "foo" );
			Assert.AreEqual( null, prop );

			section.Set( "foo", "str" );
			Assert.AreNotEqual( null, section.Get("foo") );
			Assert.AreEqual( "foo", section.Get("foo").Name );
			Assert.AreEqual( "str", section.Get("foo").Value );
		}

		[Test]
        public void Get_T()
		{
			var section = new IniSection( "section", StringComparison.Ordinal );

			MyAssert.Throws<ArgumentNullException>( () => {
				section.Get( null, "" );
			} );
			MyAssert.Throws<ArgumentException>( () => {
				section.Get( "", "" );
			} );

			Assert.AreEqual( null, section.Get<string>("foo", null) );

			section.Set( "foo", "str" );
			Assert.AreNotEqual( null, section.Get("foo", "") );
			Assert.AreEqual( "str", section.Get("foo", "") );
		}

		[Test]
		public void GetInt()
		{
			var section = new IniSection( "section", StringComparison.Ordinal );

			MyAssert.Throws<ArgumentException>( () => {
				section.GetInt("foo", 2, 1, -1);
			} );

			section.Set( "foo", 2 );
			Assert.AreEqual( -1, section.GetInt("foo", 0, 1, -1) );
			Assert.AreEqual(  2, section.GetInt("foo", 0, 2, -1) );
			Assert.AreEqual(  2, section.GetInt("foo", 2, 4, -1) );
			Assert.AreEqual( -1, section.GetInt("foo", 3, 4, -1) );

			section.Set( "foo", "bar" );
			Assert.AreEqual( -1, section.GetInt("foo", Int32.MinValue, Int32.MaxValue, -1) );
		}

		[Test]
        public void TryGet()
		{
			var section = new IniSection( "section", StringComparison.Ordinal );
			int num; // value stype
			string str; // referenced type
			MyEnum enumVal;

			MyAssert.Throws<ArgumentNullException>( () => {
				section.TryGet( null, out num );
			} );
			MyAssert.Throws<ArgumentException>( () => {
				section.TryGet( "", out num );
			} );

			Assert.AreEqual( false, section.TryGet("foo", out str) );

			section.Set( "foo", "str" );
			Assert.AreEqual( true, section.TryGet("foo", out str) );
			Assert.AreEqual( false, section.TryGet("foo", out num) );
			Assert.AreEqual( false, section.TryGet("foo", out enumVal) );
			Assert.AreEqual( "str", str );

			section.Set( "foo", 1234 );
			Assert.AreEqual( true, section.TryGet("foo", out str) );
			Assert.AreEqual( true, section.TryGet("foo", out num) );
			Assert.AreEqual( true, section.TryGet("foo", out enumVal) ); // enum can be an int
			Assert.AreEqual( "1234", str );
			Assert.AreEqual( 1234, num );
			Assert.AreEqual( (MyEnum)1234, enumVal );

			section.Set( "foo", "3.14" );
			Assert.AreEqual( true, section.TryGet("foo", out str) );
			Assert.AreEqual( false, section.TryGet("foo", out num) );
			Assert.AreEqual( false, section.TryGet("foo", out enumVal) );
			Assert.AreEqual( "3.14", str );
		}

		[Test]
		public void TryGetInt()
		{
			var section = new IniSection( "section", StringComparison.Ordinal );
			int num;

			MyAssert.Throws<ArgumentException>( () => {
				section.TryGetInt( "foo", 2, 1, out num );
			} );

			section.Set( "foo", 2 );

			Assert.AreEqual( false, section.TryGetInt("fooooo", 0, 4, out num) );

			num = 0;
			Assert.AreEqual( true, section.TryGetInt("foo", 1, 2, out num) );
			Assert.AreEqual( 2, num );

			num = 0;
			Assert.AreEqual( true, section.TryGetInt("foo", 2, 3, out num) );
			Assert.AreEqual( 2, num );

			num = 0;
			Assert.AreEqual( false, section.TryGetInt("foo", 0, 1, out num) );
			Assert.AreEqual( 2, num );

			num = 0;
			Assert.AreEqual( false, section.TryGetInt("foo", 3, 4, out num) );
			Assert.AreEqual( 2, num );
		}

		[Test]
        public void Set()
		{
			var section = new IniSection( "section", StringComparison.Ordinal );

			MyAssert.Throws<ArgumentNullException>( () => {
				section.Set( null, "" );
			} );
			MyAssert.Throws<ArgumentException>( () => {
				section.Set( "", "" );
			} );
			MyAssert.Throws<ArgumentNullException>( () => {
				section.Set<string>( "foo", null );
			} );
			MyAssert.DoesNotThrow( () => {
				section.Set( "foo", "" );
			} );

			Assert.AreEqual( "", section.Get("foo").Value );
			Assert.AreEqual( 1, section.Properties.Count );

			section.Set( "foo", "bar" );
			Assert.AreEqual( "bar", section.Get("foo").Value );
			Assert.AreEqual( 1, section.Properties.Count );

			// Insert an item to the last position
			section.Set( "hoge", "piyo" );
			Assert.AreEqual( "bar", section.Get("foo").Value );
			Assert.AreEqual( "piyo", section.Get("hoge").Value );
			Assert.AreEqual( 2, section.Properties.Count );

			// Insert an item to the first position
			section.Set( "aaa", "bbb" );
			Assert.AreEqual( "bbb", section.Get("aaa").Value );
			Assert.AreEqual( "bar", section.Get("foo").Value );
			Assert.AreEqual( "piyo", section.Get("hoge").Value );
			Assert.AreEqual( 3, section.Properties.Count );

			// Insert an item into the middle of existing items
			section.Set( "ccc", "ddd" );
			Assert.AreEqual( "bbb", section.Get("aaa").Value );
			Assert.AreEqual( "ddd", section.Get("ccc").Value );
			Assert.AreEqual( "bar", section.Get("foo").Value );
			Assert.AreEqual( "piyo", section.Get("hoge").Value );
			Assert.AreEqual( 4, section.Properties.Count );
		}

		[Test]
        public void Remove()
		{
			var section = new IniSection( "section", StringComparison.Ordinal );

			MyAssert.Throws<ArgumentNullException>( () => {
				section.Remove( null );
			} );
			MyAssert.Throws<ArgumentException>( () => {
				section.Remove( "" );
			} );

			section.Set( "foo", "bar" );
			section.Set( "hoge", "piyo" );

			// Removing non-existing item
			Assert.AreEqual( false, section.Remove("no_such_property") );
			Assert.AreEqual( "bar", section.Get("foo").Value );
			Assert.AreEqual( "piyo", section.Get("hoge").Value );
			Assert.AreEqual( 2, section.Properties.Count );

			// Removing an existing item
			Assert.AreEqual( true, section.Remove("foo") );
			Assert.AreEqual( "piyo", section.Get("hoge").Value );
			Assert.AreEqual( 1, section.Properties.Count );

			// Removing the last item
			Assert.AreEqual( true, section.Remove("hoge") );
			Assert.AreEqual( 0, section.Properties.Count );

			// Removing non-existing item
			Assert.AreEqual( false, section.Remove("no_such_property2") );
			Assert.AreEqual( 0, section.Properties.Count );
		}

		[Test]
        public void Clear()
		{
			var section = new IniSection( "section", StringComparison.Ordinal );
			MyAssert.DoesNotThrow( () => {
				section.Clear();
			} );

			section.Set( "foo", "bar" );
			Assert.AreEqual( 1, section.Properties.Count );
			section.Clear();
			Assert.AreEqual( 0, section.Properties.Count );
		}

		[Test]
        public void Properties()
		{
			var section = new IniSection( "section", StringComparison.Ordinal );
			Assert.AreEqual( 0, section.Properties.Count );
			section.Set( "foo", "bar" );
			Assert.AreEqual( 1, section.Properties.Count );
			section.Set( "hoge", "piyo" );
			Assert.AreEqual( 2, section.Properties.Count );

			Assert.AreEqual( null, section.Properties["no_such_property"] );
			Assert.AreNotEqual( null, section.Properties["foo"] );
			Assert.AreEqual( "foo", section.Properties["foo"].Name );
			Assert.AreEqual( "bar", section.Properties["foo"].Value );

			var iter = section.Properties.GetEnumerator();
			Assert.AreNotEqual( null, iter );
			Assert.AreEqual( null, iter.Current );
			Assert.AreEqual( true, iter.MoveNext() );
			Assert.AreNotEqual( null, iter.Current );
			Assert.AreEqual( "foo", iter.Current.Name );
			Assert.AreEqual( "bar", iter.Current.Value );
			Assert.AreEqual( true, iter.MoveNext() );
			Assert.AreNotEqual( null, iter.Current );
			Assert.AreEqual( "hoge", iter.Current.Name );
			Assert.AreEqual( "piyo", iter.Current.Value );
			Assert.AreEqual( false, iter.MoveNext() );
		}

		[Test]
        public void GetEnumerator()
		{
			var section = new IniSection( "section", StringComparison.Ordinal );
			section.Set( "foo", "bar" );
			section.Set( "hoge", "piyo" );

			var iter = section.GetEnumerator();
			Assert.AreNotEqual( null, iter );
			Assert.AreEqual( null, iter.Current );
			Assert.AreEqual( true, iter.MoveNext() );
			Assert.AreNotEqual( null, iter.Current );
			Assert.AreEqual( "foo", iter.Current.Name );
			Assert.AreEqual( "bar", iter.Current.Value );
			Assert.AreEqual( true, iter.MoveNext() );
			Assert.AreNotEqual( null, iter.Current );
			Assert.AreEqual( "hoge", iter.Current.Name );
			Assert.AreEqual( "piyo", iter.Current.Value );
			Assert.AreEqual( false, iter.MoveNext() );
		}

		[Test]
        public void Item()
		{
			var section = new IniSection( "section", StringComparison.Ordinal );
			section.Set( "foo", "bar" );
			section.Set( "hoge", "piyo" );

			Assert.AreEqual( null, section["no_such_property"] );
			Assert.AreNotEqual( null, section["foo"] );
			Assert.AreEqual( "bar", section["foo"] );
			Assert.AreNotEqual( null, section["hoge"] );
			Assert.AreEqual( "piyo", section["hoge"] );
		}

		[Test]
		public void ComparisonType()
		{
			var sec1 = new IniSection( "", StringComparison.Ordinal );
			var sec2 = new IniSection( "", StringComparison.OrdinalIgnoreCase );
			sec1["foo"] = "bar";
			sec2["foo"] = "bar";
			Assert.AreEqual( 1, sec1.Count );
			Assert.AreEqual( 1, sec2.Count );

			// Read
			Assert.AreEqual( "bar", sec1["foo"] );
			Assert.AreEqual( "bar", sec2["foo"] );
			Assert.AreEqual( null, sec1["FOO"] );
			Assert.AreEqual( "bar", sec2["FOO"] );

			// Write
			sec1["FOO"] = "BAR";
			sec2["FOO"] = "BAR";
			Assert.AreEqual( "bar", sec1["foo"] );
			Assert.AreEqual( "BAR", sec2["foo"] );
			Assert.AreEqual( "BAR", sec1["FOO"] );
			Assert.AreEqual( "BAR", sec2["FOO"] );
			Assert.AreEqual( 2, sec1.Count );
			Assert.AreEqual( 1, sec2.Count );
		}
	}
}
