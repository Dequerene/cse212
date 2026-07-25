using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character
    /// words (lower case, no duplicates). Using sets, find an O(n)
    /// solution for returning all symmetric pairs of words.
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return:
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the
    /// specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else and therefore should not be returned.
    /// </summary>
    /// <param name="words">
    /// An array of 2-character words (lowercase, no duplicates)
    /// </param>
    public static string[] FindPairs(string[] words)
    {
        var seenWords = new HashSet<string>();
        var pairs = new List<string>();

        foreach (var word in words)
        {
            var reversedWord = $"{word[1]}{word[0]}";

            // Words such as "aa" do not form symmetric pairs.
            if (word[0] != word[1] &&
                seenWords.Contains(reversedWord))
            {
                pairs.Add($"{word} & {reversedWord}");
            }

            seenWords.Add(word);
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file. The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that
    /// have earned that degree. The degree information is in
    /// the 4th column of the file. There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>A dictionary containing each degree and its count</returns>
    public static Dictionary<string, int> SummarizeDegrees(
        string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(',');

            if (fields.Length <= 3)
            {
                continue;
            }

            var degree = fields[3].Trim();

            if (degrees.TryGetValue(
                    degree,
                    out var currentCount))
            {
                degrees[degree] = currentCount + 1;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if word1 and word2 are anagrams. An anagram
    /// uses the same letters and the same number of each letter.
    /// Spaces and letter case are ignored. A dictionary is used
    /// to solve the problem in O(n) time.
    /// </summary>
    public static bool IsAnagram(
        string word1,
        string word2)
    {
        var letterCounts = new Dictionary<char, int>();

        foreach (var character in word1)
        {
            if (character == ' ')
            {
                continue;
            }

            var letter = char.ToLowerInvariant(character);

            if (letterCounts.TryGetValue(
                    letter,
                    out var currentCount))
            {
                letterCounts[letter] = currentCount + 1;
            }
            else
            {
                letterCounts[letter] = 1;
            }
        }

        foreach (var character in word2)
        {
            if (character == ' ')
            {
                continue;
            }

            var letter = char.ToLowerInvariant(character);

            if (!letterCounts.TryGetValue(
                    letter,
                    out var currentCount) ||
                currentCount == 0)
            {
                return false;
            }

            letterCounts[letter] = currentCount - 1;
        }

        foreach (var count in letterCounts.Values)
        {
            if (count != 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Read current-day earthquake data from the USGS GeoJSON feed
    /// and return a formatted summary of each location and magnitude.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri =
            "https://earthquake.usgs.gov/earthquakes/feed/v1.0/" +
            "summary/all_day.geojson";

        using var client = new HttpClient();

        using var getRequestMessage =
            new HttpRequestMessage(
                HttpMethod.Get,
                uri);

        using var response =
            client.Send(getRequestMessage);

        response.EnsureSuccessStatusCode();

        using var jsonStream =
            response.Content.ReadAsStream();

        using var reader =
            new StreamReader(jsonStream);

        var json = reader.ReadToEnd();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var featureCollection =
            JsonSerializer.Deserialize<FeatureCollection>(
                json,
                options);

        if (featureCollection?.Features == null)
        {
            return [];
        }

        var summaries = new List<string>();

        foreach (var feature in featureCollection.Features)
        {
            if (feature?.Properties == null)
            {
                continue;
            }

            summaries.Add(
                $"{feature.Properties.Place} - Mag " +
                $"{feature.Properties.Mag}"
            );
        }

        return summaries.ToArray();
    }
}