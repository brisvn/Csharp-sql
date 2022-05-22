using System;
using System.Data.SqlClient;
using System.Text;

namespace ConnectingToSQLServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // connect to SQL server
            establishSQLConn();
            // Get info from user
            Unit currentUnit = getUserInput();
            Console.WriteLine(currentUnit);
            addUnit(currentUnit);
                
        }
        static void establishSQLConn(){

            Console.WriteLine("Getting Connection ...");

            var datasource = @"localhost";//your server
            var database = "Units"; //your database name
            var username = "sa"; //username of server to connect
            var password = "1Secure*Password1"; //password

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
        }
        static Unit getUserInput(){
                Unit newUnit = new Unit();
                Console.WriteLine("Would you like to input a Unit? (Y/N)");
                string response = Console.ReadLine();
                if(response == "Y"){
                    Console.WriteLine("What is the name of your Unit?");
                    response = Console.ReadLine();
                    newUnit.name = response;
                    bool input_corr = false;
                    while(input_corr == false){
                        Console.WriteLine("Insert the stats of your unit 1 at a time in order of M, WS, BS, S, T, W, A, Ld, Sv");
                        response = Console.ReadLine();
                        string[] str_arr=response.Split(" ").ToArray();
                        int[] int_arr= Array.ConvertAll(str_arr,Int32.Parse);
                        if(int_arr.Length < 9){
                            Console.WriteLine("Not enough inputs");
                        }
                        else{
                            newUnit.M = int_arr[0];
                            newUnit.WS = int_arr[1];
                            newUnit.BS = int_arr[2];
                            newUnit.S = int_arr[3];
                            newUnit.T = int_arr[4];
                            newUnit.W = int_arr[5];
                            newUnit.A = int_arr[6];
                            newUnit.Ld = int_arr[7];
                            newUnit.Sv = int_arr[8];
                            input_corr = true;
                        }
                    }
                    List<string> abilityArray = new List<string>();
                    Console.WriteLine("Does your unit have any abilities? (Y/N)");
                    response = Console.ReadLine();
                    if(response == "Y"){
                        Console.WriteLine("Enter the first ability");
                        response = Console.ReadLine();
                        abilityArray.Add(response);
                        bool more = true;
                        while(more){
                            Console.WriteLine("Are there more abilites? (Y/N)");
                            response = Console.ReadLine();
                            if(response == "Y"){
                                Console.WriteLine("Enter the next ability");
                                response = Console.ReadLine();
                                abilityArray.Add(response);
                            }
                            else{
                                more = false;
                            }
                        }
                    }
                    Abilities newAbility = new Abilities(abilityArray, newUnit.name);
                    newUnit.abilities = newAbility;
                    return newUnit;
                }
                return newUnit;
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
            public Abilities abilities;
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
                abilities = new Abilities();
            }
            public Unit(string unitName, int unitM, int unitWS, int unitBS, int unitS, int unitT, int unitW, int unitA, int unitLd, int unitSv, Abilities unitAbilities){
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
                abilities = unitAbilities;
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
        static void addUnit(Unit unit_details){
            
            // StringBuilder strBuilder = new StringBuilder();
            // string Last = archonAbility.abilities.Last();
            //     foreach(string abi in archonAbility.abilities){
            //         string str = $"('{archonAbility.name}', '{abi}'";
            //         strBuilder.Append(str+")");
            //         if(abi != Last){
            //             strBuilder.Append(",");
            //         }
            //     }

            //     Console.WriteLine(strBuilder.ToString());
            //     string sqlQuery = strBuilder.ToString();
            //     using (SqlCommand command = new SqlCommand(sqlQuery, conn)) //pass SQL query created above and connection
            //     {
            //         command.ExecuteNonQuery(); //execute the Query
            //         Console.WriteLine("Query Executed.");
            //     }
        }
    }
}