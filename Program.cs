namespace Parser_Project_Code
{
    using System;
    using System.Collections.Generic;

    class TopDownParser
    {
        static Dictionary<string, List<string>> grammar = new Dictionary<string, List<string>>();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("====================\nRecursive Descent Parsing\n====================");

                Console.WriteLine("1- Define a Grammar\n2- Check a String\n3- Check if Grammar is Simple\n4- Exit");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        DefineGrammar();
                        
                        break;
                    case 2:
                        CheckString();
                        break;
                    case 3:
                        CheckIfGrammarIsSimple();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void DefineGrammar()
        {
            grammar.Clear();
            Console.WriteLine("Enter the number of non-terminals:");
            int nonTerminalCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < nonTerminalCount; i++)
            {
                Console.Write($"Enter non-terminal {i + 1}: ");
                string nonTerminal = Console.ReadLine();

                Console.Write($"Enter the number of rules for {nonTerminal}: ");
                int ruleCount = int.Parse(Console.ReadLine());

                List<string> rules = new List<string>();
                for (int j = 0; j < ruleCount; j++)
                {
                    Console.Write($"Enter rule {j + 1} for {nonTerminal}: ");
                    rules.Add(Console.ReadLine());
                }

                grammar[nonTerminal] = rules;
            }

            Console.WriteLine("Grammar is valid and has been defined.");
        }

        static void CheckString()
        {
            if (grammar.Count == 0)
            {
                Console.WriteLine("Please define a grammar first.");
                return;
            }

            Console.Write("Enter the string to check: ");
            string input = Console.ReadLine();
            List<char> inputList = new List<char>(input);

            bool result = Parse("S", inputList);

            if (result && inputList.Count == 0)
                Console.WriteLine("Your input string is Accepted.");
            else
                Console.WriteLine("Your input string is Rejected.");
        }

        static bool Parse(string nonTerminal, List<char> input)
        {
            if (!grammar.ContainsKey(nonTerminal))
                return false;

            foreach (string rule in grammar[nonTerminal])
            {
                List<char> tempInput = new List<char>(input);
                bool match = true;

                foreach (char symbol in rule)
                {
                    if (char.IsUpper(symbol))
                    {
                        if (!Parse(symbol.ToString(), tempInput))
                        {
                            match = false;
                            break;
                        }
                    }
                    else
                    {
                        if (tempInput.Count == 0 || tempInput[0] != symbol)
                        {
                            match = false;
                            break;
                        }
                        tempInput.RemoveAt(0);
                    }
                }

                if (match)
                {
                    input.Clear();
                    input.AddRange(tempInput);
                    return true;
                }
            }

            return false;
        }

        static void CheckIfGrammarIsSimple()
        {
            if (grammar.Count == 0)
            {
                Console.WriteLine("Please define a grammar first.");
                return;
            }

            bool isSimple = true;

            foreach (var entry in grammar)
            {
                var nonTerminal = entry.Key;
                var rules = entry.Value;

                HashSet<char> startingTerminals = new HashSet<char>();

                foreach (var rule in rules)
                {
                    if (rule.Length > 0)
                    {
                        char firstChar = rule[0];

                        if (!char.IsUpper(firstChar) && startingTerminals.Contains(firstChar))
                        {
                            isSimple = false;
                            break;
                        }
                        startingTerminals.Add(firstChar);

                        if (char.IsUpper(firstChar))
                        {
                            isSimple = false;
                            break;
                        }
                    }
                }

                if (!isSimple)
                    break;
            }

            if (isSimple)
                Console.WriteLine("The grammar is simple.");
            else
                Console.WriteLine("The grammar is not simple.");
        }
    }

}










