using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Test.Tests;


public class MediaTest
{
    private const string MediaApi = "http://localhost:5001/api/media";

    /* /api/categories */

}

static class HelperExt
{
    public static string? Value(this JsonNode node, string name)
    {
        var value = node[name];
        return value?.ToString();
    }

    public static string? FirstElement(this JsonArray node, string name)
    {
        return node.First()?.Value(name);
    }

    public static string? LastElement(this JsonArray node, string name)
    {
        return node.Last()?.Value(name);
    }
}