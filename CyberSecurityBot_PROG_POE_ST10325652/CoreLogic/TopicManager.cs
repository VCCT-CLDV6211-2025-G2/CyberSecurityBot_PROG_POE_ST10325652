/*
 * Michaela Ferraris
 * ST10325652
 * PROG6211 POE part 1
 */
using System;
using System.Collections.Generic;
using System.Linq;
using FuzzySharp;

namespace CyberSecurityBot_PROG_POE_ST10325652.CoreLogic
{
    //This class manages all topic objects
    public class TopicManager
    {
        private readonly List<Topic> Topics;

        public string Name { get; set; }
        public string Definition { get; set; }
        public List<string> Keywords { get; set; }
        public Dictionary<string, string> StandardAnswers { get; set; }

        #region //Constructor to initialize the TopicManager with predefined topics
        public TopicManager()
        {
            Keywords = new List<string>();
            StandardAnswers = new Dictionary<string, string>();

            //Initialize the list of topics with predefined data
            //Each topic has a name, definition, keywords, tips, and standard answers
            Topics = new List<Topic>
            {

                new Topic
                {
                    Name = "Phishing",
                    Definition = "Phishing is a cyberattack that tricks users into revealing sensitive information through fake emails or websites.",
                    //Implemented a range of keyword for each topic and they are specific to that topic
                    Keywords = new List<string> { "phishing", "email", "fake", "trick", "signs", "scam" },
                    Tips = new List<string>
                    {
                        "Be cautious of emails asking for personal info.",
                        "Check sender addresses carefully before clicking.",
                        "Avoid clicking links in unexpected messages.",
                        "Report suspicious emails to your IT team."
                    },
                    //Dictionary of standard answers for the topic (Phishing) Same words in each topic
                    StandardAnswers = new Dictionary<string, string>()
                    {
                        ["signs"] = "Signs of phishing include fake emails and urgent requests.",
                        ["examples"] = "Phishing examples include fake login pages or emails pretending to be your bank.",
                        ["explain"] = "Phishing is a cyberattack that tricks users through deception.",
                        ["more detail"] = "Phishing attacks often look legitimate and use psychological tactics like urgency or fear to prompt action. They may include spoofed email addresses, logos, or websites to fool you."
                    }
                },
                new Topic
                {
                    Name = "Password Safety",
                    Definition = "A strong password includes uppercase and lowercase letters, numbers, and symbols.",
                    Keywords = new List<string> { "password", "password safety", "strong password", "secure login" },
                    Tips = new List<string>
                    {
                        "Use a unique password for every account.",
                        "Include uppercase, lowercase, numbers, and symbols.",
                        "Avoid using names, birthdays, or common words.",
                        "Use a password manager to keep track of your credentials."
                    },
                    StandardAnswers = new Dictionary<string, string>
                    {
                        ["signs"] = "Weak passwords often contain personal info like names or birthdates.",
                        ["examples"] = "Example: 'P@ssw0rd123!' is strong; '123456' is weak.",
                        ["explain"] = "Password safety is about creating secure and unique passwords for every account.",
                        //If the user asks for more detail, this is the answer
                        ["more detail"] = "Strong passwords reduce the risk of account compromise. Use combinations of characters, avoid reuse, and change passwords regularly."
                    }
                },
                new Topic
                {
                    Name = "Safe Browsing",
                    Definition = "Always check for HTTPS in the URL and avoid downloading from untrusted sources.",
                    Keywords = new List<string> { "safe browsing", "secure", "https", "internet safety" },
                    Tips = new List<string>
                    {
                        "Always check for 'https://' in URLs.",
                        "Don’t download files from untrusted sources.",
                        "Keep your browser and antivirus up to date.",
                        "Avoid clicking suspicious pop-ups."
                    },
                    StandardAnswers = new Dictionary<string, string>
                    {
                        ["signs"] = "Secure websites show a lock icon and start with 'https'.",
                        ["examples"] = "Avoid clicking unknown links or pop-ups. Use secure networks.",
                        ["explain"] = "Safe browsing means using the internet in a secure and cautious way.",
                        ["more detail"] = "It includes avoiding suspicious links, keeping software updated, and using antivirus tools."
                    }
                },
                new Topic
                {
                    Name = "Two-Factor Authentication",
                    Definition = "Two-factor authentication adds a second layer of security to your login process.",
                    Keywords = new List<string> { "2fa", "two factor", "two-factor authentication", "code verification" },
                    Tips = new List<string>
                    {
                        "Enable 2FA on all important accounts.",
                        "Use app-based 2FA like Google Authenticator.",
                        "Avoid SMS-only verification if possible.",
                        "2FA protects your account even if your password is leaked."
                    },
                    StandardAnswers = new Dictionary<string, string>
                    {
                         ["signs"] = "2FA is usually offered as a setting in account security options.",
                         ["examples"] = "Common methods include codes sent via SMS or authenticator apps.",
                         ["explain"] = "2FA requires a password and a second factor, like a code from your phone.",
                         ["more detail"] = "Even if your password is stolen, 2FA makes unauthorized access difficult. It’s an essential security layer."
                    }
                },
                new Topic
                {
                    Name = "Social Engineering",
                    Definition = "Social engineering is manipulating people into giving away confidential information.",
                    Keywords = new List<string> { "social engineering", "manipulation", "impersonation", "tricking" },
                    Tips = new List<string>
                    {
                        "Always verify identities before sharing info.",
                        "Don’t be pressured into urgent decisions.",
                        "Watch out for oversharing on social media.",
                        "Companies will never ask for credentials by phone or email."
                    },
                    StandardAnswers = new Dictionary<string, string>
                    {
                        ["signs"] = "Be wary of messages urging immediate action or asking for personal info.",
                        ["examples"] = "Pretending to be a bank or IT support to get your password.",
                        ["explain"] = "It’s a manipulation tactic used by attackers to deceive victims.",
                        ["more detail"] = "Social engineers exploit human psychology. Always confirm requests through trusted channels."
                    }
                },
                new Topic
                {
                    Name = "Malware",
                    Definition = "Malware is malicious software like viruses, trojans, or spyware that harms systems.",
                    Keywords = new List<string> { "malware", "virus", "trojan", "spyware", "malicious software" },
                    Tips = new List<string>
                    {
                        "Don’t open unknown attachments or links.",
                        "Install and update antivirus software.",
                        "Keep your OS and apps up to date.",
                        "Use secure downloads and app stores."
                    },
                    StandardAnswers = new Dictionary<string, string>
                    {
                        ["signs"] = "Symptoms include slow performance, crashes, or unknown programs.",
                        ["examples"] = "Viruses, ransomware, spyware, and keyloggers are all types of malware.",
                        ["explain"] = "Malware is software created to damage or steal data from your system.",
                        ["more detail"] = "It can spread via email, infected downloads, or fake software. Use antivirus tools and update regularly."
                    }
                },
                new Topic
                {
                    Name = "Ransomware",
                    Definition = "Ransomware encrypts your data and demands payment for its release.",
                    Keywords = new List<string> { "ransomware", "locked files", "pay to unlock", "file encryption" },
                    Tips = new List<string>
                    {
                        "Back up your files regularly.",
                        "Don’t pay the ransom – it doesn't guarantee recovery.",
                        "Be cautious with attachments from unknown sources.",
                        "Use up-to-date security software."
                    },
                    StandardAnswers = new Dictionary<string, string>
                    {
                        ["signs"] = "You may see messages demanding payment to unlock files.",
                        ["examples"] = "WannaCry and CryptoLocker are famous ransomware attacks.",
                        ["explain"] = "Ransomware locks your files and asks for payment to unlock them.",
                        ["more detail"] = "It spreads via malicious email or downloads. Backups and antivirus tools are key to protection."
                    }
                },
                new Topic
                {
                    Name = "Privacy",
                    Definition = "Privacy involves protecting your personal and sensitive data from misuse or exposure.",
                    Keywords = new List<string> { "privacy", "data protection", "personal info", "online privacy" },
                    Tips = new List<string>
                    {
                        "Limit what you share on social media.",
                        "Adjust privacy settings on your accounts.",
                        "Don’t give your info to untrusted websites.",
                        "Use secure and encrypted communication apps."
                    },
                    StandardAnswers = new Dictionary<string, string>
                    {
                        ["signs"] = "Too much personal info online puts your privacy at risk.",
                        ["examples"] = "Sharing your birthday or home address on public profiles.",
                        ["explain"] = "Privacy is about controlling who sees and uses your personal data.",
                        ["more detail"] = "Review privacy settings, avoid oversharing, and use end-to-end encryption when possible."
                    }
                }

            };

        }
        public string GetBestResponse(string userInput)
        {
            string bestMatch = null;
            int highestScore = 0;

            // Search standard questions
            foreach (var question in StandardAnswers.Keys)
            {
                int score = Fuzz.Ratio(userInput.ToLower(), question.ToLower());
                if (score > highestScore)
                {
                    highestScore = score;
                    bestMatch = question;
                }
            }

            // If close enough match found in standard answers
            if (highestScore >= 80)  // Adjust threshold as needed
            {
                return StandardAnswers[bestMatch];
            }

            // Now try keyword matching
            foreach (var keyword in Keywords)
            {
                int score = Fuzz.PartialRatio(userInput.ToLower(), keyword.ToLower());
                if (score > highestScore)
                {
                    highestScore = score;
                    bestMatch = keyword;
        }
            }

            if (highestScore >= 75)
            {
                return $"That sounds like it’s related to: {bestMatch}. Here's what I can tell you: {Definition}";
            }

            return "Sorry, I’m not sure what you mean. Try rephrasing or choosing a topic.";
        }

