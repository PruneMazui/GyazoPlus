// See LICENSE.md for license terms of usage.
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sgry.Ini.Tests
{
	public delegate void MyTestDelegate();

	static class MyAssert
	{
		public static void Throws<T>( MyTestDelegate d ) where T : Exception
		{
			try{ d(); }
			catch(Exception e){ Assert.IsInstanceOfType(e, typeof(T)); }
		}

		public static void DoesNotThrow( MyTestDelegate d )
		{
			try{ d(); }
			catch( Exception e ){ Assert.Fail(e.ToString()); }
		}
	}
}
