﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roadkill.Core
{
	/// <summary>
	/// The exception that is thrown when a database task does not complete correctly. 
	/// </summary>
	public class DatabaseException : ExceptionBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SecurityException"/> class.
		/// </summary>
		/// <param name="message">The message as an <see cref="IFormattable"/> string.</param>
		/// <param name="args">Arguments for the message format.</param>
		public DatabaseException(string message, params object[] args) : base(message, args) { }
	}
}
