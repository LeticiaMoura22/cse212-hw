using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // O(n): one pass. For each word, check whether its reverse was already seen.
        var seen = new HashSet<string>();
        var results = new List<string>();

        foreach (var word in words)
        {
            // Words with identical letters (e.g. "aa") can never form a pair.
            if (word[0] != word[1])
            {
                var reversed = new string(new[] { word[1], word[0] });
                if (seen.Contains(reversed))
                {
                    results.Add($"{reversed} & {word}");
                }
            }

            seen.Add(word);
        }

        return results.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (fields.Length < 4)
            {
                continue; // skip blank / malformed lines
            }

            var degree = fields[3].Trim();
            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Count the letters of word1 (ignoring spaces and case), then subtract
        // the letters of word2. The words are anagrams if every count ends at 0.
        var counts = new Dictionary<char, int>();
        var total = 0; // net number of letters still unmatched

        foreach (var ch in word1)
        {
            if (ch == ' ') continue;
            var c = char.ToLowerInvariant(ch);
            counts[c] = counts.GetValueOrDefault(c) + 1;
            total++;
        }

        foreach (var ch in word2)
        {
            if (ch == ' ') continue;
            var c = char.ToLowerInvariant(ch);
            if (!counts.TryGetValue(c, out var n) || n == 0)
            {
                return false; // letter missing or used too many times
            }

            counts[c] = n - 1;
            total--;
        }

        return total == 0;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // The FeatureCollection / Feature / Properties classes (FeatureCollection.cs)
        // describe the JSON so that Deserialize above works.
        var results = new List<string>();
        if (featureCollection?.Features == null)
        {
            return results.ToArray();
        }

        foreach (var feature in featureCollection.Features)
        {
            var place = feature?.Properties?.Place ?? "Unknown location";
            var mag = feature?.Properties?.Mag;
            // InvariantCulture keeps the decimal point as '.' regardless of the machine's locale.
            var magText = mag.HasValue
                ? mag.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)
                : "N/A";
            results.Add($"{place} - Mag {magText}");
        }

        return results.ToArray();
    }
}