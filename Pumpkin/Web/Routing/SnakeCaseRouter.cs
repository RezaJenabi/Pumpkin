using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;

namespace Pumpkin.Web.Routing
{
    public class SnakeCaseRouter : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            return value == null ? null : Regex.Replace(value.ToString() ?? "", "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}