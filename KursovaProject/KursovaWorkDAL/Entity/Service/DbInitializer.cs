using KursovaWorkDAL.Entity.Entities.Car;
using Microsoft.Extensions.Logging;

namespace KursovaWorkDAL.Entity.Service
{
    /// <summary>
    /// Клас для ініціалізації бази даних автомобільного продажу.
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Об'єкт класу ILogger для логування подій 
        /// </summary>
        /// 
        private static readonly ILogger _logger = LoggerFactory.Create(builder => builder.AddConsole())
            .CreateLogger(typeof(DbInitializer));

        /// <summary>
        /// Ініціалізує базу даних автомобільного продажу.
        /// </summary>
        public static void Initialize(CarSaleContext context)
        {
            context.Database.EnsureCreated();

            _logger.LogInformation("Успішно перевірино чи база даних є створена");

            if (context.Cars.Any())
            {
                _logger.LogInformation("В базі даних уже є автомобілі");
                return; 
            }

            _logger.LogInformation("В базі даних немає автомобілів");

            var carInfos = new List<CarInfo>
            {
                new CarInfo
                {
                    Make = "Volkswagen",
                    Model = "Arteon",
                    Year = 2022,
                    Price = 1260000,
                    Description = "Розкішний седан з передовими функціями",
                    Detail = new CarDetail
                    {
                        Color = "Чорний",
                        Transmission = "Автоматична",
                        FuelType = "Бензин"
                    },
                    Images = new List<CarImage>()
                    {
                        new CarImage()
                        {
                            ImageUrl = "https://cdn.motor1.com/images/mgl/yK3PG/s3/2020-vw-arteon-r-line-edition.jpg"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://i.ytimg.com/vi/oAIervGLG9Q/maxresdefault.jpg"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://cdn.motor1.com/images/mgl/JPV9A/s3/2020-vw-arteon-r-line-edition.webp"
                        }
                    }
                },
                new CarInfo
                {
                    Make = "Porsche",
                    Model = "Taycan",
                    Year = 2023,
                    Price = 3360000,
                    Description = "Електромобіль-спорткар з видовищною продуктивністю",
                    Detail = new CarDetail
                    {
                        Color = "Білий",
                        Transmission = "Автоматична",
                        FuelType = "Електричний"
                    },
                    Images = new List<CarImage>()
                    {
                        new CarImage()
                        {
                            ImageUrl = "https://cdn.motor1.com/images/mgl/8ww1J/s3/2021-porsche-taycan-turbo-s.jpg"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://cdn.motor1.com/images/mgl/QEmQB/s3/2020-porsche-taycan.jpg"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://gaadiwaadi.com/wp-content/uploads/2020/04/Porche-taycan2-1280x720.jpg"
                        }
                    }
                },
                new CarInfo
                {
                    Make = "Audi",
                    Model = "Q8",
                    Year = 2021,
                    Price = 2100000,
                    Description = "Стрункий і просторий SUV для найвищого комфорту",
                    Detail = new CarDetail
                    {
                        Color = "Білий",
                        Transmission = "Автоматична",
                        FuelType = "Дизель"
                    },
                    Images = new List<CarImage>()
                    {
                        new CarImage()
                        {
                            ImageUrl = "https://uzr.com.ua/wp-content/uploads/2018/08/Audi-Q8-8.jpg"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://cdn.car-recalls.eu/wp-content/uploads/2020/07/Audi-Q8-2020-automatic-gearbox-oil-leak.jpg"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://pictures.dealer.com/a/audisanjuan1001eexpressway83aoa/0618/0112a064facce92a80125844c7371be2x.jpg"
                        }
                    }
                },
                new CarInfo
                {
                    Make = "Volkswagen",
                    Model = "Golf",
                    Year = 2022,
                    Price = 784000,
                    Description = "Універсальний хетчбек з високою паливною ефективністю",
                    Detail = new CarDetail
                    {
                        Color = "Синій",
                        Transmission = "Механічна",
                        FuelType = "Бензин"
                    },
                    Images = new List<CarImage>()
                    {
                        new CarImage()
                        {
                            ImageUrl = "https://i.ytimg.com/vi/JtIuMVXBYlY/maxresdefault.jpg"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://a.d-cd.net/To5JJbC-UBCTAHadHDFZ7Zu4PEM-1920.jpg"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://autoua.net/media/uploads2/volkswagen/volkswagen-golf_r-2022-1280-20.jpg"
                        }
                    }
                },
                new CarInfo
                {
                    Make = "Lamborghini",
                    Model = "Huracan",
                    Year = 2023,
                    Price = 8400000,
                    Description = "Неймовірний суперкар з культовим дизайном",
                    Detail = new CarDetail
                    {
                        Color = "Червоний",
                        Transmission = "Автоматична",
                        FuelType = "Бензин"
                    },
                    Images = new List<CarImage>()
                    {
                        new CarImage()
                        {
                            ImageUrl = "https://i.ytimg.com/vi/7IQCZxi7Mwk/maxresdefault.jpg"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://renty.ae/cdn-cgi/image/format=auto,fit=contain/https://renty.ae/uploads/car/photos/l/red_lamborghini-evo-spyder_2021_7856_abe1ff80b1f4cd65bf3347c209736e48.jpg"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://www.topgear.com/sites/default/files/cars-car/image/2019/01/tom_5734.jpg?w=1280&h=720"
                        }
                    }
                },
                new CarInfo
                {
                    Make = "Audi",
                    Model = "A4",
                    Year = 2022,
                    Price = 1120000,
                    Description = "Елегантний і спортивний седан для щоденних поїздок",
                    Detail = new CarDetail
                    {
                        Color = "Сірий",
                        Transmission = "Автоматична",
                        FuelType = "Бензин"
                    },
                    Images = new List<CarImage>()
                    {
                        new CarImage()
                        {
                            ImageUrl = "https://img.automoto.ua/thumb/1280-720/d/3/2/f/1/b/audi-a4-2022.jpg?url=aHR0cHM6Ly9hdXRvbW90by51YS91cGxvYWRzL2ZpbGUvM2IvYzkvYmEvZGYvQTQuanBn"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://i.ytimg.com/vi/y09RWxyx288/maxresdefault.jpg?sqp=-oaymwEmCIAKENAF8quKqQMa8AEB-AH-CYAC0AWKAgwIABABGFsgWyhbMA8=&rs=AOn4CLALycU5RW9apLZElHc2_e7H8y0yMg"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://www.autopediame.com/storage/images/Audi/A4%202023/a4%20exterior.jpg"
                        }
                    }
                },
                new CarInfo
                {
                    Make = "Lamborghini",
                    Model = "Aventador",
                    Year = 2023,
                    Price = 14000000,
                    Description = "Легендарний гіперкар з захоплююючою продуктивністю",
                    Detail = new CarDetail
                    {
                        Color = "Жовтий",
                        Transmission = "Автоматична",
                        FuelType = "Бензин"
                    },
                    Images = new List<CarImage>()
                    {
                        new CarImage()
                        {
                            ImageUrl = "https://i.ytimg.com/vi/2RKG2HqRy8U/maxresdefault.jpg"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://i.ytimg.com/vi/RWNpWW8MZUM/maxresdefault.jpg"
                        },
                        new CarImage()
                        {
                            ImageUrl = "https://i0.wp.com/namastecar.com/wp-content/uploads/2022/06/Lamborghini-Aventador-LP-780-4-Ultimae-Roadster-2022-video-review-specs-details-in-Hindi-1.jpeg?ssl=1"
                        }
                    }
                }
            };

            _logger.LogInformation("Додання кожного автомобіля");
            foreach (var carInfo in carInfos)
            {
                context.Cars.Add(carInfo);
            }

            context.SaveChanges();

            _logger.LogInformation("Автомобілі були успішно додані");
        }
    }

}
