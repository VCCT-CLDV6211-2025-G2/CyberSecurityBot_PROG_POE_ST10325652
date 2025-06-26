/*
 * Michaela Ferraris
 * ST10325652
 * PROG6211 POE part 1
 */
using System;
using System.Collections.Generic;

namespace CyberSecurityBot_PROG_POE_ST10325652.CoreLogic
{
    public class Topic
    {
        //Topic name not user name
        public string Name { get; set; } = "";

        //Topic desfinition
        public string Definition { get; set; } = "";

        //Topic List for keywords 
        public List<string> Keywords { get; set; } = new List<string>();

        //Topic dictionary for standard answers
        public Dictionary<string, string> StandardAnswers { get; set; } = new Dictionary<string, string>();

        //Topic List for tips 
        public List<string> Tips { get; set; } = new List<string>();

    }
}