        #endregion
//--------------------------------------------------------------------------------------------------------------------------------------//

        //Method that returns a list of all topic names when to user chooses to select a topic from the spectre console list
        public List<string> GetAllTopicNames()
        {
            return Topics.Select(t => t.Name).ToList();
        }
//--------------------------------------------------------------------------------------------------------------------------------------//

        //Method to take the topic name that the user has selected a topic and it returns the corresponding topic object
        //so that the user knows they must ask about that specific topic
        public Topic GetTopicByName(string name)
        {
            return Topics.FirstOrDefault(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
//--------------------------------------------------------------------------------------------------------------------------------------//

        //Method to check the user input topic and matches it based on the keywords
        //I have called this method for when the user asks for tips and to check the users favourite topic
        public Topic MatchTopic(string input)
        {
            return Topics.FirstOrDefault(t =>
            t.Keywords.Any(k => ContainsIgnoreCase(input, k)));
        }
//--------------------------------------------------------------------------------------------------------------------------------------//

        //Method returns a random tip from a topics list of tips
        public string GetRandomTip(Topic topic)
        {
            //First check if the topic is null or if the tips list is empty
            if (topic?.Tips == null || topic.Tips.Count == 0)
                return "No tips available for this topic";

            //Use Random to pick a tip from the list and return it
            var random = new Random();
            return topic.Tips[random.Next(topic.Tips.Count)];
        }
//--------------------------------------------------------------------------------------------------------------------------------------//

        //Method to check if the users question contains any keywords from the topics predefined answers 
        public string GetStandardAnswer(Topic topic, string question)
        {
            //Looping through each keyword in the StandardAnswers dictionary
            foreach (var kvp in topic.StandardAnswers)
            {
                //If the keyword is fouund in the users input returns the associated answer
                if (question?.IndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                    return kvp.Value;
            }

            return null;
        }
//--------------------------------------------------------------------------------------------------------------------------------------//

        //Method used to generate a reply based on the user's questions
        public string GetTopicAnswer(Topic topic, string question)
        {
            //If the question includes the word tip, return a random tip from the topic
            if (ContainsIgnoreCase(question, "tip"))
                return GetRandomTip(topic);

            //If not it check for a match using the GetStandardAnswer method
            var standardAnswer = GetStandardAnswer(topic, question);
            //If a match is found, return the standard answer
            if (standardAnswer != null)
                return standardAnswer;

            return topic.Definition;
            //If no match is found, return a default message
            //return "That's a great question. Try rephrasing or asking for a tip!";
        }
//--------------------------------------------------------------------------------------------------------------------------------------//

        //Helper method to check if the source string contains the specified keyword, ignoring case sensitivity
        //Used in both MatchTopic and GetStandardAnswer methods to make matching case insensitive
        private bool ContainsIgnoreCase(string source, string keyword)
        {
            return source?.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }
//--------------------------------------------------------------------------------------------------------------------------------------//


    }
}

//References:
//Line 255: https://chatgpt.com/share/68349748-e16c-8003-9348-10b61ac47a16
