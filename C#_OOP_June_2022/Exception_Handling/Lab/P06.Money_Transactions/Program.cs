using System;
using System.Collections.Generic;

namespace P06.Money_Transactions
{
    internal class Program
    {
        static void Main()
        {
            string[] bankAccountsInput = Console.ReadLine().Split(',');

            var bankAccounts = new Dictionary<string, double>();

            foreach (var account in bankAccountsInput)
            {
                var accountData = account.Split("-");

                bankAccounts[accountData[0]] = double.Parse(accountData[1]);
            }

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                var commandArg = command.Split();
                var cmdType = commandArg[0];
                var account = commandArg[1];
                var sum = double.Parse(commandArg[2]);

                try
                {
                    if (cmdType == "Deposit")
                    {
                        if (!bankAccounts.ContainsKey(account))
                            throw new InvalidAccountExeption();

                        bankAccounts[account] += sum;

                        Console.WriteLine($"Account {account} has new balance: {bankAccounts[account]:f2}");
                    }
                    else if (cmdType == "Withdraw")
                    {
                        if (!bankAccounts.ContainsKey(account))
                            throw new InvalidAccountExeption();

                        if (bankAccounts[account] < sum)
                            throw new InsufficientBalanceExeption();

                        bankAccounts[account] -= sum;

                        Console.WriteLine($"Account {account} has new balance: {bankAccounts[account]:f2}");
                    }
                    else
                    {
                        throw new InvalidCommandExeption();
                    }
                }
                catch (InvalidAccountExeption iae)
                {
                    Console.WriteLine(iae.Message);
                }
                catch (InsufficientBalanceExeption ibe)
                {
                    Console.WriteLine(ibe.Message);
                }
                catch (InvalidCommandExeption ice)
                {
                    Console.WriteLine(ice.Message);
                }
                finally
                {
                    Console.WriteLine("Enter another command");
                }
            }
        }
    }

    class InvalidCommandExeption : Exception
    {
        private const string DefaultExeptionMessage = "Invalid command!";

        public InvalidCommandExeption()
            : base(DefaultExeptionMessage)
        {

        }

        public InvalidCommandExeption(string message)
            : base(message)
        {

        }
    }

    class InvalidAccountExeption : Exception
    {
        private const string DefaultExeptionMessage = "Invalid account!";

        public InvalidAccountExeption()
            : base(DefaultExeptionMessage)
        {

        }

        public InvalidAccountExeption(string message)
            : base(message)
        {

        }
    }

    class InsufficientBalanceExeption : Exception
    {
        private const string DefaultExeptionMessage = "Insufficient balance!";

        public InsufficientBalanceExeption()
            : base(DefaultExeptionMessage)
        {

        }

        public InsufficientBalanceExeption(string message)
            : base(message)
        {

        }
    }
}
