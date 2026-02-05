namespace Demo_Interface02
{
    namespace Demo03
    {

        abstract class Vehicle
        {
            abstract public void Drive();
        }

        class Scooter : Vehicle
        {
            public override void Drive()
            {
                DriveScooter();
            }

            private void DriveScooter()
            {
                Console.WriteLine("Scooter is being driven");
            }
        }

        class Car : Vehicle
        {
            public override void Drive()
            {
                Console.WriteLine("Car is being driven");
            }

        }

        /// <summary>
        ///     By using INHERITANCE approach
        ///     - Maintenance Issue is solved
        ///     - Knowledge of the object type (by inheritance)
        ///     - Knowledge of how to interact with the object (base class defines interactions)
        ///     - Future-Ready (plug-and-play with any vehicle object)
        ///     PROBLEM:
        ///     - code-dependency due to inheritance
        /// </summary>
        class Driver
        {
            public void Drive(Vehicle objVehicle)
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

                Vehicle objVehicle = new Scooter(); 
                objVehicle.Drive();
                objDriver.Drive(objVehicle);
            }
        }

    }
}
