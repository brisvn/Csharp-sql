using System;
using System.Data.SqlClient;
using System.Text;

namespace ConnectingToSQLServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection ...");

            var datasource = @"localhost";//your server
            var database = "DatabaseName"; //your database name
            var username = "sa"; //username of server to connect
            var password = "S@LPword"; //password

            //your connection string 
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            //create instanace of database connection
            SqlConnection conn = new SqlConnection(connString);
           

            try
            {
                Console.WriteLine("Openning Connection ...");

                //open connection
                conn.Open();

                Console.WriteLine("Connection successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            //create a new SQL Query using StringBuilder
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append("INSERT INTO Abilities (Unit_Name, Ability) VALUES ");
                
                Unit archon = new Unit("Archon", 8, 2, 2, 3, 3, 5, 5, 9, 5);
                List<string> abilityArray = new List<string>();
                abilityArray.Add("First Ability: This");
                abilityArray.Add("Second Ability: That");
                Abilities archonAbility = new Abilities(abilityArray, "Archon");
                // Console.WriteLine(archon.name);
                // strBuilder.Append(archon);
                string Last = archonAbility.abilities.Last();
                foreach(string abi in archonAbility.abilities){
                    string str = $"('{archonAbility.name}', '{abi}'";
                    strBuilder.Append(str+")");
                    if(abi != Last){
                        strBuilder.Append(",");
                    }
                }

                Console.WriteLine(strBuilder.ToString());
                string sqlQuery = strBuilder.ToString();
                using (SqlCommand command = new SqlCommand(sqlQuery, conn)) //pass SQL query created above and connection
                {
                    command.ExecuteNonQuery(); //execute the Query
                    Console.WriteLine("Query Executed.");
                }
        }
        class Unit{
            public string name;
            public int M;
            public int WS;
            public int BS;
            public int S;
            public int T;
            public int W;
            public int A;
            public int Ld;
            public int Sv;
            public Unit(){
                name = "";
                M = 0;
                WS = 0;
                BS = 0;
                S = 0;
                T = 0;
                W = 0;
                A = 0;
                Ld = 0;
                Sv = 0;
            }
            public Unit(string unitName, int unitM, int unitWS, int unitBS, int unitS, int unitT, int unitW, int unitA, int unitLd, int unitSv){
                name = unitName;
                M = unitM;
                WS = unitWS;
                BS = unitBS;
                S = unitS;
                T = unitT;
                W = unitW;
                A = unitA;
                Ld = unitLd;
                Sv = unitSv;
            }
            public override string ToString(){
                return $"('{name}', '{M}', '{WS}', '{BS}', '{S}','{T}','{W}','{A}','{Ld}','{Sv}')";
            }
        }
        class Abilities{
            public string name;
            public List<string> abilities;
            public Abilities(){
                name = "";
                abilities = new List<string>();
            }
            public Abilities(List<string> abilityArray, string unitName){
                name = unitName;
                abilities = new List<string>();
                foreach(string abi in abilityArray){
                    abilities.Add(abi);
                }
            }
        }
        static void AddUnit(Unit unit_details){

        }
    }
}