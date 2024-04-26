using System.ComponentModel;

namespace Intellivoid.Netlenium
{
    /// <summary>
    /// By attribute for searching elements
    /// </summary>
    public enum By
    {
        [Description("class_name")]
        ClassName = 0,

        [Description("css_selector")]
        CssSelector = 1,

        [Description("id")]
        Id = 2,

        [Description("name")]
        Name = 3,

        [Description("tag_name")]
        TagName = 4,

        [Description("xpath")]
        XPath = 5
    }
}
