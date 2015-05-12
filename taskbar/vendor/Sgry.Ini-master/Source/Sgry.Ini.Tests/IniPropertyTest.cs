// See LICENSE.md for license terms of usage.
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAttribute = Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;

namespace Sgry.Ini.Tests
{
    [TestClass]
    public class PropertyTest
	{
		#region Public methods and properties
		[Test]
        public void ctor()
        {
            MyAssert.Throws<ArgumentNullException>( () => new IniProperty(null, "bar") );
            MyAssert.Throws<ArgumentException>( () => new IniProperty("", "bar") );
            MyAssert.DoesNotThrow( () => new IniProperty("foo", "") );
            MyAssert.Throws<ArgumentNullException>( () => new IniProperty("foo", null) );

			var prop = new IniProperty( "foo", "bar" );
			Assert.AreEqual( "foo", prop.Name );
			Assert.AreEqual( "bar", prop.Value );
        }

		[Test]
        public void Name()
        {
            MyAssert.Throws<ArgumentNullException>( () => {
				var tmp = new IniProperty( "foo", "bar" );
				tmp.Name = null;
			} );
            MyAssert.Throws<ArgumentException>( () => {
				var tmp = new IniProperty( "foo", "bar" );
				tmp.Name = "";
			} );

			var prop = new IniProperty( "foo", "bar" );
			Assert.AreEqual( "foo", prop.Name );
            prop.Name = "FOO";
            Assert.AreEqual( "FOO", prop.Name );
        }

        [Test]
        public void Value()
        {
            MyAssert.Throws<ArgumentNullException>( () => {
				var tmp = new IniProperty( "foo", "bar" );
				tmp.Value = null;
			} );
            MyAssert.DoesNotThrow( () => {
				var tmp = new IniProperty( "foo", "bar" );
				tmp.Value = "";
			} );

			var prop = new IniProperty( "foo", "bar" );
            Assert.AreEqual( "bar", prop.Value );
            prop.Value = "BAR";
            Assert.AreEqual( "BAR", prop.Value );
        }

        [Test]
        public void IntValue()
        {
			MyAssert.Throws<FormatException>( () => {
				var tmp = new IniProperty("foo", "bar").IntValue;
			} );
			MyAssert.Throws<OverflowException>( () => {
				var tmp = new IniProperty("foo", "2147483648").IntValue;
			} );
			MyAssert.Throws<OverflowException>( () => {
				var tmp = new IniProperty("foo", "-2147483649").IntValue;
			} );

            Assert.AreEqual( Int32.MaxValue, new IniProperty("foo", "2147483647").IntValue );
            Assert.AreEqual( Int32.MinValue, new IniProperty("foo", "-2147483648").IntValue );
		}
		#endregion
	}
}
