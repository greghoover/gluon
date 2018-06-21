using hase.AppServices.Calculator.Client;
using hase.AppServices.FileSystemQuery.Client;
using hase.AppServices.FileSystemReader.Client;
using hase.DevLib.Framework.Contract;
using System.Collections.Generic;

namespace hase.ClientUI.XFApp
{
	public class ServiceDefinitions
	{
		// todo: 06/19/18 gph. Serialize and publish form defs to a repository as part of service development. Not here.
		public static IEnumerable<InputFormDef> GetAll()
		{
			var calcClient = new Calculator();
			yield return calcClient.GenerateUntypedClientDef();

			var fsqClient = new FileSystemQuery();
			yield return fsqClient.GenerateUntypedClientDef();

			var fsrClient = new FileSystemReader();
			yield return fsrClient.GenerateUntypedClientDef();
		}
		// todo: 06/19/18 gph. Load form defs from a repository.
		public static IEnumerable<InputFormDef> GetAll2()
		{
			return new List<InputFormDef>
			{
				new InputFormDef {
					Name = "Calculator",
					NavigationTitle = "Calculator",
					ContentTitle = "Calculator Page",
					Description = "Add Two Numbers",
					RequestClrType = "hase.AppServices.Calculator.Contract.CalculatorRequest, hase.AppServices.Calculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
					ResponseClrType = "hase.AppServices.Calculator.Contract.CalculatorResponse, hase.AppServices.Calculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
					ServiceClrType = "hase.AppServices.Calculator.Service.CalculatorService, hase.AppServices.Calculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",

					InputFields = new List<InputFieldDef> {
						new InputFieldDef
						{
							Caption = "Operation:",
							Choices = new List<string> {"Add", "Subtract"},
							ClrType = "hase.AppServices.Calculator.Contract.CalculatorOpEnum, hase.AppServices.Calculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
							Name = "Operation",
							DefaultValue = "Add",
						},
						new InputFieldDef {
							Caption = "Number1:",
							ClrType = "System.Int32, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e",
							Name = "Number1",
							DefaultValue = "0"
						},
						new InputFieldDef {
							Caption = "Number2:",
							ClrType = "System.Int32, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e",
							Name = "Number2",
							DefaultValue = "0"
						},
					}
				},

				new InputFormDef {
					Name = "FileSystemQuery",
					NavigationTitle = "FileSystemQuery",
					ContentTitle = "File System Query Page",
					Description = "Check File Path",
					RequestClrType = "hase.AppServices.FileSystemQuery.Contract.FileSystemQueryRequest, hase.AppServices.FileSystemQuery, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
					ResponseClrType = "hase.AppServices.FileSystemQuery.Contract.FileSystemQueryResponse, hase.AppServices.FileSystemQuery, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
					ServiceClrType = "hase.AppServices.FileSystemQuery.Service.FileSystemQueryService, hase.AppServices.FileSystemQuery, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",

					InputFields = new List<InputFieldDef> {
						new InputFieldDef {
							Caption = "QueryType:",
							Choices = new List<string> { "DirectoryExists" },
							ClrType = "hase.AppServices.FileSystemQuery.Contract.FileSystemQueryTypeEnum, hase.AppServices.FileSystemQuery, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
							Name = "QueryType",
							DefaultValue = "DirectoryExists",
						},
						new InputFieldDef {
							Caption = "FolderPath:",
							ClrType = "System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e",
							Name = "FolderPath",
							DefaultValue = @"c:\"
						}
					}
				}
			};
		}
	}
}