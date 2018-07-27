using hase.AppServices.Calculator.Service;
using hase.AppServices.FileSystemQuery.Service;
using hase.DevLib.Framework.Repository.Contract;
using hase.DevLib.Framework.Repository.Service;
using System;
using System.IO;
using Xunit;

namespace hase.DevLib.Tests
{
	public class PublishServices
	{
		[Fact]
		public void PublishCalculatorService()
		{
			try
			{
				var serviceName = nameof(CalculatorService);
				var path = @"C:\Users\greg\Source\Repos\gluon\hase\hase.AppServices.Calculator\bin\Debug\netstandard2.0";
				//var filePath = @"C:\Users\greg\Source\Repos\gluon\hase\hase.DevLib.Core20\bin\Debug\netcoreapp2.1\hase.DevLib.Core20.dll";

				var folderSpec = ServiceRepo.GetFolder(serviceName, new DirectoryInfo(path));
				ServiceRepo.SaveFolder(folderSpec);

				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}
		[Fact]
		public void PublishFileSystemQueryService()
		{
			try
			{
				var serviceName = nameof(FileSystemQueryService);
				var path = @"C:\Users\greg\Source\Repos\gluon\hase\hase.AppServices.FileSystemQuery\bin\Debug\netstandard2.0";
				//var filePath = @"C:\Users\greg\Source\Repos\gluon\hase\hase.DevLib.Core20\bin\Debug\netcoreapp2.1\hase.DevLib.Core20.dll";

				var folderSpec = ServiceRepo.GetFolder(serviceName, new DirectoryInfo(path));
				ServiceRepo.SaveFolder(folderSpec);

				Xunit.Assert.True(true);
			}
			catch (Exception ex)
			{
				Xunit.Assert.True(false);
			}
		}

		//[Fact]
		//public async void GetFile()
		//{
		//	try
		//	{
		//		var fileName = @"fred.txt";
		//		var response = await Util.GetDoc(@"http://172.27.211.17:5000", fileName);

		//		Xunit.Assert.True(true);
		//	}
		//	catch (Exception ex)
		//	{
		//		Xunit.Assert.True(false);
		//	}
		//}
		//[Fact]
		//public async void PublishFile()
		//{
		//	try
		//	{
		//		var filePath = @"C:\Users\greg\Source\Repos\gluon\hase\hase.DevLib.Core20\bin\Debug\netcoreapp2.1\hase.DevLib.Core20.dll";
		//		var model = new CreateDocumentModel()
		//		{
		//			Document = new byte[] { 0x03, 0x10, 0xFF, 0xFF },
		//			Name = "Test",
		//			CreationDate = new DateTime(2017, 12, 27)
		//		}; var response = await Util.PostDoc(@"http://172.27.211.17:5000", model);

		//		Xunit.Assert.True(response.IsSuccessStatusCode);
		//	}
		//	catch (Exception ex)
		//	{
		//		Xunit.Assert.True(false);
		//	}
		//}
	}
}
