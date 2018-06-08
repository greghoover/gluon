using System.Collections.Generic;

namespace hase.ClientUI.XFApp
{
    public class ServiceDefinition
    {
        public string NavigationTitle { get; set; }
        public string ContentTitle { get; set; }
        public string InfoHeader { get; set; }
        public List<FieldDefinition> Fields { get; set; }
    }
    public class FieldDefinition
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string DefaultValue { get; set; }
    }

    public class ServiceDefinitions
    {
        public static IEnumerable<ServiceDefinition> GetAll()
        {
            return new List<ServiceDefinition>
            {
                new ServiceDefinition {
                    NavigationTitle = "Calculator",
                    ContentTitle = "Calculator Page",
                    InfoHeader = "Add Two Numbers",

                    Fields = new List<FieldDefinition> {
                        new FieldDefinition {
                            Caption = "FirstNumber:",
                            Name = "FirstNumber",
                            DefaultValue = "0"
                        },
                        new FieldDefinition {
                            Caption = "SecondNumber:",
                            Name = "SecondNumber",
                            DefaultValue = "0"
                        }
                    }
                },

                new ServiceDefinition {
                    NavigationTitle = "FileSystemQuery",
                    ContentTitle = "FileSystemQuery Page",
                    InfoHeader = "Check File Path",

                    Fields = new List<FieldDefinition> {
                        new FieldDefinition {
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