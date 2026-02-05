namespace Demo_Interface02.Demo02
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
    ///     By using BOXING and UNBOXING
    ///      - Maintenance Issue is solved using Boxing/Unboxing
    ///     PROBLEMS:
    ///     - Knowledge of the object type (by overloaded methods)
    ///     - Knowledge of how to interact with the object (custom method invocations)
    /// </summary>
    class Driver
    {
        public void Drive(object objVehicle)
        {
            if (objVehicle.GetType() == typeof(Scooter))
            {
                Scooter objScooter = (Scooter)objVehicle;           // unboxing (can raise exception)
                Console.WriteLine("Driver is now driving a Scooter");
                objScooter.DriveScooter();
                Console.WriteLine();
            }
            else if (objVehicle is Car)
            {
                Car? objCar = objVehicle as Car;                    // TYPE-SAFE unboxing
                if (objCar is not null)
                {
                    Console.WriteLine("Driver is now driving a Car");
                    objCar.DriveCar();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Driver does not know how to drive this object!");
            }
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
            objDriver.Drive(objScooter);        // implicit boxing

            Console.WriteLine("--- Client requests Driver to drive Car");
            objDriver.Drive((object)objCar);    // explicit boxing

            object o = objCar;                  // explicit boxing
            objDriver.Drive(o);
        }
    }

}

