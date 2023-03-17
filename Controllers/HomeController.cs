﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projet2_CRA.Models;
using projet2_CRA.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projet2_CRA.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            using (Dal dal = new Dal())
            {
                // On récupère tous les utilisateurs pour les stocker dans une liste
                List<User> users = dal.GetAllUsers();
                HomeViewModel hvm = new HomeViewModel { Users = users };
                return View(hvm);
            }
        }

        public IActionResult UserDetail(int id)
        {
            using (Dal dal = new Dal())
            {
                // On récupère l'utilisateur en fonction de son id
                User user = dal.GetUserById(id);
                // Si l'utilisateur existe en BDD
                if (user != null)
                {
                    // On l'envoie en paramètre à la vue UserDetail
                    return View(user);
                }
                // Si l'utilisateur n'existe pas, redirection vers l'Index
                return RedirectToAction("Index");
            }
        }

        public IActionResult UserForm(int id)
        {
            using (Dal dal = new Dal())
            {
                User user = dal.GetUserById(id);
                if(user != null)
                {   
                    return View(user);
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        // Une fois qu'on appuie sur le bouton du formulaire, cette méthode récupère un objet user
        public IActionResult UserForm(User user)
        {
            using(Dal dal= new Dal())
            {
                // On récupère l'ensemble des données renseignées pour cet utilisateur en BDD grâce à une requête
                User oldUser = dal.GetUserById(user.Id);
                // On remplace un par un l'ensemble des champs du formulaire
                oldUser.Login = user.Login;
                oldUser.UserData.Firstname = user.UserData.Firstname;
                oldUser.UserData.Lastname = user.UserData.Lastname;
                oldUser.UserData.Birthday = user.UserData.Birthday;
                oldUser.UserData.Email = user.UserData.Email;
                // On envoie l'ancien user maintenant modifié à la méthode pour confirmer les changements dans la BDD
                dal.ModifyUser(oldUser);
            }
           // Une fois que c'est réalisé, on redirige vers la vue UserDetail avec un id en paramètre.
           // On va donc sur la page des détails de l'utilisateur qu'on vient de modifier.
            return RedirectToAction("UserDetail", new { @id = user.Id });
        }
    }
}

