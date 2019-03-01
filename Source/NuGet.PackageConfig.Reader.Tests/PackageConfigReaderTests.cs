using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.PackageConfig.Reader.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace NuGet.PackageConfig.Reader.Tests
{

	[TestClass]
	public class PackageConfigReaderTests
	{
		public IPackageConfigReader Target { get; set; }

		public PackageConfigReaderBuilder Builder { get; set; }



		[TestInitialize]
		public virtual void TestInitialize()
		{
			Builder = new PackageConfigReaderBuilder();

			Target = Builder.Build();
		}
	}



	[TestClass]
	public class Constructor : PackageConfigReaderTests
	{
		[TestMethod]
		public void Should_Create_An_Instance()
		{
			Assert.IsNotNull(Target);
		}
	}



	[TestClass]
	public class ReadMethod : PackageConfigReaderTests
	{
		public IEnumerable<Package> Packages { get; set; }

		public Package Result { get; set; }



		[TestClass]
		public class WhenGivenAPackageFilePathThatDoesNotExist : ReadMethod
		{
			[ExpectedException(typeof(FileNotFoundException))]
			[TestMethod]
			public void Should_Throw_A_File_Not_Found_Exception()
			{
				Target.Read(Builder.GetPackagesConfigFilePathThatDoesNotExist());
			}
		}



		[TestClass]
		public class WhenGivenAValidPackageConfigFile : ReadMethod
		{
			[TestInitialize]
			public override void TestInitialize()
			{
				base.TestInitialize();

				Packages = Target.Read(Builder.GetPackagesConfigFilePath());

				Result = Packages.First();
			}


			[TestMethod]
			public void Should_Read_All_Packages()
			{
				Assert.AreEqual(
					expected: 2,
					actual: Packages.Count());
			}


			[TestMethod]
			public void Should_Populate_The_Package_Id()
			{
				Assert.AreEqual(
					expected: "Newtonsoft.Json",
					actual: Result.Id);
			}


			[TestMethod]
			public void Should_Populate_The_Package_Version()
			{
				Assert.AreEqual(
					expected: "12.0.1",
					actual: Result.Version);
			}


			[TestMethod]
			public void Should_Populate_The_Package_Target_Framework()
			{
				Assert.AreEqual(
					expected: "net461",
					actual: Result.TargetFramework);
			}


			[TestMethod]
			public void Should_Populate_The_Package_Allowed_Versions()
			{
				Assert.AreEqual(
					expected: "[8,13)",
					actual: Result.AllowedVersions);
			}


			[TestMethod]
			public void Should_Populate_The_Package_Development_Dependency()
			{
				Assert.IsFalse(
					Result.DevelopmentDependency.HasValue && Result.DevelopmentDependency.Value,
					$"expected the Package.DevelopmentDependency to be 'false' but was " +
					$"'{(Result.DevelopmentDependency.HasValue ? Result.DevelopmentDependency.ToString() : "null")}'");
			}
		}



		[TestClass]
		public class WhenReadingAPackageWithADevelopmentDependencyThatIsNotAValidBoolean : ReadMethod
		{
			[ExpectedException(typeof(InvalidBooleanAttributeException))]
			[TestMethod]
			public void Should_Throw_An_Invalid_Boolean_Attribute_Exception()
			{
				Packages = Target.Read(Builder.GetPackagesConfigWithInvalidAttributeValuesFilePath());

				// force the IEnumerable to iterate... 
				Packages.ToList();
			}
		}
	}



	public class PackageConfigReaderBuilder
	{
		public IPackageConfigReader Build()
		{
			return new PackageConfigReader();
		}


		public string GetPackagesConfigFilePath()
		{
			return Path.Combine(
				Environment.CurrentDirectory,
				@"..\..\PackageConfigFiles\packages.config");
		}

		internal string GetPackagesConfigFilePathThatDoesNotExist()
		{
			return Path.Combine(
				Environment.CurrentDirectory,
				@"..\..\PackageConfigFiles\lkjasdflkjasdflkjasdf.config");
		}

		internal string GetPackagesConfigWithInvalidAttributeValuesFilePath()
		{
			return Path.Combine(
				Environment.CurrentDirectory,
				@"..\..\PackageConfigFiles\packages-with-invalid-development-dependency-attribute.config");
		}
	}
}
