namespace LinqSnipperts
{
    public class Snippets
    {
        // ejemplo basico con lista de texto
        static public void BasicLiqQ()
        {
            string[] cars = { "BMW", "Mercedes", "Audi", "Porsche", "Seat Leon", "VW Golf" };

            //1. select * of cars
            var carList = from car in cars select car;
            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            // 2. select where car is Audi
            var audiList = from car in cars where car.Contains("Audi") select car;
            foreach (var car in audiList)
            {
                Console.WriteLine(car);
            }


        }

        // numeros
        static public void LinqNumbers()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            // Each number multiplied by 3
            // take all numbers, but 9
            // order number by ascending value

            var proccessdNumbers = numbers // take all numbers
                .Select(num => num * 3) // each number multiplied by 3
                .Where(num => num != 9) // take all numbers, but 9
                .OrderBy(num => num); // order number by ascending value

        }

        // busquedas
        static public void SearchExample()
        {
            List<string> textList = new List<string>() { "BMW", "Mercedes", "Audi", "Porsche", "Seat Leon", "VW Golf", "c" };
            // 1. first of all elementes
            var first = textList.First();

            // 2. first element that has in "c"
            var cText = textList.First(text => text.Equals("c"));

            // 3. forst element that contins "j"
            var jText = textList.First(text => text.Contains("j"));

            // 4. First element that contains z or default
            var firtsOrDefaultText = textList.FirstOrDefault(text => text.Contains('z')); // "" or element that contais "z"

            // 5. last element that contains "z" or default
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains('z')); // "" or last element that contais "z"

            // 6. single value
            var uniqueTexts = textList.Single();
            var uniqueOrDefaultText = textList.SingleOrDefault(); // "" or last element that contais "z"

            int[] evenNumber = { 2, 4, 6, 8, 10 };
            int[] otherEventNumer = { 2, 6, 10 };

            // Obtain no repeated elements
            var myEventNumbers = evenNumber.Except(otherEventNumer); // 4, 8
        }

        static public void MultipleSelects()
        {
            // select many
            string[] myOptions = { "Option 1, text 1", "Option 2, text 2", "Option 3, text 3" };
            // separate each option by ","
            var myOpinionSelection = myOptions.SelectMany(opinion => opinion.Split(","));

            // lista de empresas
            var enterprises = new[]
            {
                new Enterprice()
                {
                    Id = 1,
                    Name = "Enterprice 1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Employee 1",
                            Salary = 1000,
                            Email = "employe1@email.com"
                        },
                        new Employee {
                            Id = 2,
                            Name = "Employee 2",
                            Salary = 2000,
                            Email = "employee2@email.com"
                        },
                        new Employee {
                             Id = 3,
                            Name = "Employee 3",
                            Salary = 3000,
                            Email = "employee3@email.com"
                        }
                    }
                },
                 new Enterprice()
                {
                    Id = 2,
                    Name = "Enterprice 2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 4,
                            Name = "Employee 4",
                            Salary = 1000,
                            Email = "employe4@email.com"
                        },
                        new Employee {
                            Id = 5,
                            Name = "Employee 5",
                            Salary = 2000,
                            Email = "employee5@email.com"
                        },
                        new Employee {
                             Id = 6,
                            Name = "Employee 6",
                            Salary = 3000,
                            Email = "employee6@email.com"
                        }
                    }
                }
            };

            // obtain all employess of all enterprises
            var employeesList = enterprises.SelectMany(enterprice => enterprice.Employees);

            // know if a list is empty
            bool hasEnterprises = enterprises.Any(); // return boolean 

            // hast employees
            bool hasEmployees = enterprises.Any(enterprice => enterprice.Employees.Any());

            //  all enterprises at least hast an employee with more that 1000 of salary
            bool hasEmporyeeWuthSalaryMoreThanOrEqual1000 = enterprises.Any(enterprice => enterprice.Employees.Any(employee => employee.Salary > 1000));
        }

        static public void linqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            // iner join
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                                    secondList,
                                    element => element,
                                    secondElement => secondElement,
                                    (element, secondElement) => new { element, secondElement });

            // outer join - left
            var leftOuterJoin = from element in firstList // totos los elementos de la primer lista
                                join secondElement in secondList // todos los elementos de la segunda lista
                                on element equals secondElement // los que son iguales
                                into tempList // guardamos los elementos que son iguales en una lista temporal
                                from tempElement in tempList.DefaultIfEmpty() // si no hay elementos iguales, los dejamos vacios
                                where element != tempElement // excluimos los elementos que son iguales
                                select new { Element = element }; // seleccionamos los elementos de la primer lista

            // simplificar en una sola linea
            var leftOuterJoin2 = from element in firstList
                                 from seconElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = seconElement };

            // outer join - right
            var rightOuterJoin = from secondElement in secondList // totos los elementos de la primer lista
                                 join element in firstList // todos los elementos de la segunda lista
                                on secondElement equals element // los que son iguales
                                into tempList // guardamos los elementos que son iguales en una lista temporal
                                from tempElement in tempList.DefaultIfEmpty() // si no hay elementos iguales, los dejamos vacios
                                where secondElement != tempElement // excluimos los elementos que son iguales
                                select new { Element = secondElement }; // seleccionamos los elementos de la primer lista

            // union
            var unionList = leftOuterJoin.Union(rightOuterJoin);


        }

        static public void SkipTakeLinq()
        {           
            var myList = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // skip => omitir
            var skipTwoFirstValues = myList.Skip(2); // { 3, 4,5,6,7,8,9,10 }
            var skipLastTwoValues = myList.SkipLast(2); // { 1, 2,3,4,5,6,7,8 }
            var skipWhile = myList.SkipWhile(num => num < 5); // { 5,6,7,8,9,10 }

            // take => tomar
            var takeFirstTwoValues = myList.Take(2); // { 1, 2 }
            var takeLastTwoValues = myList.TakeLast(2); // { 9, 10 }
            var takeWhile = myList.TakeWhile(num => num < 5); // { 1, 2, 3, 4 }
        }

        // Paginag: with skip and take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int statrtIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(statrtIndex).Take(resultsPerPage);
        }

        // Variables
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var aboveAverage = from number in numbers
                               let average = numbers.Average() // el let define variables dentro del ambito de la consulta (solo se puede usar dentro de la propia consulta) 
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > average // aqui se puede usar la variable average
                               select number;
            Console.WriteLine("Average: {0}", numbers.Average()); // aqui ya no por lo cual se usa el numbers.Average()
            foreach (var number in aboveAverage)
            {
                Console.WriteLine("Query: {0} Square: {1}", number, Math.Pow(number, 2));
            }
        }

        // ZIP => intercalar registros de dos listas
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };
            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => $"{number} = {word}"); // { 1 = one, 2 = two, 3 = three, 4 = four, 5 = five }
        }

        // Reapeat and Range
        static public void RepeatAndRange()
        {
            // repeat
            var repeat = Enumerable.Repeat("Hello", 3); // { Hello, Hello, Hello }
            // range
            var range = Enumerable.Range(1, 10); // { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
        }

        // All
        // Aggregate
        // Disctinct
        // GroupBy

    }
}
