using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gateway.DTO;
using Gateway.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Lab2Tests
{
    internal class Builder
    {
        public static PaginationResponse<IEnumerable<Hotels>>? BuildHotelsPages(int? page, int? size)
        {
            var hotels = new List<Hotels>();
            hotels.Add(new Hotels()
            {
                Id = 1,
                HotelUid = Guid.NewGuid(),
                Address = "Госпитальная набережная, д.2",
                City = "Москва",
                Country = "Россия",
                Name = "Физра у Брызгалова",
                Price = 10000,
                Stars = 1
            });

            hotels.Add(new Hotels()
            {
                Id = 2,
                HotelUid = Guid.NewGuid(),
                Address = "Измайловский пр., 73А",
                City = "Москва",
                Country = "Россия",
                Name = "Капибария",
                Price = 20000,
                Stars = 5
            });

            hotels.Add(new Hotels()
            {
                Id = 3,
                HotelUid = Guid.NewGuid(),
                Address = "Улица Пушкина, д. Колотушкина",
                City = "в Небыляндии",
                Country = "Где-то",
                Name = "Прилети Сова",
                Price = 15000,
                Stars = 4
            });

            var total = hotels.Count;

            var response = new PaginationResponse<IEnumerable<Hotels>>()
            {
                Page = page.Value,
                PageSize = size.Value,
                Items = hotels,
                TotalElements = total
            };

            return response;
        }


        public static Hotels? BuildHotelByUid(Guid guid)
        {
            var hotel = new Hotels()
            {
                Id = 2,
                HotelUid = guid,
                Address = "Измайловский пр., 73А",
                City = "Москва",
                Country = "Россия",
                Name = "Капибария",
                Price = 20000,
                Stars = 5
            };
            return hotel;
        }

        public static Hotels? BuildHotelById(int id)
        {
            Hotels hotel;
            switch (id)
            {
                case 1:
                    hotel = new Hotels()
                    {
                        Id = 1,
                        HotelUid = Guid.NewGuid(),
                        Address = "Госпитальная набережная, д.2",
                        City = "Москва",
                        Country = "Россия",
                        Name = "Физра у Брызгалова",
                        Price = 10000,
                        Stars = 1
                    };
                    break;
                case 2:
                    hotel = new Hotels()
                    {
                        Id = 2,
                        HotelUid = Guid.NewGuid(),
                        Address = "Измайловский пр., 73А",
                        City = "Москва",
                        Country = "Россия",
                        Name = "Капибария",
                        Price = 20000,
                        Stars = 5
                    };
                    break;
                default:
                    hotel = new Hotels()
                    {
                        Id = 3,
                        HotelUid = Guid.NewGuid(),
                        Address = "Улица Пушкина, д. Колотушкина",
                        City = "в Небыляндии",
                        Country = "Где-то",
                        Name = "Прилети Сова",
                        Price = 15000,
                        Stars = 4
                    };
                    break;
            }
            return hotel;
        }

        public static IEnumerable<Reservation>? BuildReservationsList(string username)
        {
            var reservations = new List<Reservation>();
            reservations.Add(new Reservation()
            {
                Id = 1,
                ReservationUid = Guid.NewGuid(),
                Username = username,
                PaymentUid = System.Guid.Empty,
                HotelId = 1,
                Status = "PAID",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
            });

            reservations.Add(new Reservation()
            {
                Id = 2,
                ReservationUid = Guid.NewGuid(),
                Username = username,
                PaymentUid = System.Guid.Empty,
                HotelId = 2,
                Status = "PAID",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(4),
            });
            return reservations;
        }

        public static Reservation? BuildReservationRequest()
        {
            var reservations = new Reservation()
            {
                PaymentUid = System.Guid.Empty,
                HotelId = 2,
                EndDate = DateTime.Now.AddDays(2),
                StartDate = DateTime.Now,
            };
            return reservations;
        }

        public static CreateReservationRequest? BuildReservationRequestMessage()
        {
            var reservations = new CreateReservationRequest()
            {
                HotelUid = System.Guid.Empty,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
    };
            return reservations;
        }

        public static Reservation? BuildReservationResponse()
        {
            var reservations = new Reservation()
            {
                Id = 1,
                ReservationUid = System.Guid.Empty,
                Username = "TestUsername",
                PaymentUid = System.Guid.Empty,
                HotelId = 2,
                Status = "PAID",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
            };
            return reservations;
        }

        public static Reservation? BuildReservationByGuid(Guid guid)
        {
            var reservaton = new Reservation()
            {
                Id = 1,
                ReservationUid = guid,
                Username = "TestUsername",
                PaymentUid = System.Guid.Empty,
                HotelId = 1,
                Status = "PAID",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2),
            };
            return reservaton;
        }

            public static Payment? BuildPaymentByUId(Guid guid)
        {
            Payment payment;

            payment = new Payment()
            {
                Id = 1,
                PaymentUid = guid,
                Status = "PAID",
                Price = 10000,
            };

            return payment;
        }

        public static Payment? BuildPaymentRequest(int sum)
        {
            Payment paymentRequest = new Payment()
            {
                Id = 1,
                Price = sum,
                PaymentUid = System.Guid.Empty,
                Status = "PAID",
            };
            return paymentRequest;
        }

        public static Payment? BuildPaymentResponse(int sum)
        {
            Payment paymentRequest = new Payment()
            {
                Id = 1,
                Price = sum,
                PaymentUid = System.Guid.Empty,
                Status = "PAID",
            };
            return paymentRequest;
        }

        public static Loyalty? BuildLoyaltyByUsername(string username)
        {
            Loyalty loyalty;

            loyalty = new Loyalty()
            {
                Id = 1,
                Username = username,
                Status = "GOLD",
                ReservationCount = 26,
                Discount = 10,
            };
            return loyalty;
        }
    }
}
