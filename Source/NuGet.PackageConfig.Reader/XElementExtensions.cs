using System;
using System.Xml.Linq;

namespace NuGet.PackageConfig.Reader
{
	internal static class XElementExtensions
	{
		/// <remarks>
		/// Throws an exception if an invalid boolean value was provided.
		/// </remarks>
		/// <returns>
		/// Returns null if the attribute doesn't exist.
		/// </returns>
		internal static bool? GetBoolAttributeNullOrThrow(this XElement element, string attributeName)
		{
			var value = element.Attribute(attributeName)?.Value;
			if (value == null)
				return null;

			if (bool.TryParse(value, out var result))
				return result;

			throw new InvalidBooleanAttributeException($"package.config attribute '{attributeName}' must " +
				$"be a valid boolean value but was '{value}'");
		}
	}
}
