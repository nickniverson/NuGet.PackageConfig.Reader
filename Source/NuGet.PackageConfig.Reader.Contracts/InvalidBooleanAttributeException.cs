using System;
using System.Runtime.Serialization;

namespace NuGet.PackageConfig.Reader
{
	[Serializable]
	public class InvalidBooleanAttributeException : Exception
	{
		public InvalidBooleanAttributeException()
		{
		}

		public InvalidBooleanAttributeException(string message) : base(message)
		{
		}

		public InvalidBooleanAttributeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidBooleanAttributeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}