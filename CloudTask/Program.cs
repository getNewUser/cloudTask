using System;
using System.Linq;

namespace CloudTask
{
    class Program
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter loan amount:");
                double loan = GetInput();

                Console.WriteLine("Enter years to pay:");
                double years = GetInput();

                Console.WriteLine("Enter annual interest rate without '%':");
                double rate = GetInput();

                Console.WriteLine("Enter one time administration fee percentage without '%'");
                double administrationFeePercent = GetInput();

                double monthlyPayment = CalculateMonthlyPayment(loan, years, rate);
                double administrationFee = AdministrationFee(loan, administrationFeePercent);
                double totalInterest = TotalPaidInterestAmount(monthlyPayment, years, loan);
                double aop = GetAOP(monthlyPayment, loan);

                Console.WriteLine();
                Console.WriteLine("Monthly payment:");
                Console.WriteLine(Math.Round(monthlyPayment,2) + " kr.");

                Console.WriteLine("One time administration fee");
                Console.WriteLine(Math.Round(administrationFee, 2) + " kr.");

                Console.WriteLine("Total interest paid:");
                Console.WriteLine(Math.Round(totalInterest, 2) + " kr.");

                Console.WriteLine("AOP:");
                Console.WriteLine(Math.Round(aop, 2) + "%");


                Console.WriteLine();
                Console.WriteLine("Enter 'y' to calculate again ");
                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    continue;
                }
                else break;
            }
            

        }

        static double GetInput()
        {
            string input = Console.ReadLine();
            double output;
            while ((output = CheckIfDoubleAndPositive(input)) < 0)
            {
                Console.WriteLine("Incorrect format, enter again:");
                input = Console.ReadLine();
            }
            return output;
        }

        static double CheckIfDoubleAndPositive(string input)
        {
            if (double.TryParse(input, out double output))
            {
                if (output >= 0) return output;
                else return -1;
            }
            return -1;
        }

        static double CalculateMonthlyPayment(double loan, double years, double rate)
        {
            rate = rate / 100 / 12;
            double months = years * 12;
            double onePlusRatePowerToMonths = Math.Pow(1 + rate, months);
            double monthlyPayment = (loan * rate * onePlusRatePowerToMonths) / (onePlusRatePowerToMonths - 1);
            return monthlyPayment;
        }

        static double AdministrationFee(double loan, double percent)
        {
            double onePercent = (loan / 100) * percent;
            return onePercent <= 10000 ? onePercent : 10000;
        }

        static double TotalPaidInterestAmount(double monthlyPayment, double years, double loan)
        {
            double months = years * 12;
            double totalAmountPaid = months * monthlyPayment;
            double totalInterest = totalAmountPaid - loan;
            return totalInterest;
        }

        static double GetAOP(double monthlyPayment, double loan)
        {
            double yearPayment = monthlyPayment * 12;
            double aop = (yearPayment / loan) * 100;
            return aop;
        }

    }
}
