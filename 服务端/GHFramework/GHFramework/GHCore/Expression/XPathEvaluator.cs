using System.IO;

namespace GHCore.Expression
{
    /// <summary>
    /// XPath，动态计算数学表达式值
    /// </summary>
    public class XPathEvaluator
    {
        public static object Eval(string expression)
        {
            return new System.Xml.XPath.XPathDocument(new StringReader("<r/>"))
                             .CreateNavigator()
                             .Evaluate(string.Format("number({0})",
                             new System.Text.RegularExpressions.Regex(@"([\+\-\*])")
                             .Replace(expression, " ${1} ")
                             .Replace("/", " div ")
                             .Replace("%", " mod ")));
        }
    }
}
