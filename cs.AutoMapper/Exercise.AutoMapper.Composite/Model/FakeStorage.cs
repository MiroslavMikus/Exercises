using System.Collections.Generic;

namespace Exercise.AutoMapper.Composite.Model
{
    public static class FakeStorage
    {
        public static IEnumerable<UserSource> GetUsers()
        {
            return new List<UserSource>
            {
                new UserSource
                {
                    Name = "Juliane",
                    Child = new List<UserSource>
                    {
                        new UserSource
                        {
                            Name = "Mirka"
                        },
                        new UserSource
                        {
                            Name = "Samuel"
                        }
                    }
                },
                new UserSource
                {
                    Name = "Miro",
                    Child = new List<UserSource>
                    {
                        new UserSource
                        {
                            Name = "Nertil",
                            Child = new List<UserSource>
                            {
                                new UserSource
                                {
                                    Name = "Ervis"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
