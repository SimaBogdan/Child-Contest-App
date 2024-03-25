using System;
using System.Collections.Generic;
using System.Configuration;
using src.Model;
using src.Repository;
using System.Linq;
using log4net.Config;
using log4net;

namespace src
{
    internal class Program
    {
        private static string getConnectionStringByName(string name)
        {
            string returnValue = null;
            
            var settings = ConfigurationManager.ConnectionStrings[name];
            
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
        public static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            IDictionary<string, string> props = new SortedList<string, string>();
            props.Add("ConnectionString", getConnectionStringByName("concurscopiiDB"));
            var participantDbRepository = new ParticipantDBRepository(props);
            
            Console.WriteLine("FindOne: ");
            
            Console.WriteLine(participantDbRepository.findOne(1));

            Console.WriteLine("FindAll: ");

            foreach (var participant in participantDbRepository.findAll())
            {
                Console.WriteLine(participant);
            }
            
            // Console.WriteLine("Add: ");

            // var participantAdd = new Participant(2, "Pop", "Ana", 11, "desen");
            // participantDbRepository.add(participantAdd);
            //
            // foreach (var participant in participantDbRepository.findAll())
            // {
            //     Console.WriteLine(participant);
            // }
            
        }
    }
}