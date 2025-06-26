using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

public static class LogAnalyzer
{
    public static string Analyze(string logPath, string rulesPath)
    {
        JObject rules = JObject.Parse(File.ReadAllText(rulesPath));
        var logValues = ParseLogFile(logPath);
        return CompareWithRules(rules, logValues);
    }

    static Dictionary<string, string> ParseLogFile(string path)
    {
        var values = new Dictionary<string, string>();
        var regex = new Regex(@"(?<key>\w+)\s*=\s*(?<val>-?\d+)", RegexOptions.IgnoreCase);

        var jsonBuffer = new StringBuilder();
        int braceCount = 0;

        foreach (var line in File.ReadLines(path))
        {
            // 일반적인 key=val 패턴도 계속 파싱
            foreach (Match match in regex.Matches(line))
            {
                values[match.Groups["key"].Value] = match.Groups["val"].Value;
            }

            // JSON 블록 수집 시작
            if (line.Contains("{"))
            {
                braceCount += line.Count(c => c == '{');
                braceCount -= line.Count(c => c == '}');
                jsonBuffer.AppendLine(line);

                if (braceCount == 0 && jsonBuffer.Length > 0)
                {
                    string candidate = ExtractJsonPart(jsonBuffer.ToString());
                    try
                    {
                        var jObj = JObject.Parse(candidate);
                        ExtractJsonValues(jObj, values);
                    }
                    catch { /* JSON 파싱 실패 무시 */ }

                    jsonBuffer.Clear();
                }
            }
        }

        return values;
    }

    // 로그 라인 안에서 JSON 부분만 추출
    static string ExtractJsonPart(string raw)
    {
        int start = raw.IndexOf('{');
        int end = raw.LastIndexOf('}');
        if (start >= 0 && end > start)
            return raw.Substring(start, end - start + 1);
        return "{}";
    }

    // 🧠 재귀적으로 JSON key-value 펼치기
    static void ExtractJsonValues(JToken token, Dictionary<string, string> dict, string prefix = "")
    {
        if (token is JObject obj)
        {
            foreach (var prop in obj.Properties())
            {
                ExtractJsonValues(prop.Value, dict, $"{prefix}{prop.Name}.");
            }
        }
        else if (token is JArray array)
        {
            for (int i = 0; i < array.Count; i++)
            {
                ExtractJsonValues(array[i], dict, $"{prefix}{i}.");
            }
        }
        else
        {
            string key = prefix.TrimEnd('.');
            dict[key] = token.ToString();
        }
    }


    private static string CompareWithRules(JObject rules, Dictionary<string, string> logData)
    {
        Debug.WriteLine("compare with rules 호출");
        var sb = new System.Text.StringBuilder();

        foreach (var rule in rules)
        {
            string key = rule.Key;
            var ruleObj = (JObject)rule.Value;

            string description = ruleObj["description"]?.ToString();
            string expected = ruleObj["expected"]?.ToString();
            var map = ruleObj["map"]?.ToObject<Dictionary<string, string>>();

            if (!logData.TryGetValue(key, out string actualRaw))
            {
                sb.AppendLine($"❗ 로그에 키 '{key}' 없음");
                continue;
            }

            string actualHuman = map != null && map.TryGetValue(actualRaw, out var mapped)
                ? mapped
                : actualRaw;

            if (actualHuman == expected)
                sb.AppendLine($"✅ {description} ({key}): {actualHuman} (일치)");
            else
                sb.AppendLine($"❌ {description} ({key}): 기대값 '{expected}', 실제값 '{actualHuman}'");
        }

        return sb.ToString();
    }
}
