namespace Demo_Interface02
{
    namespace Demo01
    {

        class Scooter
        {
            public void DriveScooter()
            {
                Console.WriteLine("Scooter is being driven");
            }
        }

        class Car
        {
            public void DriveCar()
            {
                Console.WriteLine("Car is being driven");
            }

        }

        /// <summary>
        ///     By using Overloaded approach
        ///     PROBLEMS:
        ///     - Knowledge of the object type (by overloaded methods)
        ///     - Knowledge of how to interact with the object (custom method invocations)
        ///     - Maintenance Issue
        /// </summary>
        class Driver
        {
            public void Drive(Scooter objScooter)
            {
                Console.WriteLine("Driver is now driving a Scooter");
                objScooter.DriveScooter();
                Console.WriteLine();
            }

            public void Drive(Car objCar)
            {
                Console.WriteLine("Driver is now driving a Car");
                objCar.DriveCar();
                Console.WriteLine();
            }
        }

        /// <summary>
        ///     The Client
        /// </summary>
        class RunThis
        {
            public static void Run()
            {
                Scooter objScooter = new Scooter();
                Car objCar = new Car();
                Driver objDriver = new Driver();

                Console.WriteLine("--- Client requests Driver to drive Scooter");
                objDriver.Drive(objScooter);

                Console.WriteLine("--- Client requests Driver to drive Car");
                objDriver.Drive(objCar);
            }
        }

    }
}
