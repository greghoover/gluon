using hase.DevLib.Framework.Contract;
using hase.DevLib.Services.Calculator.Client;
using hase.DevLib.Services.FileSystemQuery.Client;
using System.Collections.Generic;

namespace hase.ClientUI.XFApp
{
	public class ServiceDefinitions
	{
		public static IEnumerable<InputFormDef> GetAll()
		{
			var calcClient = new Calculator();
			yield return calcClient.GenerateUntypedClientDef();

			var fsqClient = new FileSystemQuery();
			yield return fsqClient.GenerateUntypedClientDef();
		}
		public static IEnumerable<InputFormDef> GetAll2()
		{
			return new List<InputFormDef>
			{
				new InputFormDef {
					Name = "Calculator",
					NavigationTitle = "Calculator",
					ContentTitle = "Calculator Page",
					Description = "Add Two Numbers",

					InputFields = new List<InputFieldDef> {
						new InputFieldDef {
							Caption = "FirstNumber:",
							Name = "FirstNumber",
							DefaultValue = "0"
						},
						new InputFieldDef {
							Caption = "SecondNumber:",
							Name = "SecondNumber",
							DefaultValue = "0"
						}
					}
				},

				new InputFormDef {
					Name = "FileSystemQuery",
					NavigationTitle = "FileSystemQuery",
					ContentTitle = "FileSystemQuery Page",
					Description = "Check File Path",

					InputFields = new List<InputFieldDef> {
						new InputFieldDef {
							Caption = "DoesDirectoryExist:",
							Name = "DoesDirectoryExist",
							DefaultValue = @"c:\"
						}
					}
				}
			};
		}
	}
}