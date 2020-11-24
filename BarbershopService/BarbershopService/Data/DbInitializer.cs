using BarbershopService.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarbershopService.Data
{
    public class DbInitializer
    {
        private static Random randObj = new Random(1);

        public static void Initialize(BarbershopContext db)
        {
            db.Database.EnsureCreated();

            int clientCount = 100;
            int reviewsCount = 10000;
            int serviceCount = 100;
            // Виды услуг и сотрудники создаются статически

            EmployeeGenerate(db);
            ServiceTypeGenerate(db);
            ClientGenerate(db, clientCount);
            ServiceGenerate(db, serviceCount);
            ReviewGenerate(db, reviewsCount);

        }

        private static void EmployeeGenerate(BarbershopContext db)
        {
            if (db.Employees.Any())
            {
                return;
            }
            db.Employees.AddRange(
                new Employee[]
                {
                    new Employee() 
                    {
                        FullName = "Липский Д.Ю.",
                        Experience = 1,
                        Address = "ул. Полесская, 10"
                    },
                    new Employee()
                    {
                        FullName = "Ропрот И.В.",
                        Experience = 2,
                        Address = "ул. Интерноциональная, 14"
                    },
                    new Employee()
                    {
                        FullName = "Литовских Л.В.",
                        Experience = 4,
                        Address = "ул. Интерноциональная, 54"
                    },
                    new Employee()
                    {
                        FullName = "Белько Э.А.",
                        Experience = 6,
                        Address = "ул. Интерноциональная, 10"
                    },
                    new Employee()
                    {
                        FullName = "Семёнов Д.С.",
                        Experience = 1,
                        Address = "ул. Гастело, 3"
                    },
                    new Employee() // 6
                    {
                        FullName = "Стольный С.В.",
                        Experience = 1,
                        Address = "ул. Горького, 14"
                    }
                }
            );
            db.SaveChanges();
        }

        private static void ServiceTypeGenerate(BarbershopContext db)
        {
            if (db.ServiceTypes.Any())
            {
                return;
            }
            db.ServiceTypes.AddRange(new ServiceType[]
            {
                new ServiceType()
                {
                    Name = "Стрижка полубокс",
                    Description = "Включительно обработка лаком (по желанию) и феном"
                },
                new ServiceType()
                {
                    Name = "Стрижка на заказ",
                    Description = "Парикмахер воплотит стрижку с любой фотографии или описания"
                },
                new ServiceType()
                {
                    Name = "Стрижка от стилиста",
                    Description = "Стилист сам подберёт стрижку, наиболее подходящую"
                },
                new ServiceType()   // 4
                {
                    Name = "Налысо",
                    Description = "Корректировки приветствуются"
                }
            });
        }

        private static void ClientGenerate(BarbershopContext db, int count)
        {
            if (db.Clients.Any())
            {
                return;
            }

            string fullName;
            string address;
            string phoneNumber;
            double discount;

            string[] fullNamesVoc = { "Жмайлик А.В.", "Сетко А.И.", "Семёнов С.А.", "Давыдчик А.Е.", "Пискун Е.А.",
                                  "Дракула В.А.", "Ястребов А.А.", "Степаненко Ю.А.", "Башаримов Ю.И.", "Каркозов В.В." };

            string[] addressVoc = {"пер.Заслонова, ", "ул.Гастело, ", "ул.Полесская, ", "пр.Речецкий, ", "ул, Интерноциональная, ",
                                    "пр.Октября, ", "ул.Бассейная, ", "бул.Юности, " };

            for (int i = 0; i < count; i++)
            {
                fullName = fullNamesVoc[randObj.Next(fullNamesVoc.GetLength(0))] + randObj.Next(count);
                address = addressVoc[randObj.Next(addressVoc.GetLength(0))] + randObj.Next(count);
                phoneNumber = "+375 (29) " + randObj.Next(100, 999) + "-" + randObj.Next(10, 99) +
                              "-" + randObj.Next(10, 99);
                discount = randObj.NextDouble();

                db.Clients.Add(new Client()
                {
                    FullName = fullName,
                    Address = address,
                    PhoneNumber = phoneNumber,
                    Discount = Math.Round(discount, 3)
                });
            }

            db.SaveChanges();
        }

        private static void ServiceGenerate(BarbershopContext db, int count)
        {
            if (db.Services.Any())
            {
                return;
            }

            string[] descriptionVoc = { "Описание отсутствует", "Услуга оказана успешно", "Клиент остался доволен стрижкой",
                                    "Скидка 15%", "Для данной услуги скидка отсутствует"};

            int clientCount = db.Clients.Count();
            int serviceTypeCount = db.ServiceTypes.Count();
            int employeeCount = db.Employees.Count();


            for (int i = 0; i < count; i++)
            {
                var dateService = DateTime.Now.AddDays(-randObj.Next(1000));
                var description = descriptionVoc[randObj.Next(descriptionVoc.GetLength(0))] + " " + randObj.Next(count);
                var price = Convert.ToDecimal(randObj.NextDouble() + randObj.Next(100));
                var clientId = randObj.Next(1, clientCount - 1);
                var serviceTypeId = randObj.Next(1, serviceTypeCount - 1);
                var employeeId = randObj.Next(1, employeeCount - 1);

                db.Services.Add(new Service()
                {
                    DateService = dateService,
                    Description = description,
                    Price = price, 
                    ClientId = clientId,
                    ServiceTypeId = serviceTypeId,
                    EmployeeId = employeeId
                });
            }
            db.SaveChanges();
        }

        public static void ReviewGenerate(BarbershopContext db, int count)
        {
            if (db.Reviews.Any())
            {
                return;
            }

            int serviceTypeCount = db.ServiceTypes.Count();
            int clientCount = db.Clients.Count();

            for (int i = 0; i < count; i++)
            {
                var clientMark = randObj.Next(1, 5);
                var serviceTypeId = randObj.Next(1, serviceTypeCount - 1);
                var clientId = randObj.Next(1, clientCount - 1);


                db.Reviews.Add(new Review()
                {
                    ClientMark = clientMark,
                    ServiceTypeId = serviceTypeId,
                    ClientId = clientId
                });
            }

            db.SaveChanges();
        }
    }
}
