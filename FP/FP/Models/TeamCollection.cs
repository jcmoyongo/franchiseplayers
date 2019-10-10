using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;

namespace FP.Models
{
    class TeamCollection
    {

        public static List<Team> GetTeams()
        {
            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(TeamCollection)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("FP.Data.Teams1920.csv");

                using (StreamReader sr = new StreamReader(stream))
                {
                    sr.ReadLine(); //Skip header/first line
                    string team;

                    List<Team> teams = new List<Team>();

                    while ((team = sr.ReadLine()) != null)
                    {
                        var arr = team.Split(new char[] { ',' });
                        teams.Add(
                            new Team(
                                code: arr[0], 
                                fullName: arr[1], 
                                shortname: arr[2], 
                                nickName: arr[3], 
                                division: arr[4], 
                                conference: arr[5],
                                arena: arr[6]
                                )
                        );
                    }

                    return teams;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
