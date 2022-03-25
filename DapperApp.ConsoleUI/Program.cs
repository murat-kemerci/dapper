using DapperApp.Library1.Models;
using DapperApp.Library1.Queries;
using System;

namespace DapperApp.ConsoleUI
{
    internal class Program
    {
        static PersonQueries personQueries = new PersonQueries();
        static bool EndProgram = true;
        static int PersonId;
        static void Main(string[] args)
        {
            //https://github.com/btkacademy/csharp-basic
            //https://github.com/btkacademy/design-patterns
            //https://www.learndapper.com
            do
            {
                Console.WriteLine("| 1: Kişi Listesi | 2: Kişi Bul | 3: Kişi Ekle |4: Kişi Güncelle |\n| 5: Kişi Sil  | 6 : Filtreleme | 7 : Programı Sonlandır |");
                Console.Write("| İşlem Seçin:");
                string choosed = Console.ReadLine();
                Console.WriteLine("|----------------------------------------------------------------------------------------------|");

                if (choosed == "1")
                    WritePersonList();
                else if (choosed == "2")
                {
                    Console.Write("Kişinin Id'si: ");
                    try
                    {
                        PersonId = Convert.ToInt32(Console.ReadLine());
                        FindPerson(PersonId);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Sayı girişi yapın!");
                    }
                }
                else if (choosed == "3")
                    CreatePerson();
                else if (choosed == "4")
                    UpdatePerson();
                else if (choosed == "5")
                    DeletePerson();
                else if (choosed == "6")
                    FilterPerson();
                else if (choosed == "7")
                    EndProgram = false;
                else
                    Console.WriteLine("Yanlış Seçim, Tekrar Deneyin.");

            } while (EndProgram);

        }

        static void CreatePerson()
        {
            Person person = new Person();
            Console.Write("Adı: ");
            person.Name = Console.ReadLine();
            Console.Write("Soyadı: ");
            person.Surname = Console.ReadLine();
            personQueries.CreatePerson(person);
            Console.WriteLine("Kayıt eklendi id = {0}", person.Id);
        }
        static void WritePersonList()
        {
            var persons = personQueries.GetPersons();
            foreach (var person in persons)
            {
                Console.WriteLine("Id = {0}, Adı = {1}, Soyad = {2}",
                    person.Id,
                    person.Name,
                    person.Surname);
            }
        }

        //Update, Delete methodları yazılacak
        //Find
        static void FindPerson(int PersonId)
        {
            try
            {
                var Person = personQueries.FindPerson(PersonId);
                foreach (var person in Person)
                {
                    Console.WriteLine("Id = {0}, Adı = {1}, Soyad = {2}",
                        person.Id,
                        person.Name,
                        person.Surname);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Kişi bulunamadı");
            }

        }

        //Update
        static void UpdatePerson()
        {
            Console.Write("Güncellenecek Kişinin Id'si: ");
            try
            {
            PersonId = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Yanlış Giriş!");
            }
            Console.Write("Yeni Ad: ");
            string newName = Console.ReadLine();
            Console.Write("Yeni Soyad: ");
            string newSurname = Console.ReadLine();
            personQueries.UpdatePerson(PersonId, newName, newSurname);

            do
            {
                Console.Write("Kişiyi görüntülemek ister misiniz?: (y/n)");
                string ans = Console.ReadLine().ToLower();
                if (ans == "y")
                {
                    FindPerson(PersonId);
                    EndProgram = false;
                }
                else if (ans == "n")
                    EndProgram = false;
                else
                    Console.WriteLine("Yanlış Seçim, Tekrar Deneyin.");
            } while (EndProgram);
            EndProgram = true;
        }
        //Delete
        static void DeletePerson()
        {
            Console.Write("Silinicek Kişinin Id'si: ");
            PersonId = Convert.ToInt32(Console.ReadLine());
            FindPerson(PersonId);
            do
            {
                Console.Write("Kişiyi silemek istediğinize emin misiniz?: (y/n)");
                string ans = Console.ReadLine().ToLower();
                if (ans == "y")
                {
                    personQueries.DeletePerson(PersonId);
                    EndProgram = false;
                }
                else if (ans == "n")
                {
                    Console.WriteLine("İşem İptal Edildi.");
                    EndProgram = false;
                }
                else
                    Console.WriteLine("Yanlış Seçim, Tekrar Deneyin.");
            } while (EndProgram);
            EndProgram = true;

        }

        //Filtreli listeleme yapılabilir (Adı x ile başlayanları listelesin gibi ...)
        static void FilterPerson()
        {
            string table;
            string key;
            do
            {
                Console.Write("Filtreleme türü (1: Ad | 2: Soyad): ");
                string choosed = Console.ReadLine();
                if (choosed == "1")
                {
                    table = "Name";
                    Console.Write("Aranacak kelime/harf: ");
                    key = Console.ReadLine();
                    var Person = personQueries.FilterPersons(table, key);
                    if (Person.Count != 0)
                    {
                        foreach (var person in Person)
                        {
                            Console.WriteLine("Id = {0}, Adı = {1}, Soyad = {2}",
                                person.Id,
                                person.Name,
                                person.Surname);
                        }
                    }
                    else
                        Console.WriteLine("Kayıt Bulunamadı");
                    EndProgram = false;
                }
                else if (choosed == "2")
                {
                    table = "Surname";
                    Console.Write("Aranacak kelime/harf: ");
                    key = Console.ReadLine();
                    var Person = personQueries.FilterPersons(table, key);
                    if (Person.Count != 0)
                    {
                        foreach (var person in Person)
                        {
                            Console.WriteLine("Id = {0}, Adı = {1}, Soyad = {2}",
                                person.Id,
                                person.Name,
                                person.Surname);
                        }
                    }
                    else
                        Console.WriteLine("Kayıt Bulunamadı");
                    EndProgram = false;
                }
                else
                {
                    Console.WriteLine("Yanlış giriş!");
                }
            } while (EndProgram);
            EndProgram = true;
        }
    }
}
