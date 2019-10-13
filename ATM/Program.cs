using System;

namespace ATM
{
    class ATM
    {
        // all answers through the program from user
        static int answer = 0;
        static int answer0 = 0;
        static int answer1 = 0;
        static int answer2 = 0;
        static int answer3 = 0;
        static int answer4 = 0;
        static int answer5 = 0;
        static int answer6 = 0;
        static int billsLimit = 1000; // bills limit into ATM
        static int billsForNow = 500; // how many bills in ATM at the moment 
        static int total = 0; // money supply inside ATM
        static string[] separatedBills; // strings with ranks of bills
        static int[] bills; // converted separatedBills to integers
        static int[] billsValues = {10, 50, 100, 500, 1000, 5000};

        // intermediate flags for interaction with user
        static bool flag = true;
        static bool anotherFlag = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the program of performing the second test task by Aleksandr Solovyov.");
            Console.WriteLine("The program represent simple model of automatic tenner machine (ATM).");
            Console.WriteLine("ATM able to take money, give it out, show itself status.");
            Console.WriteLine("ATM has a limit of bills it can to store within, value of bills is 10, 50, 100, 500, 1000, 5000 of conventional units.");
            Console.WriteLine("So let's start again!\n");

            // the program is going on cycle
            while (flag)
            {
                Console.WriteLine("Select the operation: ");
                Console.WriteLine("1 - put money;\n2 - withdraw money;\n3 - show the ATM status;\n0 - exit the program.");

                try
                {
                    answer = Convert.ToInt32(Console.ReadLine());
                } catch (Exception)
                {
                    Console.WriteLine("Number allowed only. Try again please.");
                }

                switch (answer)
                {
                    // checking if ATM has a space for putting money
                    case 1:
                        if (billsForNow < billsLimit)
                            putMoney();
                        else
                        {
                            Console.WriteLine("ATM bills limit is overflowed. Select the action: 1 - withdraw money, 0 - back to main menu.");
                            while (true)
                            {
                                try
                                {
                                    answer0 = Convert.ToInt32(Console.ReadLine());
                                    break;
                                } catch (Exception)
                                {
                                    Console.WriteLine("Number allowed only. Try again please.");
                                }
                            }

                            switch (answer0)
                            {
                                case 1:
                                    withdrawMoney();
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    // calling method for withdrawing money
                    case 2:
                        if (total != 0)
                            withdrawMoney();
                        // checking if ATM is empty and proposing to put the money
                        else
                        {
                            while (true)
                            {
                                Console.WriteLine("ATM has no money for withdrawing. Want to put (1 - yes, 0 - no) ?");
                                try
                                {
                                    answer3 = Convert.ToInt32(Console.ReadLine());
                                    break;
                                } catch (Exception)
                                {
                                    Console.WriteLine("Number allowed only. Try again please.");
                                }
                            }

                            switch (answer3)
                            {
                                case 1:
                                    putMoney();
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    // showing the ATM status
                    case 3:
                        Console.WriteLine("ATM has bills limit by " + billsLimit + " bills.\nNow it's " + billsForNow + " bills inside, money supply is " + total + " conventional units.\n" + (billsLimit - billsForNow)+ " bills left to put.");
                        break;
                    // ending of program
                    case 0:
                        Console.WriteLine("Thank you for coming, see you later!");
                        flag = false;
                        break;
                    default:
                        break;
                }
            }
        } //Main()

        // method for putting money, calculating internal space for putting with printing warnings while necesssary
        static void putMoney()
        {
            anotherFlag = true;
        
            Console.WriteLine("ATM accept the bills of 10, 50, 100, 500, 1000 and 5000 conventional units values only.");
            // checking if it's a space for putting money
            if ((billsLimit - billsForNow) < billsLimit)
            {
                while (anotherFlag)
                {
                    Console.WriteLine("ATM has a limit of bills quantity - 1000 bills. Now you have " + (billsLimit - billsForNow) + " bills to put.");
                    Console.WriteLine("Enter the amount of every bill starting from 10 separated by space (10 50 100 500 1000 5000): ");
                    string inputBills = Console.ReadLine();

                    // dividing user's input to separated parts
                    separatedBills = inputBills.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    bills = new int[separatedBills.Length];

                    // converting user's input to integers
                    for (int i = 0; i < bills.Length; i++)
                    {
                        bills[i] = Convert.ToInt32(separatedBills[i]);
                        billsForNow += bills[i];
                    }

                    // checking if ATM bills limit is excedeed and proposing options to solve it
                    if (billsForNow > billsLimit)
                    {
                        // subtracting entered amount of bills
                        for (int i = 0; i < bills.Length; i++)
                            billsForNow -= bills[i];

                        Console.WriteLine("ATM bills limit is exceeded, you can put " + (billsLimit - billsForNow) + " bills only.");
                        Console.WriteLine("Select the action: 1 - try again with another bills amount, 0 - back to main menu.");
                        try
                        {
                            answer2 = Convert.ToInt32(Console.ReadLine());
                        } catch (Exception)
                        {
                            Console.WriteLine("Number allowed only. Try again please.");
                        }

                        switch (answer2)
                        {
                            // going the cycle of putting on
                            case 1:
                                break;
                            // quit the cycle and return to back menu
                            default:
                                anotherFlag = false;
                                break;
                        }
                    }
                    // process of putting money and calculating internal money supply
                    else
                    {
                        anotherFlag = false;
                        for (int i = 0; i < bills.Length; i++)
                            total += bills[i] * billsValues[i];
                    }
                    Console.WriteLine("Your facilities were successfully enrolled.\n");
                } // while (anotherFlag)
            }
            // if it's no space for putting money
            else
            {
                while (true)
                {
                    Console.WriteLine("ATM has no space for putting money. Want to withdraw (1 - yes, 0 - no) ?");
                    try
                    {
                        answer1 = Convert.ToInt32(Console.ReadLine());
                        break;
                    } catch (Exception)
                    {
                        Console.WriteLine("Number allowed only. Try again please.");
                    }
                }

                switch (answer1)
                {
                    case 1:
                        withdrawMoney();
                        break;
                    default:
                        break;
                }
            }
        } // putMoney()

        // method for withdrawing money, calculating internal space for withdrawing with printing warnings while necesssary
        static void withdrawMoney()
        {
            while (true)
            {
                Console.WriteLine("ATM's money supply is " + total + " conventional units at the moment. How much you would like to withdraw?");
                try
                {
                    answer4 = Convert.ToInt32(Console.ReadLine());
                    
                    // checking if input more that money supply inside the ATM
                    if (answer4 > total)
                        Console.WriteLine("You can't withdraw more than ATM has at the moment. Try again.");
                    // checking about correct input of sum
                    else if (answer4 % 10 != 0)
                        Console.WriteLine("Choose the correct value of withrdrawing money (multiple by 10).");
                    // if it's okay
                    else
                    {
                        while (true)
                        {
                            Console.WriteLine("You want to withdraw " + answer4 + " conventional units. What king of bills would you like to take your money (10, 50, 100, 500, 1000, 5000) ?");
                            try
                            {
                                answer5 = Convert.ToInt32(Console.ReadLine());

                                // checking about correct input of sum again
                                if (answer4 % answer5 != 0)
                                Console.WriteLine("There is no opportunity for withdrawing with this bills value. Try to choose correct value of bills.");
                                // if it's correct input
                                else if (answer5 == 10 || answer5 == 50 || answer5 == 100 || answer5 == 500 || answer5 == 1000 || answer5 == 5000)
                                    break;
                                // other error
                                else
                                    Console.WriteLine("Enter the correct bills value.");
                            } catch (Exception)
                            {
                                Console.WriteLine("Number allowed only. Try again please.");
                            }
                        }

                        // if ATM has no bills to issue the user's sum
                        if (billsForNow < (answer4 / answer5))
                            Console.WriteLine("ATM is out of bills, you can't withdraw your money.");
                        // if it's okay
                        else
                        {
                            total -= answer4; // subtracting the etntered sum from money supply inside ATM
                            billsForNow -= answer4 / answer5; // subtracting the etntered amount of bills from bills inside ATM
                            Console.WriteLine("Withdrawed successfully. You have got " + answer4 + " conventional units with " + (answer4 / answer5) +
                                " bills of " + answer5 + " value. ATM has " + total + " conventional units by now.");
                        }
                        break;
                    }
                } catch (Exception)
                {
                    Console.WriteLine("Number allowed only. Try again please.");
                }
            }
        } // withdrawMoney()
    }
}