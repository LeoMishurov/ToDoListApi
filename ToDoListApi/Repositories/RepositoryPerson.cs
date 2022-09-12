using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using ToDoListApi.Models;

namespace ToDoListApi.Repositories
{
    public class RepositoryPerson
    {
        MyContext context = new();

        public RepositoryPerson(MyContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// находит пользователя по логину и паролю
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Person GetPerson(string username, string password)
        {
            var person = context.People.FirstOrDefault(x => x.Login == username && x.Password == password);
            return person;
        }

        /// <summary>
        /// проверка есть ли в бд посльзователь с таким логином
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool PersonExist(string username)
        {
            var isPersonExist = context.People.Any(x => x.Login == username);
            return isPersonExist;
        }

        /// <summary>
        /// добавление пользователя в бд
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void AddPerson(string username, string password)
        {
            Person person = new Person
            {
                Login = username,
                Password = password
            };

            context.People.Add(person);
            context.SaveChanges();
        }
    }
}
