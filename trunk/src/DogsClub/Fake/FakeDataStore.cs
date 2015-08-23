using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsClub.Fake
{
    public class FakeDataStore
    {
        private static List<ExpositionWinner> _expositionWinners = new List<ExpositionWinner>()
		{
			new ExpositionWinner()
				{
					Id = 1,
					ExpositionMember = new ExpositionMember()
					{
						Dog = new Dog()
						{
							Id = 1,
							Name = "Dog1",
							OwnerId = 1,
							DogType = new DogType()
							{
								Id = 1,
								Name = "labrador"
							}
						},
					}
				},
				new ExpositionWinner()
				{
					Id = 2,
					ExpositionMember = new ExpositionMember()
					{
						Dog = new Dog()
						{
							Id = 2,
							Name = "Dog2",
							OwnerId = 1,
							DogType = new DogType()
							{
								Id = 1,
								Name = "labrador"
							}
						}
					}
				},
				new ExpositionWinner()
				{
					Id = 3,
					ExpositionMember = new ExpositionMember()
					{
						Dog = new Dog()
						{
							Id = 3,
							Name = "Dog3",
							OwnerId = 2,
							DogType = new DogType()
							{
								Id = 1,
								Name = "labrador"
							}
						}
					}
				},
				new ExpositionWinner()
				{
					Id = 4,
					ExpositionMember = new ExpositionMember()
					{
						Dog = new Dog()
						{
							Id = 3,
							Name = "Dog3",
							OwnerId = 3,
							DogType = new DogType()
							{
								Id = 1,
								Name = "labrador"
							}
						}
					}
				}
		};

        private static List<User> _users = new List<User>()
		{
			new User()
			{
				Id = 1,
				LastName = "last user 1",
				FirstName = "firstn user 1",
				MiddleName = "middle user 1",
				Payments = new List<Payment>()
				{
					new Payment()
					{
						Id = 1,
						PaymentType = new PaymentType()
						{
							Id = 1,
							Name = "членский взнос"
						},
						PayDate = new DateTime(2012, 11, 11),
						PaySize = 1000
					},
					new Payment()
					{
						Id = 2,
						PaymentType = new PaymentType()
						{
							Id = 1,
							Name = "членский взнос"
						},
						PayDate = new DateTime(2013, 11, 11),
						PaySize = 1000
					},
					new Payment()
					{
						Id = 4,
						PaymentType = new PaymentType()
						{
							Id = 1,
							Name = "членский взнос"
						},
						PayDate = new DateTime(2014, 11, 11),
						PaySize = 1000
					},
                    new Payment()
                    {
                        Id = 5,
                        PaymentType = new PaymentType()
                        {
                            Id = 2,
                            Name = "Что-то еще"
                        },
                        PayDate = new DateTime(2014, 10, 11),
                        PaySize = 500
                    },
                    new Payment()
                    {
                        Id = 6,
                        PaymentType = new PaymentType()
                        {
                            Id = 2,
                            Name = "Что-то еще"
                        },
                        PayDate = new DateTime(2013, 10, 11),
                        PaySize = 1500
                    },
                    new Payment()
                    {
                        Id = 6,
                        PaymentType = new PaymentType()
                        {
                            Id = 3,
                            Name = "Что-то еще другое"
                        },
                        PayDate = new DateTime(2014, 1, 11),
                        PaySize = 2000
                    }
				}
			},
			new User()
			{
				Id = 2,
				LastName = "last user 2",
				FirstName = "first user 2",
				MiddleName = "middle user 2",
				Payments = new List<Payment>()
				{
					new Payment()
					{
						Id = 5,
						PaymentType = new PaymentType()
						{
							Id = 1,
							Name = "членский взнос"
						},
						PayDate = new DateTime(2014, 10, 5),
						PaySize = 1000
					},
					new Payment()
					{
						Id = 6,
						PaymentType = new PaymentType()
						{
							Id = 1,
							Name = "Комиссия с продажи"
						},
						PayDate = new DateTime(2015, 2, 3),
						PaySize = 4000
					},
				}
			},
			new User()
			{
				Id = 3,
				LastName = "last user 3",
				FirstName = "first user 3",
				MiddleName = "middle user 3",
			}
		};

        private static List<Dog> _dogs = new List<Dog>()
		{
			new Dog()
			{
				Id = 1,
				Age = 11,
				Vaccinations = new List<Vaccination>()
				{
					new Vaccination()
					{
						DateVacination = new DateTime(2011, 10, 11),
						Id = 1,
						IdDog = 1,
						Name = "Бешенство"
					},
					new Vaccination()
					{
						DateVacination = new DateTime(2012, 10, 11),
						Id = 2,
						IdDog = 1,
						Name = "Бешенство2"
					}
				},
				Name = "tuzik",
				OwnerId = 1,
				Sex = 1,
				DogType = new DogType()
				{
					Id = 1,
					Name = "labrador"
				},
				User = new User()
				{
					Id = 1,
					FirstName = "name",
					LastName = "last",
					MiddleName = "middle"
				},
                 ExpositionMembers = new List<ExpositionMember>()
                 {
                     new ExpositionMember()
                     {
						 Id = 1,
                         Exposition = new Exposition()
                         {
                             Id = 1
                         },
                         Dog = new Dog()
				        {
				            Id = 1,
				            Name = "Dog1",
				            OwnerId = 1,
				            DogType = new DogType()
				            {
					            Id = 1,
					            Name = "labrador"
				            }
                         },
						 ExpositionWinners = new List<ExpositionWinner>()
						 {
							 new ExpositionWinner()
							 {
								 Id = 1,
								 IdExpositionMember = 1,
								 WinnerPlace = 5
							 }
						 }
                     }
                 }
			},
			new Dog()
			{
				Id = 2,
				Age = 11,
				Vaccinations = new List<Vaccination>()
				{
					new Vaccination()
					{
						DateVacination = new DateTime(2014, 10, 11),
						Id = 3,
						IdDog = 2,
						Name = "Бешенство"
					},
				},
				Name = "tuzik 2",
				OwnerId = 1,
				Sex = 1,
				DogType = new DogType()
				{
					Id = 1,
					Name = "labrador"
				},
				User = new User()
				{
					Id = 1,
					FirstName = "name",
					LastName = "last",
					MiddleName = "middle"
				}
			}
		};

        private static List<PaymentType> _paymentTypes = new List<PaymentType>()
        {
            new PaymentType()
            {
                Id = 1,
                IdPeriod = 1,
                Name = "Членский взнос",
                Payments = new List<Payment>()
                {
                    new Payment()
                    {
                        Id = 1,
                        Description = "Заплатил взнос",
                        IdType = 1,
                        PayDate = new DateTime(2014, 10, 25),
                        PaySize = 1000,
                        UserId = 1,
                        User = new User()
                        {
                            Id = 1,
                            LastName = "lastname1",
                            FirstName = "firstName1",
                            MiddleName = "middleName1"
                        }
                    },
                    new Payment()
                    {
                        Id = 2,
                        Description = "Заплатил взнос",
                        IdType = 1,
                        PayDate = new DateTime(2013, 10, 25),
                        PaySize = 1500,
                        UserId = 2,
                        User = new User()
                        {
                            Id = 2,
                            LastName = "lastname2",
                            FirstName = "firstName2",
                            MiddleName = "middleName2"
                        }
                    },
                    new Payment()
                    {
                        Id = 3,
                        Description = "Заплатил взнос еще раз",
                        IdType = 1,
                        PayDate = new DateTime(2015, 10, 25),
                        PaySize = 2000,
                        UserId = 2,
                        User = new User()
                        {
                            Id = 2,
                            LastName = "lastname2",
                            FirstName = "firstName2",
                            MiddleName = "middleName2"
                        }
                    }
                }
            },
            new PaymentType()
            {
                Id = 2,
                IdPeriod = 2,
                Name = "Какой-то другой взнос",
                Payments = new List<Payment>()
                {
                    new Payment()
                    {
                        Description = "description",
                        Id = 4,
                        IdType = 2,
                        PayDate = new DateTime(2014, 5, 11),
                        PaySize = 1500,
                        UserId = 3,
                        User = new User()
                        {
                            MiddleName = "middkeUser",
                            LastName = "lastNameAnotherUser",
                            FirstName = "first",
                            Id = 3
                        }
                    },
                    new Payment()
                    {
                        Description = "another description",
                        Id = 5,
                        IdType = 2,
                        PayDate = new DateTime(2013, 5, 11),
                        PaySize = 1700,
                        UserId = 4,
                        User = new User()
                        {
                            MiddleName = "middkeUser4",
                            LastName = "4User",
                            FirstName = "4first",
                            Id = 4
                        }
                    }
                }
            }
        };

        public static IEnumerable<ExpositionWinner> GetExpositionWinners()
        {
            return _expositionWinners;
        }

        public static IEnumerable<User> GetUsers()
        {
            return _users;
        }

        public static IEnumerable<Dog> GetDogs()
        {
            return _dogs;
        }

        public static IEnumerable<PaymentType> GetPaymentTypes()
        {
            return _paymentTypes;
        }
    }
}
