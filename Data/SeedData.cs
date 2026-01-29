using Microsoft.EntityFrameworkCore;
using NowyPrzewodnikMVC.Models;

namespace NowyPrzewodnikMVC.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<AppDbContext>())
            {
                // 1. Sprawdzamy, czy baza jest już pełna. Jeśli tak - przerywamy.
                if (context.Waypoints.Any())
                {
                    return;
                }

                // 2. TWORZENIE MIEJSC (Waypoints)
                // Uwaga: Nie podajemy ID ręcznie. SQL Server nada je automatycznie: 1, 2, 3...
                // Ważne jest zachowanie kolejności z Twojego JSONa!

                var waypoints = new Waypoint[]
                {
                    new Waypoint { Name = "Wejście", ImageUrl = "/media/img_1cabfd1d.png" }, // ID: 1
                    new Waypoint { Name = "Portiernia", ImageUrl = "/media/img_6ac4e439.png" }, // ID: 2
                    new Waypoint { Name = "Korytarz przed sekretariatem", ImageUrl = "/media/img_1d21d1a7.png" }, // ID: 3
                    new Waypoint { Name = "Sekratariat w środku", ImageUrl = "/media/img_f987822e.png" }, // ID: 4
                    new Waypoint { Name = "Korytarz", ImageUrl = "/media/img_fdfd0354.png" }, // ID: 5
                    new Waypoint { Name = "Instytut kształcenia podyplomowego", ImageUrl = "/media/img_90f90473.png" }, // ID: 6
                    new Waypoint { Name = "Korytarz lewo 1", ImageUrl = "/media/img_10165ddc.png" }, // ID: 7
                    new Waypoint { Name = "Koordynator do spraw dostepnosci", ImageUrl = "/media/img_d80cd2c9.png" }, // ID: 8
                    new Waypoint { Name = "Lewo od korytarz 1", ImageUrl = "/media/img_12b11628.png" }, // ID: 9
                    new Waypoint { Name = "Korytarz lewo 2", ImageUrl = "/media/img_25e17855.png" }, // ID: 10
                    new Waypoint { Name = "Sala komputerowa", ImageUrl = "/media/img_0ea02410.png" }, // ID: 11
                    new Waypoint { Name = "Prawo od korytarz lewo 2", ImageUrl = "/media/img_baebb3a9.png" }, // ID: 12
                    new Waypoint { Name = "Korytarz i winda dla niepełnosprawnych", ImageUrl = "/media/img_a71d7cf6.png" }, // ID: 13
                    new Waypoint { Name = "Wnęka, strefa relaksu", ImageUrl = "/media/img_e59fd162.png" }, // ID: 14
                    new Waypoint { Name = "Korytarz lewo 3", ImageUrl = "/media/img_fe3d442f.png" }, // ID: 15
                    new Waypoint { Name = "Studium kształcenia na odległość", ImageUrl = "/media/img_e6857f89.png" }, // ID: 16
                    new Waypoint { Name = "Korytarz lewo 4", ImageUrl = "/media/img_3b7346ca.png" }, // ID: 17
                    new Waypoint { Name = "Korytarz lewo 5", ImageUrl = "/media/img_bb711e3f.png" }, // ID: 18
                    new Waypoint { Name = "Sala wykładowa a8", ImageUrl = "/media/img_42394f77.png" }, // ID: 19
                    new Waypoint { Name = "Toalety w prawo od korytarz lewo 5", ImageUrl = "/media/img_59a1ae60.png" }, // ID: 20
                    new Waypoint { Name = "Korytarz lewo 6", ImageUrl = "/media/img_97a3f79f.png" }, // ID: 21
                    new Waypoint { Name = "Sala wykładowa a9", ImageUrl = "/media/img_9d6f1e71.png" }, // ID: 22
                    new Waypoint { Name = "Wyjście ewakuacyjne i schody", ImageUrl = "/media/img_039dfc8a.png" }, // ID: 23
                    new Waypoint { Name = "Klatka schodowa lewo 1", ImageUrl = "/media/img_49f8c910.png" }, // ID: 24
                    new Waypoint { Name = "Klatka schodowa lewo 2", ImageUrl = "/media/img_f7e03b35.png" }, // ID: 25
                    new Waypoint { Name = "Klatka schodowa lewo 3", ImageUrl = "/media/img_3d416ca1.png" }, // ID: 26
                    new Waypoint { Name = "Klatka schodowa lewo 4", ImageUrl = "/media/img_97c9ed27.png" }, // ID: 27
                    new Waypoint { Name = "Klatka schodowa góra 1", ImageUrl = "/media/img_e02b43dd.png" }, // ID: 28
                    new Waypoint { Name = "Klatka schodowa góra 2", ImageUrl = "/media/img_3b9527cb.png" }, // ID: 29
                    new Waypoint { Name = "Klatka schodowa góra 3", ImageUrl = "/media/img_1e9bf0eb.png" }, // ID: 30
                    new Waypoint { Name = "Korytarz góra 1", ImageUrl = "/media/img_036bcfc8.png" }, // ID: 31
                    new Waypoint { Name = "Sala a26", ImageUrl = "/media/img_49f03ea1.png" }, // ID: 32
                    new Waypoint { Name = "Korytarz góra 2", ImageUrl = "/media/img_32ab6b95.png" }, // ID: 33
                    new Waypoint { Name = "Korytarz góra 3", ImageUrl = "/media/img_65827b22.png" }, // ID: 34
                    new Waypoint { Name = "Sekretariat kolegium studiów administracji i informatyki", ImageUrl = "/media/img_019a7fa7.png" }, // ID: 35
                    new Waypoint { Name = "Korytarz góra 4", ImageUrl = "/media/img_e82b997c.png" }, // ID: 36
                    new Waypoint { Name = "Laboratorium a27", ImageUrl = "/media/img_d64ab5b0.png" }, // ID: 37
                    new Waypoint { Name = "Dziekan kolegium studiów administracji i informatyki", ImageUrl = "/media/img_dd2b4b50.png" }, // ID: 38
                    new Waypoint { Name = "Korytarz góra 5", ImageUrl = "/media/img_5f79974a.png" }, // ID: 39
                    new Waypoint { Name = "Sekretariat kolegium studiów zarzadzania i bezpieczeństwa", ImageUrl = "/media/img_45368868.png" }, // ID: 40
                    new Waypoint { Name = "Korytarz góra 6", ImageUrl = "/media/img_b64df76f.png" }, // ID: 41
                    new Waypoint { Name = "Dziekan kolegium studiów zarzadzania i bezpieczeństwa", ImageUrl = "/media/img_17c4531b.png" }, // ID: 42
                    new Waypoint { Name = "Korytarz góra 7", ImageUrl = "/media/img_823151cf.png" }, // ID: 43
                    new Waypoint { Name = "Pokój wykladowców 29", ImageUrl = "/media/img_3dc986bd.png" }, // ID: 44
                    new Waypoint { Name = "Korytarz góra 8", ImageUrl = "/media/img_4bef29f2.png" }, // ID: 45
                    new Waypoint { Name = "Sala komputerowa a30", ImageUrl = "/media/img_a85a3748.png" }, // ID: 46
                    new Waypoint { Name = "Wnęka strefa relaksu góra", ImageUrl = "/media/img_f65e1ceb.png" }, // ID: 47
                    new Waypoint { Name = "Korytarz góra 9", ImageUrl = "/media/img_b080718b.png" }, // ID: 48
                    new Waypoint { Name = "Winda dla niepelnosprawnych góra", ImageUrl = "/media/img_90fe1d75.png" }, // ID: 49
                    new Waypoint { Name = "Dziekan kolegium studiów pedagogicznych i wychowania fizycznego", ImageUrl = "/media/img_abe262d3.png" }, // ID: 50
                    new Waypoint { Name = "Sala wykładowa a31", ImageUrl = "/media/img_59086308.png" }, // ID: 51
                    new Waypoint { Name = "Sekretariat kolegium studiów pedagogicznych i wychowania fizycznego", ImageUrl = "/media/img_1ea051c1.png" }, // ID: 52
                    new Waypoint { Name = "Korytarz góra 10", ImageUrl = "/media/img_6fe0ef49.png" }, // ID: 53
                    new Waypoint { Name = "Sala komputerowa a32", ImageUrl = "/media/img_8ae61cdc.png" }, // ID: 54
                    new Waypoint { Name = "Toalety góra", ImageUrl = "/media/img_ede21840.png" }, // ID: 55
                    new Waypoint { Name = "Sala komputerowa a33", ImageUrl = "/media/img_2b7b4c61.png" }, // ID: 56
                    new Waypoint { Name = "Korytarz góra 11", ImageUrl = "/media/img_7fc435c2.png" }, // ID: 57
                    new Waypoint { Name = "Laboratorium a34", ImageUrl = "/media/img_5467243a.png" }  // ID: 58
                };

                context.Waypoints.AddRange(waypoints);
                context.SaveChanges(); // Zapisujemy, żeby nadały się ID w bazie

                // 3. TWORZENIE POŁĄCZEŃ (Connections)
                // Używamy ID, które już znamy z Twojego JSONa.
                
                var connections = new Connection[]
                {
                    new Connection { SourceId = 1, TargetId = 2, Direction = "FORWARD" },
                    new Connection { SourceId = 2, TargetId = 1, Direction = "BACK" },
                    new Connection { SourceId = 2, TargetId = 3, Direction = "LEFT" },
                    new Connection { SourceId = 3, TargetId = 2, Direction = "BACK" },
                    new Connection { SourceId = 3, TargetId = 4, Direction = "LEFT" },
                    new Connection { SourceId = 4, TargetId = 3, Direction = "BACK" },
                    new Connection { SourceId = 3, TargetId = 5, Direction = "FORWARD" },
                    new Connection { SourceId = 5, TargetId = 3, Direction = "BACK" },
                    new Connection { SourceId = 5, TargetId = 6, Direction = "RIGHT" },
                    new Connection { SourceId = 6, TargetId = 5, Direction = "BACK" },
                    new Connection { SourceId = 5, TargetId = 7, Direction = "FORWARD" },
                    new Connection { SourceId = 7, TargetId = 8, Direction = "RIGHT" },
                    new Connection { SourceId = 8, TargetId = 7, Direction = "BACK" },
                    new Connection { SourceId = 7, TargetId = 9, Direction = "LEFT" },
                    new Connection { SourceId = 9, TargetId = 7, Direction = "BACK" },
                    new Connection { SourceId = 7, TargetId = 10, Direction = "FORWARD" },
                    new Connection { SourceId = 10, TargetId = 11, Direction = "LEFT" },
                    new Connection { SourceId = 11, TargetId = 10, Direction = "BACK" },
                    new Connection { SourceId = 10, TargetId = 12, Direction = "RIGHT" },
                    new Connection { SourceId = 12, TargetId = 10, Direction = "BACK" },
                    new Connection { SourceId = 10, TargetId = 13, Direction = "FORWARD" },
                    new Connection { SourceId = 13, TargetId = 10, Direction = "BACK" },
                    new Connection { SourceId = 13, TargetId = 14, Direction = "RIGHT" },
                    new Connection { SourceId = 14, TargetId = 13, Direction = "BACK" },
                    new Connection { SourceId = 13, TargetId = 15, Direction = "FORWARD" },
                    new Connection { SourceId = 15, TargetId = 13, Direction = "BACK" },
                    new Connection { SourceId = 15, TargetId = 16, Direction = "RIGHT" },
                    new Connection { SourceId = 16, TargetId = 15, Direction = "BACK" },
                    new Connection { SourceId = 15, TargetId = 17, Direction = "FORWARD" },
                    new Connection { SourceId = 17, TargetId = 15, Direction = "BACK" },
                    new Connection { SourceId = 17, TargetId = 18, Direction = "FORWARD" },
                    new Connection { SourceId = 18, TargetId = 17, Direction = "BACK" },
                    new Connection { SourceId = 18, TargetId = 19, Direction = "LEFT" },
                    new Connection { SourceId = 19, TargetId = 18, Direction = "BACK" },
                    new Connection { SourceId = 18, TargetId = 20, Direction = "RIGHT" },
                    new Connection { SourceId = 20, TargetId = 18, Direction = "BACK" },
                    new Connection { SourceId = 18, TargetId = 21, Direction = "FORWARD" },
                    new Connection { SourceId = 21, TargetId = 22, Direction = "LEFT" },
                    new Connection { SourceId = 22, TargetId = 21, Direction = "BACK" },
                    new Connection { SourceId = 21, TargetId = 23, Direction = "FORWARD" },
                    new Connection { SourceId = 23, TargetId = 21, Direction = "BACK" },
                    new Connection { SourceId = 23, TargetId = 24, Direction = "FORWARD" },
                    new Connection { SourceId = 24, TargetId = 23, Direction = "BACK" },
                    new Connection { SourceId = 24, TargetId = 25, Direction = "FORWARD" },
                    new Connection { SourceId = 25, TargetId = 24, Direction = "BACK" },
                    new Connection { SourceId = 25, TargetId = 26, Direction = "FORWARD" },
                    new Connection { SourceId = 26, TargetId = 25, Direction = "BACK" },
                    new Connection { SourceId = 26, TargetId = 27, Direction = "FORWARD" },
                    new Connection { SourceId = 27, TargetId = 26, Direction = "BACK" },
                    new Connection { SourceId = 3, TargetId = 28, Direction = "RIGHT" },
                    new Connection { SourceId = 28, TargetId = 3, Direction = "BACK" },
                    new Connection { SourceId = 28, TargetId = 29, Direction = "FORWARD" },
                    new Connection { SourceId = 29, TargetId = 28, Direction = "BACK" },
                    new Connection { SourceId = 29, TargetId = 30, Direction = "FORWARD" },
                    new Connection { SourceId = 30, TargetId = 29, Direction = "BACK" },
                    new Connection { SourceId = 30, TargetId = 31, Direction = "FORWARD" },
                    new Connection { SourceId = 31, TargetId = 32, Direction = "LEFT" },
                    new Connection { SourceId = 32, TargetId = 31, Direction = "BACK" },
                    new Connection { SourceId = 31, TargetId = 33, Direction = "FORWARD" },
                    new Connection { SourceId = 33, TargetId = 31, Direction = "BACK" },
                    new Connection { SourceId = 21, TargetId = 18, Direction = "BACK" },
                    new Connection { SourceId = 10, TargetId = 7, Direction = "BACK" },
                    new Connection { SourceId = 7, TargetId = 5, Direction = "BACK" },
                    new Connection { SourceId = 33, TargetId = 34, Direction = "FORWARD" },
                    new Connection { SourceId = 34, TargetId = 35, Direction = "RIGHT" },
                    new Connection { SourceId = 35, TargetId = 34, Direction = "BACK" },
                    new Connection { SourceId = 34, TargetId = 36, Direction = "FORWARD" },
                    new Connection { SourceId = 36, TargetId = 34, Direction = "BACK" },
                    new Connection { SourceId = 34, TargetId = 33, Direction = "BACK" },
                    new Connection { SourceId = 36, TargetId = 37, Direction = "LEFT" },
                    new Connection { SourceId = 37, TargetId = 36, Direction = "BACK" },
                    new Connection { SourceId = 36, TargetId = 38, Direction = "RIGHT" },
                    new Connection { SourceId = 38, TargetId = 36, Direction = "BACK" },
                    new Connection { SourceId = 36, TargetId = 39, Direction = "FORWARD" },
                    new Connection { SourceId = 39, TargetId = 36, Direction = "BACK" },
                    new Connection { SourceId = 39, TargetId = 40, Direction = "RIGHT" },
                    new Connection { SourceId = 40, TargetId = 39, Direction = "BACK" },
                    new Connection { SourceId = 39, TargetId = 41, Direction = "FORWARD" },
                    new Connection { SourceId = 41, TargetId = 39, Direction = "BACK" },
                    new Connection { SourceId = 41, TargetId = 42, Direction = "RIGHT" },
                    new Connection { SourceId = 42, TargetId = 41, Direction = "BACK" },
                    new Connection { SourceId = 41, TargetId = 43, Direction = "FORWARD" },
                    new Connection { SourceId = 43, TargetId = 41, Direction = "BACK" },
                    new Connection { SourceId = 43, TargetId = 44, Direction = "LEFT" },
                    new Connection { SourceId = 44, TargetId = 43, Direction = "BACK" },
                    new Connection { SourceId = 43, TargetId = 45, Direction = "FORWARD" },
                    new Connection { SourceId = 45, TargetId = 46, Direction = "LEFT" },
                    new Connection { SourceId = 46, TargetId = 45, Direction = "BACK" },
                    new Connection { SourceId = 45, TargetId = 47, Direction = "RIGHT" },
                    new Connection { SourceId = 47, TargetId = 45, Direction = "BACK" },
                    new Connection { SourceId = 45, TargetId = 48, Direction = "FORWARD" },
                    new Connection { SourceId = 48, TargetId = 45, Direction = "BACK" },
                    new Connection { SourceId = 48, TargetId = 49, Direction = "LEFT" },
                    new Connection { SourceId = 49, TargetId = 48, Direction = "BACK" },
                    new Connection { SourceId = 48, TargetId = 50, Direction = "FORWARD" },
                    new Connection { SourceId = 50, TargetId = 48, Direction = "BACK" },
                    new Connection { SourceId = 50, TargetId = 51, Direction = "LEFT" },
                    new Connection { SourceId = 51, TargetId = 50, Direction = "BACK" },
                    new Connection { SourceId = 50, TargetId = 52, Direction = "RIGHT" },
                    new Connection { SourceId = 52, TargetId = 50, Direction = "BACK" },
                    new Connection { SourceId = 50, TargetId = 53, Direction = "FORWARD" },
                    new Connection { SourceId = 53, TargetId = 50, Direction = "BACK" },
                    new Connection { SourceId = 53, TargetId = 54, Direction = "LEFT" },
                    new Connection { SourceId = 54, TargetId = 53, Direction = "BACK" },
                    new Connection { SourceId = 53, TargetId = 55, Direction = "FORWARD" },
                    new Connection { SourceId = 55, TargetId = 53, Direction = "BACK" },
                    new Connection { SourceId = 55, TargetId = 56, Direction = "LEFT" },
                    new Connection { SourceId = 56, TargetId = 55, Direction = "BACK" },
                    new Connection { SourceId = 55, TargetId = 57, Direction = "FORWARD" },
                    new Connection { SourceId = 57, TargetId = 58, Direction = "LEFT" },
                    new Connection { SourceId = 58, TargetId = 57, Direction = "BACK" },
                    new Connection { SourceId = 45, TargetId = 43, Direction = "BACK" },
                    new Connection { SourceId = 31, TargetId = 30, Direction = "BACK" }
                };

                context.Connections.AddRange(connections);
                context.SaveChanges();
            }
        }
    }
}