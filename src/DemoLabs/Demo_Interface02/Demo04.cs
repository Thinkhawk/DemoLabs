namespace Demo_Interface02
{
    namespace Demo04
    {

        /// <summary>
        ///     Defines the contract for interactions
        /// </summary>
        interface IVehicle
        {
            void Drive();
        }

        class Scooter : IVehicle
        {
            public void Drive()
            {
                DriveScooter();
            }

            private void DriveScooter()
            {
                Console.WriteLine("Scooter is being driven");
            }
        }

        class Car : IVehicle
        {
            public void Drive()
            {
                Console.WriteLine("Car is being driven");
            }

        }

        /// <summary>
        ///     By using INTERFACE approach
        ///     - Maintenance Issue is solved
        ///     - Knowledge of the object type (by IMPLEMENTATION)
        ///     - Knowledge of how to interact with the object (interface defines interactions)
        ///     - Future-Ready (plug-and-play with any vehicle object)
        ///     - code-dependency due to inheritance
        /// </summary>
        class Driver
        {
            public void Drive(IVehicle objVehicle)
            {
                Console.WriteLine("Driver is now driving a {0}", objVehicle.GetType().Name.ToUpper() );
                objVehicle.Drive();
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
