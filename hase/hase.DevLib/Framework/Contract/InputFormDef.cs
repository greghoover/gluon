using System.Collections.Generic;

namespace hase.DevLib.Framework.Contract
{
    public class InputFormDef
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string NavigationTitle { get; set; }
        public string ContentTitle { get; set; }
        public List<InputFieldDef> InputFields { get; set; }
    }
}
