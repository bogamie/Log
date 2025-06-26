using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

public static class LogAnalyzer
{
    public static string Analyze(string logPath, string rulesPath)
    {
        JObject rules = JObject.Parse(File.ReadAllText(rulesPath));
        var targetKeys = new HashSet<string>(rules.Properties().Select(p => p.Name));
        var logValues = ParseLogFileParallel(logPath, targetKeys);
        return CompareWithRulesParallel(rules, logValues);
    }

    static Dictionary<string, string> ParseLogFileParallel(string path, HashSet<string> targetKeys)
    {
        var values = new ConcurrentDictionary<string, string>();
        var lines = File.ReadAllLines(path);
        var regex = new Regex(@"(?<key>\w+)\s*=\s*(?<val>-?\d+)", RegexOptions.IgnoreCase);

        Parallel.ForEach(lines, line =>
        {
            foreach (Match match in regex.Matches(line))
            {
                string key = match.Groups["key"].Value;
                if (targetKeys.Contains(key))
                    values[key] = match.Groups["val"].Value;
            }

            if (line.Contains("JsonObject") || line.Contains("makeVehicleStatus"))
            {
                string jsonStr = ExtractJsonPart(line);
                try
                {
                    var jObj = JObject.Parse(jsonStr);
                    ExtractJsonValues(jObj, values, targetKeys);
                }
                catch { /* 무시 */ }
            }
        });

        return new Dictionary<string, string>(values);
    }

    static string ExtractJsonPart(string raw)
    {
        int start = raw.IndexOf('{');
        int end = raw.LastIndexOf('}');
        if (start >= 0 && end > start)
            return raw.Substring(start, end - start + 1);
        return "{}";
    }

    static void ExtractJsonValues(JToken token, ConcurrentDictionary<string, string> dict, HashSet<string> targetKeys, string prefix = "")
    {
        if (token is JObject obj)
        {
            foreach (var prop in obj.Properties())
            {
                ExtractJsonValues(prop.Value, dict, targetKeys, $"{prefix}{prop.Name}.");
            }
        }
        else if (token is JArray array)
        {
            for (int i = 0; i < array.Count; i++)
            {
                ExtractJsonValues(array[i], dict, targetKeys, $"{prefix}{i}.");
            }
        }
        else
        {
            string key = prefix.TrimEnd('.');
            if (targetKeys.Contains(key))
                dict[key] = token.ToString();
        }
    }

    private static string CompareWithRulesParallel(JObject rules, Dictionary<string, string> logData)
    {
        var results = new ConcurrentBag<string>();

        Parallel.ForEach(rules.Properties(), rule =>
        {
            string key = rule.Name;
            var ruleObj = (JObject)rule.Value;

            string description = ruleObj["description"]?.ToString();
            string expected = ruleObj["expected"]?.ToString();
            var map = ruleObj["map"]?.ToObject<Dictionary<string, string>>();

            if (!logData.TryGetValue(key, out string actualRaw))
            {
                results.Add($"❗ 로그에 키 '{key}' 없음");
                return;
            }

            string actualHuman = map != null && map.TryGetValue(actualRaw, out var mapped)
                ? mapped
                : actualRaw;

            if (actualHuman == expected)
                results.Add($"✅ {description} ({key}): {actualHuman} (일치)");
            else
                results.Add($"❌ {description} ({key}): 기대값 '{expected}', 실제값 '{actualHuman}'");
        });

        return string.Join(Environment.NewLine, results);
    }
}
