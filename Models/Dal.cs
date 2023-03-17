using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace projet2_CRA.Models
{
	public class Dal : IDal
	{
        private BddContext _bddContext;
        public Dal()
        {
            // On instancie BddContext pour l'utiliser dans les méthodes qui interagissent avec la BDD
            _bddContext = new BddContext();
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public int CreateCompany(string name, string adress, float totalCa)
        {
            Company company = new Company() { Name = name, Adress = adress, TotalCA = totalCa };
            _bddContext.Companies.Add(company);
            _bddContext.SaveChanges();
            return company.Id;
        }

        public List<Company> GetAllCompanies()
        {
            return _bddContext.Companies.ToList();
        }

        public int CreateDataUser(string firstName, string lastName, DateTime birthDay, string email)
        {
            UserData userData = new UserData() { Firstname = firstName, Lastname = lastName, Birthday = birthDay, Email = email };
            _bddContext.UsersData.Add(userData);
            _bddContext.SaveChanges();
            return userData.Id;
        }

        public void CreateUser(string login,string password, DateTime creationDate, string lastName, string firstName, DateTime birthDay, string email, int companyId)
        {
            int id = CreateDataUser(firstName, lastName, birthDay, email);
            User user = new User() { Login = login, Password = password, CreationDate = creationDate, UserDataId= id, CompanyId = companyId };
            _bddContext.Users.Add(user);
            _bddContext.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _bddContext.Users.ToList();
        }

        public User GetUserById(int id)
        {
            // Le Include permet ici de récupérer les données du UserData (qui est lié à User par une clé étrangère)
            // Sans Include, impossible de récupérer certaines données en faisant User.Userdata.FirstName, par exemple.
            User user = _bddContext.Users.Include(u => u.UserData).SingleOrDefault(u => u.Id == id);
            return user;
        }

        public void ModifyUser(User oldUser)
        {
            // Update permet de mettre à jour directement le bon User dans la table (grâce à l'id sans doute)
            this._bddContext.Users.Update(oldUser);
            this._bddContext.SaveChanges();
        }

        public UserData GetOneUserData(int id)
        {
            UserData userData = _bddContext.UsersData.SingleOrDefault(u => u.Id == id);
            return userData;
        }

        public List<Notification> GetAllNotificationsByUser(int id)
        {
            List<Notification> notifications = _bddContext.Notifications.Where(n => n.UserId == id).ToList();
            return notifications;
        }
    }
}

