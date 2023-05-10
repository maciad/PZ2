

using System.Collections;
using System.Globalization;
using System.Text.Json;
using System.Xml.Serialization;

namespace jsonDeserialize
{
    public class Tweet
    {
        public string? Text { get; set; }
        public string? UserName { get; set; }
        public string? LinkToTweet { get; set; }
        public string? FirstLinkUrl { get; set; }
        public string CreatedAt { get; set; }
        public string? TweetEmbedCode { get; set; }

        public override string ToString()
        {
            return $" UserName: {UserName}, Date: {CreatedAt}";
        }

        public Tweet(string Text, string UserName, string LinkToTweet, string FirstLinkUrl, string CreatedAt, string TweetEmbedCode)
        {
            this.Text = Text;
            this.UserName = UserName;
            this.LinkToTweet = LinkToTweet;
            this.FirstLinkUrl = FirstLinkUrl;
            this.CreatedAt = CreatedAt;
            this.TweetEmbedCode = TweetEmbedCode;
        }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            // ZAD1
            List<Tweet> tweets = readJson("favorite-tweets.jsonl");
            // ZAD2
            // List<Tweet> tweets = fromXML("favorite-tweets.xml");
            // toXML(tweets, "favorite-tweets.xml");
            //ZAD3
            sortByCreatedAt(tweets);
            // sortByUserName(tweets);
            // ZAD4
            Console.WriteLine("najstarszy tweet: " + tweets.First());
            Console.WriteLine("najnowszy tweet: " + tweets.Last());
            // ZAD5
            Dictionary<string, List<Tweet>> tweetsByUser = createDict(tweets);
            //ZAD 6
            Dictionary<string, int> words = countWords(tweets);
            //ZAD 7
            top10words(words);
            //ZAD 8
            Dictionary<string, double> idf = toIDF(words, tweets);
            top10idf(idf);
        }

        static List<Tweet> readJson(string fileName )
        {
            string jsonString = File.ReadAllText(fileName);
            jsonString = "[\n" + jsonString.Replace("\n", ",\n") + "\n]";
            List<Tweet> tweets = JsonSerializer.Deserialize<List<Tweet>>(jsonString);

            return tweets;
        }

        static void toXML(List<Tweet> tweets, string outFileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Tweet>));
            using (TextWriter writer = new StreamWriter(outFileName))
            {
                serializer.Serialize(writer, tweets);
            }
        }

        static List<Tweet> fromXML(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Tweet>));
            using (TextReader reader = new StreamReader(fileName))
            {
                List<Tweet> tweets = (List<Tweet>)serializer.Deserialize(reader);
                return tweets;
            }
        }

        static void sortByUserName(List<Tweet> tweets)
        {
            tweets.Sort((x, y) => x.UserName.CompareTo(y.UserName));
        }

        static void sortByCreatedAt(List<Tweet> tweets)
        {
            tweets.Sort((x, y) => DateTime.ParseExact(x.CreatedAt, "MMMM dd, yyyy 'at' hh:mmtt", CultureInfo.InvariantCulture).CompareTo(DateTime.ParseExact(y.CreatedAt, "MMMM dd, yyyy 'at' hh:mmtt", CultureInfo.InvariantCulture)));
        }
    
        static Dictionary<string, List<Tweet>> createDict(List<Tweet> tweets)
        {
            Dictionary<string, List<Tweet>> tweetsByUser = new Dictionary<string, List<Tweet>>();
            foreach (Tweet tweet in tweets)
            {
                if (tweetsByUser.ContainsKey(tweet.UserName))
                    tweetsByUser[tweet.UserName].Add(tweet);
                else
                    tweetsByUser.Add(tweet.UserName, new List<Tweet>() { tweet });
            }
            return tweetsByUser;
        }
    
        static Dictionary<string, int> countWords(List<Tweet> tweets)
        {
            Dictionary<string, int> words = new Dictionary<string, int>();
            char[] charsToRemove = new char[] { '.', ',', '!', '?', ':', ';', '(', ')', '[', ']', '-', '/', '\\' };
            string text = "";

            foreach (Tweet tweet in tweets)
            {
                text = tweet.Text;
                foreach (var c in charsToRemove)
                {
                    text = text.Replace(c, ' ');
                }
                string[] wordsInTweet = tweet.Text.Split(new char[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);


                foreach (string word in wordsInTweet)
                {
                    if (words.ContainsKey(word))
                        words[word]++;
                    else
                        words.Add(word, 1);
                }
            }
            return words;
        }
    
        static void top10words(Dictionary<string, int> words, int minLen = 5)
        {
            int i = 0;
            Console.WriteLine("TOP 10 WORDS");
            foreach (var word in words.OrderByDescending(key => key.Value))
            {
                if (i == 10)
                    break;
                if (word.Key.Length >= minLen)
                    {
                    Console.WriteLine(word.Key + " - " + word.Value);
                    i++;
                    }
            }
            Console.WriteLine();
        }

        static Dictionary<string, double> toIDF(Dictionary<string, int> words, List<Tweet> tweets)
        {
            int size = tweets.Count;
            double idf = 0;
            Dictionary<string, double> idfDict = new Dictionary<string, double>();
            foreach (var word in words)
            {
                idf = Math.Log(size / word.Value);
                idfDict.Add(word.Key, idf);
            }
            return idfDict;
        }

        static void top10idf(Dictionary<string, double> idfDict)
        {
            int i = 0;
            Console.WriteLine("TOP 10 IDF");
            foreach (var word in idfDict.OrderByDescending(key => key.Value))
            {
                if (i == 10)
                    break;
                Console.WriteLine(word.Key + " idf: " + word.Value);
                i++;
            }
        }
    }
}