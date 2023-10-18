using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EntityConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("E:\\Лабораторные работы\\ASP.NET\\2\\EntityConsole\\EntityConsole\\appsettings.json");
            var config = builder.Build();
            string connectionstring = config.GetConnectionString("DefaultConnection");

            var optionsbuilder = new DbContextOptionsBuilder<VaccinationsDbContext>();
            var options = optionsbuilder
                .UseSqlServer(connectionstring)
                .Options;

            using (VaccinationsDbContext db = new VaccinationsDbContext(options))
            {
                string? f = "";

                while (f != null)
                {
                    Console.Clear();
                    Console.Beep();
                    Console.WriteLine("Select task (1 - 10) ...");
                    f = Console.ReadLine();

                    switch (f)
                    {
                        case "1": SelectOneSide(db); break;
                        case "2":
                            Console.WriteLine("Enter patient Sex");
                            string? sex = Console.ReadLine();
                            SelectOneSide(db, sex); break;
                        case "3":
                            Console.WriteLine("Enter patient Sex");
                            string? sexC = Console.ReadLine();
                            SelectManySideCount(db, sexC); break;
                        case "4": SelectOneToMany(db); break;
                        case "5":
                            Console.WriteLine("Enter disease code");
                            int code = int.Parse(Console.ReadLine());
                            SelectOneToMany(db, code); break;
                        case "6":
                            Console.WriteLine("Enter name and adress");
                            string? name = Console.ReadLine();
                            string? adress = Console.ReadLine();
                            AddMedInst(db, name, adress); break;
                        case "7":
                            Console.WriteLine("Enter diseaseId, description and manufacturer");
                            int? diseaseId = int.Parse(Console.ReadLine());
                            string? desc = Console.ReadLine();
                            string? manufac = Console.ReadLine();
                            AddVaccine(db, diseaseId, desc, manufac); break;
                        case "8":
                            Console.WriteLine("Enter medical institute Id");
                            int instId = int.Parse(Console.ReadLine());
                            DeleteMedInst(db, instId); break;
                        case "9":
                            Console.WriteLine("Enter vaccine Id");
                            int vacId = int.Parse(Console.ReadLine());
                            DeleteVaccine(db, vacId); break;
                        case "10":
                            Console.WriteLine("Enter doctor data");
                            string doctor = Console.ReadLine();
                            UpdateMessages(db, doctor); break;

                        default: f = null; break;
                    }

                    Console.ReadLine();
                }
            }
        }

        public static void SelectOneSide(VaccinationsDbContext db)
        {
            var patients = db.Patients.ToList();
            Console.WriteLine("Список объектов:");
            foreach (Patient p in patients)
                Console.WriteLine($"Id: {p.PatientId}, FIO: {p.Fio}, Sex: {p.Sex}, Pasport: {p.Pasport}, Adress: {p.Adress}");
        }

        public static void SelectOneSide(VaccinationsDbContext db, string? sex)
        {
            var patients = db.Patients.Where(p => p.Sex == sex).ToList();
            Console.WriteLine("Список объектов:");
            foreach (Patient p in patients)
                Console.WriteLine($"Id: {p.PatientId}, FIO: {p.Fio}, Sex: {p.Sex}, Pasport: {p.Pasport}, Adress: {p.Adress}");
        }

        public static void SelectManySideCount(VaccinationsDbContext db, string? sex)
        {
            var query = from p in db.Patients
                        join v in db.Vaccinations
                        on p.PatientId equals v.PatientId
                        where p.Sex == sex
                        group v by p.Sex into ps
                        select new
                        {
                            Количество_прививок = ps.Count(),
                        };

            Console.WriteLine(query.ToList()[0]);
        }

        public static void SelectOneToMany(VaccinationsDbContext db)
        {
            var query = from v in db.Vaccines
                        join d in db.Diseases
                        on v.DiseaseId equals d.DiseaseId
                        orderby v.VaccineId
                        select new
                        {
                           Код_вакцины = v.VaccineId,
                           Описание = v.Description,
                           От_заболевания = d.Name,
                        };

            foreach (var p in query.ToList()) Console.WriteLine(p);
        }

        public static void SelectOneToMany(VaccinationsDbContext db, int code)
        {
            var query = from v in db.Vaccines
                        join d in db.Diseases
                        on v.DiseaseId equals d.DiseaseId
                        where d.Code == code
                        orderby v.VaccineId
                        select new
                        {
                            Код_вакцины = v.VaccineId,
                            Описание = v.Description,
                            От_заболевания = d.Name,
                        };

            foreach (var p in query.ToList()) Console.WriteLine(p);
        }

        public static void AddMedInst(VaccinationsDbContext db, string? name, string? adress)
        {
            MedicalInstitution mi = new MedicalInstitution()
            {
                Name = name,
                Adress = adress
            };

            try
            {
                db.MedicalInstitutions.Add(mi);
                db.SaveChanges();
                Console.WriteLine("Successfully added!");
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        public static void AddVaccine(VaccinationsDbContext db, int? diseaseId, string? desc, string? manufacturer)
        {
            Vaccine vaccine = new Vaccine()
            {
                DiseaseId = diseaseId,
                Description = desc,
                Manufacturer = manufacturer
            };

            try
            {
                db.Vaccines.Add(vaccine);
                db.SaveChanges();
                Console.WriteLine("Successfully added!");
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        public static void DeleteMedInst(VaccinationsDbContext db, int medInstId)
        {
            try
            {
                var inst = db.MedicalInstitutions.FirstOrDefault(x => x.MedicalInstitutionId == medInstId);
                db.MedicalInstitutions.Remove(inst);
                db.SaveChanges();
                Console.WriteLine("Successfully deleted!");
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        public static void DeleteVaccine(VaccinationsDbContext db, int vacId)
        {
            try
            {
                var vac = db.Vaccines.FirstOrDefault(x => x.VaccineId == vacId);
                db.Vaccines.Remove(vac);
                db.SaveChanges();
                Console.WriteLine("Successfully deleted!");
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        public static void UpdateMessages(VaccinationsDbContext db, string doctor)
        {
            try
            {
                var messages = db.MessagesAfterVaccinations.Where(x => x.Doctor == doctor).ToList();
                foreach (MessagesAfterVaccination message in messages) message.Recommendations = "--Classified--";
                db.SaveChanges();
                Console.WriteLine("Successfully classified!");
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
    }
}
