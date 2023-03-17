using System;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;

namespace projet2_CRA.Models
{
    // Classe qui permet la création des tables dans la BDD
    public class BddContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserData> UsersData { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Les paramètres du serveur changent en fonction des configurations personnelles
            optionsBuilder.UseMySql("server=localhost;port=8889;user id=root;password=root;database=projet2");
        }

        // Méthode qui permet d'initialiser des données lors du démarrage de l'application
        // Obsolète une fois que la BDD est déjà remplie.
        public void InitializeDb()
        {
            // On garantit la suppression de la BDD si elle existe
            this.Database.EnsureDeleted();
            // On garantit la création de la BDD
            this.Database.EnsureCreated();

            // Ajout d'une entreprise dans la table Companies
            this.Companies.Add(new Company { Id = 1, Name = "Isika", Adress = "25 rue de la Boustifaille 75000 Paris", TotalCA = 200000 });

            // Ajout de données persos pour chaque utilisateur dans la table UsersData
            this.UsersData.AddRange(
                new UserData { Id = 1, Lastname="Xuxu", Firstname="Xaxa", Birthday= new DateTime(2018,12,4), Email="xxxx@gmail.com"},
                new UserData { Id = 2, Lastname = "Watson", Firstname = "Bobby", Birthday = new DateTime(2015, 5, 28), Email = "bobby.watson@gmail.com" },
                new UserData { Id = 3, Lastname = "Multipass", Firstname = "Lilou", Birthday = new DateTime(2019, 6, 18), Email = "lilou@gmail.com" }
                );

            // Ajout d'utilisateurs dans la table Users
            this.Users.AddRange(
                new User { Id = 1, Login = "xxxxx", Password = "ppppp", CreationDate = DateTime.Now, CompanyId = 1, UserDataId = 1 },
                new User { Id= 2, Login = "Bob", Password = "ppppp", CreationDate = DateTime.Now, CompanyId = 1, UserDataId = 2 },
                new User { Id= 3, Login = "Lilou", Password = "ppppp", CreationDate = DateTime.Now, CompanyId = 1, UserDataId = 3 }
                );

            // Ajout de roles dans la table Roles
            this.Roles.AddRange(
                new Role { JobLabel= "Développeur", RoleType = RoleType.SALARIE},
                new Role { JobLabel= "Chef de projet", RoleType=RoleType.MANAGER},
                new Role { JobLabel= "Administrateur réseau", RoleType=RoleType.ADMIN}
                );

            // Ajout de liens entre des clés étrangères (user et role) dans la table RoleUsers
            this.RoleUsers.AddRange(
                new RoleUser { UserId=1, RoleId=1},
                new RoleUser { UserId=2, RoleId=1},
                new RoleUser { UserId=2, RoleId=2},
                new RoleUser { UserId=3, RoleId=3}
                );

            // Ajout de notifications dans la table Notifications
            // L'attribut classContext serait utilisé par la suite avec les classes CSS bootstrap pour créer un style dynamique en fonction de la notification reçue
            this.Notifications.AddRange(
                new Notification { MessageContent = "Cra validé avec succès", ClassContext = "success", UserId = 1 },
                new Notification { MessageContent = "Cra refusée, il manque des données sur les jours du 12 au 14 février", ClassContext = "danger", UserId = 1 }
                );

            // On sauvegarde les changements apportés à la BDD
            this.SaveChanges();
        }
    }
}

