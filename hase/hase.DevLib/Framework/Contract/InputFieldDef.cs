using System.Collections.Generic;

namespace hase.DevLib.Framework.Contract
{
    public class InputFieldDef
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string ClrType { get; set; }
        public string DefaultValue { get; set; }
        public IList<string> Choices { get; set; }
    }
}
