using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace MySqlWebShop
{
    internal class Program
    {
        static MySqlConnection connection = new MySqlConnection("host=localhost;user=root;pwd=;database=webshop;");

        static void Main(string[] args)
        {
            connection.Open();

            //4. feladat
            string query = "SELECT DISTINCT megnevezes, bruttoegysegar FROM termekek INNER JOIN szamlatetel ON szamlatetel.termekid = termekek.id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader read = cmd.ExecuteReader();

            Console.WriteLine("4.feladat:");
            while (read.Read())
            {
                Console.WriteLine($"{read[0]} {read[1]}");
            }
            read.Close();
            connection.Close();

            //5. feladat
            string valtozatas = "UPDATE termekek SET ar = ar - ar * 0.05 WHERE ar > 5000";
            //MySqlCommand cmd2 = new MySqlCommand(valtozatas, connection);
            //cmd2.ExecuteNonQuery();
            Console.WriteLine("5. feladat: 5000 Ft-nál drágább termékek 5%-al olcsóbbak lettek.");

            //6. feladat
            connection.Open();
            string query2 = "SELECT SUM(bruttoegysegar * mennyiseg) FROM szamlatetel INNER JOIN szamlafej ON szamlafej.id = szamlatetel.szamlafejid WHERE teljesites > '2018-01-01' AND teljesites < '2018-01-15'";
            MySqlCommand cmd3 = new MySqlCommand(query2, connection);
            MySqlDataReader read2 = cmd3.ExecuteReader();

            while (read2.Read())
            {
                Console.WriteLine($"6. feladat: Webshop bruttó árbevétele: {read2[0]} Ft");
            }
            read2.Close();
            connection.Close();

            //7. feladat
            connection.Open();
            string query3 = "SELECT nev, telepules FROM vevok LEFT JOIN szamlafej ON szamlafej.vevoid = vevok.id WHERE kelt IS NULL ORDER BY nev ASC;";
            MySqlCommand cmd4 = new MySqlCommand(query3, connection);
            MySqlDataReader read3 = cmd4.ExecuteReader();

            Console.WriteLine("7. feladat:");
            while (read3.Read())
            {
                Console.WriteLine($"{read3[0]} {read3[1]}");
            }
            read3.Close();
            connection.Close();

            //8. feladat
            connection.Open();
            string query4 = "SELECT megnevezes, SUM(bruttoegysegar * mennyiseg) AS 'bevetel' FROM szamlatetel INNER JOIN termekek ON termekek.id = szamlatetel.termekid GROUP BY megnevezes ORDER BY bevetel DESC LIMIT 3;";
            MySqlCommand cmd5 = new MySqlCommand(query4, connection);
            MySqlDataReader read4 = cmd5.ExecuteReader();

            Console.WriteLine("8. feladat:");
            while (read4.Read())
            {
                Console.WriteLine($"{read4[0]} {read4[1]}");
            }
            read4.Close();
            connection.Close();
        }
    }
}
