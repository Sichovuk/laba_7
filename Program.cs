using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_7
{
    public class Phone : IComparable
    {
        public string Model { get; set; }
        public string Diagonal { get; set; }
        public int Cores { get; set; }
        public string Platform { get; set; }
        public int SimQuantity { get; set; }
        public bool HasAI { get; set; }
        public bool HasTypeC { get; set; }

        public Phone(string model, string diagonal, int cores, string platform, int simQuantity, bool hasAI, bool hasTypeC)
        {
            Model = model;
            Diagonal = diagonal;
            Cores = cores;
            Platform = platform;
            SimQuantity = simQuantity;
            HasAI = hasAI;
            HasTypeC = hasTypeC;
        }

        public Phone()
        {
        }

    public string Info()
        {
            return Model + ", " + Diagonal + ", " + Cores;
        }

        public int CompareTo(object obj)
        {
            Phone t = obj as Phone;
            return string.Compare(this.Model, t.Model);
        }
    }
    internal class Program
    {
        static List<Phone> phones = new List<Phone>();

        static void PrintPhones()
        {
            foreach (Phone phone in phones) 
            {
                Console.WriteLine(phone.Info().Replace('і', 'i'));
            }
        }

        static void Main(string[] args)
        {
            phones = new List<Phone>();
            FileStream fs = new FileStream("try1.phones", FileMode.Open);
            BinaryReader reader = new BinaryReader(fs);

            try
            {
                Phone phone;
                Console.WriteLine("      Читаємо данi з файлу...\n");

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    phone = new Phone();
                    for (int i = 0; i <= 6; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                phone.Model = reader.ReadString();
                                break;
                            case 1:
                                phone.Diagonal = reader.ReadString();
                                break;
                            case 2:
                                phone.Cores = reader.ReadInt32();
                                break;
                            case 3:
                                phone.Platform = reader.ReadString();
                                break;
                            case 4:
                                phone.SimQuantity = reader.ReadInt32();
                                break;
                            case 5:
                                phone.HasAI = reader.ReadBoolean();
                                break;
                            case 6:
                                phone.HasTypeC = reader.ReadBoolean();
                                break;
                        }
                    }
                    phones.Add(phone);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Сталась помилка: {0}", ex.Message);
            }
            finally
            {
                reader.Close();
            }
            Console.WriteLine("     Несортований перелiк телефонiв: {0}", phones.Count);
            PrintPhones();
            phones.Sort();
            Console.WriteLine("     Сортований перелiк телефонiв: {0}", phones.Count);
            PrintPhones();
            Console.WriteLine("     Додаємо новий запис: Власний телефон");
            Phone phonePOCO = new Phone("POCO X3 pro", "1600x720", 8, "Android", 3, true, true);
            phones.Add(phonePOCO);
            phones.Sort();
            Console.WriteLine("     Перелiк телефонiв: {0}", phones.Count);
            PrintPhones();
            Console.WriteLine("     Видаляємо останнє значення");
            phones.RemoveAt(phones.Count - 1);
            Console.WriteLine("     Перелiк телефонiв: {0}", phones.Count);
            PrintPhones();
            Console.ReadKey();
        }
    }
}
