using Db_CodeFirst.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Db_CodeFirst
{
    class Program
    {
        static string connectionString = new ConnectionStringManager().ConnectionString;

        static void Main(string[] args)
        {
            var passengers = GetPassengers();
            var trips = GetTrips();
            var passInTrip = GetPassInTrip(trips, passengers);
            CreateSqlCommand(AddPassengers(passengers));
            CreateSqlCommand(AddCompanies());
            CreateSqlCommand(AddTrips(trips));
            CreateSqlCommand(AddPassInTrip(passInTrip));
            CreateSqlCommand(UpdateTown("Kurumoch", "Samara"));
            CreateSqlCommand(DeletePassInTrip("Company1"));
            CreateSqlCommand(DeleteTrip("Company1"));
        }

        static void CreateSqlCommand(string commandText)
        {
            using var connection = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(commandText, connection);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
        }

        static string UpdateTown(string oldTown, string newTown)
        {
            var commandText = $"UPDATE Trip SET TownFrom = '{newTown}' WHERE TownFrom = '{oldTown}'";
            return commandText;
        }

        static string DeletePassInTrip(string company)
        {
            var commandText = $"DELETE FROM PassInTrip WHERE PassInTrip.TripNo IN " +
                $"(SELECT Trip.TripNo FROM Trip WHERE Trip.CompId IN " +
                $"(SELECT Company.CompId FROM Company WHERE Company.[Name] = '{company}'))";
            return commandText;
        }

        static string DeleteTrip(string company)
        {
            var commandText = $"DELETE FROM Trip WHERE Trip.CompId IN " +
               $"(SELECT Company.CompId FROM Company WHERE Company.[Name] = '{company}')";
            return commandText;
        }

        static string AddCompanies()
        {
            sb = new StringBuilder($"INSERT INTO Company(CompId, Name) VALUES ");
            for (int i = 0; i < companies.Length; i++) 
            {
                sb.Append($"({i+1}, '{companies[i]}')");
                if (i != companies.Length - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        static string AddPassengers(List<Passenger> passengers)
        {
            sb = new StringBuilder($"INSERT INTO Passenger(PassId, Name) VALUES ");
            for (int i = 0; i < passengers.Count; i++)
            {
                sb.Append($"({passengers[i].PassId}, '{passengers[i].Name}')");
                if (i != passengers.Count - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        static string AddTrips(List<Trip> trips)
        {
            sb = new StringBuilder($"INSERT INTO Trip VALUES ");
            int i = 0;
            foreach (var trip in trips)
            {
                i++;
                var timeOut = trip.TimeOut.Year + "-" + trip.TimeOut.Month + "-" + trip.TimeOut.Day + " " + trip.TimeOut.Hour + ":00:00";
                var timeIn = trip.TimeIn.Year + "-" + trip.TimeIn.Month + "-" + trip.TimeIn.Day + " " + trip.TimeIn.Hour + ":00:00";
                sb.Append($"({trip.TripNo}, {trip.CompId}, '{trip.Plane}', '{trip.TownFrom}', '{trip.TownTo}', '{timeOut}', '{timeIn}'),");                
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        static string AddPassInTrip(List<PassInTrip> passInTrip)
        {
            sb = new StringBuilder($"INSERT INTO PassInTrip VALUES ");
            int i = 0;
            foreach(var pass in passInTrip)
            {
                i++;
                var date = pass.Date.Year + "-" + pass.Date.Month + "-" + pass.Date.Day + " " + pass.Date.Hour + ":00:00";
                sb.Append($"({pass.TripNo}, '{date}', {pass.PassId}, '{pass.Place}'),");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        static List<Passenger> GetPassengers()
        {
            var passengers = new List<Passenger>();
            for (int i = 0; i < 3; i++)
            {
                passengers.Add(new Passenger()
                {
                    PassId = i + 1,
                    Name = $"Passenger{i + 1}"
                });
            }
            return passengers;
        }

        static List<Trip> GetTrips()
        {
            var random = new Random();
            var trips = new List<Trip>();
            for (int i = 0; i < 7; i++)
            {
                var timeOut = new DateTime(random.Next(2000, DateTime.Now.Year), random.Next(1, 13), random.Next(1, 28));
                var timeIn = timeOut.AddHours(random.Next(1, 13));
                trips.Add(new Trip()
                {
                    TripNo = i + 1,
                    CompId = random.Next(1, 4),
                    Plane = plane[random.Next(3)],
                    TownFrom = townFrom[random.Next(townFrom.Length)],
                    TownTo = townTo[random.Next(townTo.Length)],
                    TimeOut = timeOut,
                    TimeIn = timeIn
                });
            }
            return trips;
        }

        static List<PassInTrip> GetPassInTrip(List<Trip> trips, List<Passenger> passengers)
        {
            var random = new Random();
            var passInTrip = new List<PassInTrip>();
            for (int i = 0; i < trips.Count; i++)
            {
                for (int j = 0; j < passengers.Count; j++)
                {
                    passInTrip.Add(new PassInTrip()
                    {
                        TripNo = trips[i].TripNo,
                        Date = trips[i].TimeOut,
                        PassId = passengers[j].PassId,
                        Place = random.Next(1, 11).ToString() + (char)('A' + random.Next(10))
                    });
                }
            }
            return passInTrip;
        }

        static string[] companies = { "Company1", "Company2", "Company3" };
        static string[] plane = { "Plane1", "Plane2", "Plane3" };
        static string[] townFrom = { "Moscow", "Paris", "Kurumoch" };
        static string[] townTo = { "Berlin", "Vegas", "Tashkent" };
        static StringBuilder sb; 
    }
}
