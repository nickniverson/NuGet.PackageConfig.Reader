using System;

namespace NuGet.PackageConfig.Reader.Contracts
{
	public class Package
	{
		public Package(string id, string version, string targetFramework = null, string allowedVersions = null, bool? developmentDependency = null)
		{
			// required properties
			Id = id ?? throw new ArgumentNullException(nameof(id));
			Version = version ?? throw new ArgumentNullException(nameof(version));

			// optional properties
			TargetFramework = targetFramework;
			AllowedVersions = allowedVersions;
			DevelopmentDependency = developmentDependency;
		}

		public string Id { get; }

		public string Version { get; }

		public string TargetFramework { get; }

		public string AllowedVersions { get; }

		public bool? DevelopmentDependency { get; }
	}
}